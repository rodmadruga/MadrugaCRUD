Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Dynamic
Imports System.Globalization
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Text
Imports Dapper
Imports System.Reflection

Namespace MadrugaCRUD.Repository
    Public Class SQLRepository(Of tT As Class)
        Implements ISQLRepository(Of tT)

        Private ReadOnly _tableName As String
        Private ReadOnly _connectionString As String

        Friend ReadOnly Property Connection As IDbConnection
            Get
                Return New SqlConnection(_connectionString)
            End Get
        End Property

        Public Sub New(ByVal tableName As String, ByVal Optional connectionString As String = "")
            _tableName = tableName
            _connectionString = If(String.IsNullOrEmpty(connectionString), ConfigurationManager.ConnectionStrings("connection").ConnectionString, ConfigurationManager.ConnectionStrings(connectionString).ConnectionString)
        End Sub

        Public Overridable Sub Add(ByVal item As Dictionary(Of String, Object))
            Dim lstQuery As List(Of String) = New List(Of String)()
            Dim strQuery = ""

            If item.Any() Then
                For Each parameter In item

                    If parameter.Value Is Nothing Then
                        Continue For
                    ElseIf parameter.Value.GetType() Is GetType(Boolean) OrElse parameter.Value.GetType() Is GetType(Integer) Then
                        lstQuery.Add(String.Format("@{0}={1}", parameter.Key, parameter.Value))
                    Else
                        lstQuery.Add(String.Format("@{0}='{1}'", parameter.Key, parameter.Value))
                    End If
                Next

                strQuery = String.Join(",", lstQuery)
            End If

            Using cn = Connection
                cn.Open()
                cn.Query(String.Format("exec {0} {1}", _tableName, strQuery))
            End Using
        End Sub

        Private Function TratarValueString(ByVal value As String) As String
            Dim retorno = value
            retorno = retorno.Replace("'", "''")
            Return retorno
        End Function

        'public virtual void Update(T item)
        '{
        '    using (IDbConnection cn = Connection)
        '    {
        '        var parameters = (object)Mapping(item);
        '        cn.Open();
        '        cn.Update(_tableName, parameters);
        '    }
        '}

        Public Overridable Sub Remove(ByVal item As tT) Implements ISQLRepository(Of tT).Remove
            Using cn = Connection
                cn.Open()
                'cn.Execute("DELETE FROM " + _tableName + " WHERE ID=@ID", new { ID = item.ID });
            End Using
        End Sub

        Public Overridable Function FindByID(ByVal id As Guid) As tT Implements ISQLRepository(Of tT).FindByID
            Dim item As tT = Nothing

            Using cn = Connection
                cn.Open()
                item = cn.Query(Of tT)("SELECT * FROM " & _tableName & " WHERE ID=@ID", New With {
                    id
                }).SingleOrDefault()
            End Using

            Return item
        End Function

        Public Overridable Function Find(ByVal predicate As Expression(Of Func(Of tT, Boolean))) As IEnumerable(Of tT) Implements ISQLRepository(Of tT).Find
            Dim items As IEnumerable(Of tT) = Nothing

            ' extract the dynamic sql query and parameters from predicate
            Dim result = DynamicQuery.GetDynamicQuery(_tableName, predicate)

            Using cn = Connection
                cn.Open()
                items = cn.Query(Of tT)(result.Sql, CObj(result.Param))
            End Using

            Return items
        End Function

        Public Overridable Function Exec(ByVal parameters As Dictionary(Of String, Object)) As IEnumerable(Of tT)
            Try
                Dim item As IEnumerable(Of tT) = Nothing
                Dim lstQuery As List(Of String) = New List(Of String)()
                Dim strQuery = ""

                If parameters.Any() Then
                    For Each parameter In parameters

                        If parameter.Value Is Nothing Then
                            Continue For
                        ElseIf parameter.Value.GetType() Is GetType(Boolean) OrElse parameter.Value.GetType() Is GetType(Integer) Then
                            lstQuery.Add(String.Format("@{0}={1}", parameter.Key, parameter.Value))
                        ElseIf parameter.Value.GetType() Is GetType(Decimal) Then
                            Dim val = CDec(parameter.Value)
                            Dim valstr = val.ToString("0.00", CultureInfo.InvariantCulture)
                            lstQuery.Add(String.Format("@{0}={1}", parameter.Key, valstr))
                        ElseIf parameter.Value.GetType() Is GetType(Date) Then
                            Dim val = CDate(parameter.Value)
                            Dim valstr = val.ToString("yyyy-MM-dd HH:mm:ss")
                            lstQuery.Add(String.Format("@{0}='{1}'", parameter.Key, valstr))
                        ElseIf parameter.Value.GetType() Is GetType(String) Then
                            Dim val = TratarValueString(CStr(parameter.Value))
                            lstQuery.Add(String.Format("@{0}='{1}'", parameter.Key, val))
                        Else
                            lstQuery.Add(String.Format("@{0}='{1}'", parameter.Key, parameter.Value))
                        End If
                    Next

                    strQuery = String.Join(",", lstQuery)
                End If

                Dim t = New DynamicParameters()

                Using cn = Connection
                    cn.Open()
                    item = cn.Query(Of tT)(String.Format("exec {0} {1}", _tableName, strQuery), Nothing, Nothing, True, 9999999, Nothing)
                End Using

                Return item
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Overridable Function ExecFunction(ByVal parameters As List(Of Object)) As IEnumerable(Of tT)
            Try
                Dim item As IEnumerable(Of tT) = Nothing
                Dim lstQuery As List(Of String) = New List(Of String)()
                Dim strQuery = ""

                If parameters.Any() Then
                    For Each parameter In parameters

                        If parameter Is Nothing Then
                            Continue For
                        ElseIf parameter.GetType() Is GetType(Boolean) OrElse parameter.GetType() Is GetType(Integer) Then
                            lstQuery.Add(String.Format("{0}", parameter))
                        ElseIf parameter.GetType() Is GetType(Decimal) Then
                            Dim val = CDec(parameter)
                            Dim valstr = val.ToString("0.00", CultureInfo.InvariantCulture)
                            lstQuery.Add(String.Format("{0}", parameter, valstr))
                        ElseIf parameter.GetType() Is GetType(Date) Then
                            Dim val = CDate(parameter)
                            Dim valstr = val.ToString("yyyy-MM-dd HH:mm:ss")
                            lstQuery.Add(String.Format("'{0}'", parameter, valstr))
                        ElseIf parameter.GetType() Is GetType(String) Then
                            Dim val = TratarValueString(CStr(parameter))
                            lstQuery.Add(String.Format("'{0}'", parameter, val))
                        Else
                            lstQuery.Add(String.Format("'{0}'", parameter))
                        End If
                    Next

                    strQuery = String.Join(",", lstQuery)
                End If

                Dim t = New DynamicParameters()

                Using cn = Connection
                    cn.Open()
                    item = cn.Query(Of tT)(String.Format("select {0}({1})", _tableName, strQuery), Nothing, Nothing, True, 9999999, Nothing)
                End Using

                Return item
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Overridable Function ExecScalar(ByVal parameters As Dictionary(Of String, Object)) As Integer
            Try
                Dim item = 0
                Dim lstQuery As List(Of String) = New List(Of String)()
                Dim strQuery = ""

                If parameters.Any() Then
                    For Each parameter In parameters

                        If parameter.Value Is Nothing Then
                            Continue For
                        ElseIf parameter.Value.GetType() Is GetType(Boolean) OrElse parameter.Value.GetType() Is GetType(Integer) Then
                            lstQuery.Add(String.Format("@{0}={1}", parameter.Key, parameter.Value))
                        Else
                            lstQuery.Add(String.Format("@{0}='{1}'", parameter.Key, parameter.Value))
                        End If
                    Next

                    strQuery = String.Join(",", lstQuery)
                End If

                Dim t = New DynamicParameters()

                Using cn = Connection
                    cn.Open()
                    item = cn.ExecuteScalar(Of Integer)(String.Format("exec {0} {1}", _tableName, strQuery, New With {
                        .commandTimeout = 9999999
                    }))
                End Using

                Return item
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Overridable Function ExecWithGetParameter(ByVal parameters As Dictionary(Of String, Object), ByVal scopeIdentity As String) As Integer
            Try
                Dim Dp = New DynamicParameters()

                If parameters.Any() Then
                    For Each parameter In parameters

                        If parameter.Value Is Nothing Then
                            Continue For
                        Else
                            Dp.Add("@" & parameter.Key, parameter.Value)
                        End If
                    Next
                End If

                Dp.Add("@" & scopeIdentity, 1, dbType:=DbType.Int32, direction:=ParameterDirection.Output)
                'p.Add("@c", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                Using cn = Connection
                    cn.Execute(_tableName, Dp, commandType:=CommandType.StoredProcedure)
                End Using

                Return Dp.[Get](Of Integer)("@" & scopeIdentity)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Overridable Function FindAll() As IEnumerable(Of tT) Implements ISQLRepository(Of tT).FindAll
            Dim items As IEnumerable(Of tT) = Nothing

            Using cn = Connection
                cn.Open()
                items = cn.Query(Of tT)("SELECT * FROM " & _tableName)
            End Using

            Return items
        End Function

        Public Overridable Function ExecQuery() As IEnumerable(Of tT)
            Dim items As IEnumerable(Of tT) = Nothing

            Using cn = Connection
                cn.Open()
                items = cn.Query(Of tT)(_tableName)
            End Using

            Return items
        End Function

        Public Overridable Function Exec(ByVal parameters As Dictionary(Of String, Object), ByVal cn As IDbConnection, ByVal transacao As IDbTransaction) As IEnumerable(Of tT)
            Try
                Dim item As IEnumerable(Of tT) = Nothing
                Dim lstQuery As List(Of String) = New List(Of String)()
                Dim strQuery = ""

                If parameters.Any() Then
                    For Each parameter In parameters

                        If parameter.Value Is Nothing Then
                            Continue For
                        ElseIf parameter.Value.GetType() Is GetType(Boolean) OrElse parameter.Value.GetType() Is GetType(Integer) Then
                            lstQuery.Add(String.Format("@{0}={1}", parameter.Key, parameter.Value))
                        ElseIf parameter.Value.GetType() Is GetType(Decimal) Then
                            Dim val = CDec(parameter.Value)
                            Dim valstr = val.ToString("0.00", CultureInfo.InvariantCulture)
                            lstQuery.Add(String.Format("@{0}={1}", parameter.Key, valstr))
                        ElseIf parameter.Value.GetType() Is GetType(Date) Then
                            Dim val = CDate(parameter.Value)
                            Dim valstr = val.ToString("yyyy-MM-dd HH:mm:ss")
                            lstQuery.Add(String.Format("@{0}='{1}'", parameter.Key, valstr))
                        ElseIf parameter.Value.GetType() Is GetType(String) Then
                            Dim val = TratarValueString(CStr(parameter.Value))
                            lstQuery.Add(String.Format("@{0}='{1}'", parameter.Key, val))
                        Else
                            lstQuery.Add(String.Format("@{0}='{1}'", parameter.Key, parameter.Value))
                        End If
                    Next

                    strQuery = String.Join(",", lstQuery)
                End If

                Dim t = New DynamicParameters()
                item = cn.Query(Of tT)(String.Format("exec {0} {1}", _tableName, strQuery), Nothing, transacao, True, 9999999, Nothing)
                Return item
            Catch ex As Exception
                Throw ' new ApiException() { ErrorCode = (int)HttpStatusCode.InternalServerError, ErrorDescription = ex.Message, Source = "SQLRepository" };
            End Try
        End Function
    End Class

    Public Interface ISQLRepository(Of T As Class)
        'void Add(T item);
        Sub Remove(ByVal item As T)
        'void Update(T item);
        Function FindByID(ByVal id As Guid) As T
        Function Find(ByVal predicate As Expression(Of Func(Of T, Boolean))) As IEnumerable(Of T)
        Function FindAll() As IEnumerable(Of T)
    End Interface

    Public Class QueryResult
        ''' <summary>
        ''' The _result
        ''' </summary>
        Private ReadOnly _result As Tuple(Of String, Object)

        ''' <summary>
        ''' Gets the SQL.
        ''' </summary>
        ''' <value>
        ''' The SQL.
        ''' </value>
        Public ReadOnly Property Sql As String
            Get
                Return _result.Item1
            End Get
        End Property

        ''' <summary>
        ''' Gets the param.
        ''' </summary>
        ''' <value>
        ''' The param.
        ''' </value>
        Public ReadOnly Property Param As Object
            Get
                Return _result.Item2
            End Get
        End Property

        ''' <summary>
        ''' Initializes a new instance of the <seecref="QueryResult"/> class.
        ''' </summary>
        ''' <paramname="sql">The SQL.</param>
        ''' <paramname="param">The param.</param>
        Public Sub New(ByVal sql As String, ByVal param As Object)
            _result = New Tuple(Of String, Object)(sql, param)
        End Sub
    End Class

    ''' <summary>
    ''' Dynamic query class.
    ''' </summary>
    Public NotInheritable Class DynamicQuery
        ''' <summary>
        ''' Gets the insert query.
        ''' </summary>
        ''' <paramname="tableName">Name of the table.</param>
        ''' <paramname="item">The item.</param>
        ''' <returns>
        ''' The Sql query based on the item properties.
        ''' </returns>
        Public Shared Function GetInsertQuery(ByVal tableName As String, ByVal item As Object) As String
            Dim props As PropertyInfo() = item.[GetType]().GetProperties()
            Dim columns As String() = props.[Select](Function(p) p.Name).Where(Function(s) Not Equals(s, "ID")).ToArray()
            Return String.Format("INSERT INTO {0} ({1}) OUTPUT inserted.ID VALUES (@{2})", tableName, String.Join(",", columns), String.Join(",@", columns))
        End Function

        ''' <summary>
        ''' Gets the update query.
        ''' </summary>
        ''' <paramname="tableName">Name of the table.</param>
        ''' <paramname="item">The item.</param>
        ''' <returns>
        ''' The Sql query based on the item properties.
        ''' </returns>
        Public Shared Function GetUpdateQuery(ByVal tableName As String, ByVal item As Object) As String
            Dim props As PropertyInfo() = item.[GetType]().GetProperties()
            Dim columns As String() = props.[Select](Function(p) p.Name).ToArray()
            Dim parameters = columns.[Select](Function(name) name & "=@" & name).ToList()
            Return String.Format("UPDATE {0} SET {1} WHERE ID=@ID", tableName, String.Join(",", parameters))
        End Function

        ''' <summary>
        ''' Gets the dynamic query.
        ''' </summary>
        ''' <paramname="tableName">Name of the table.</param>
        ''' <paramname="expression">The expression.</param>
        ''' <returns>A result object with the generated sql and dynamic params.</returns>
        Public Shared Function GetDynamicQuery(Of T)(ByVal tableName As String, ByVal expression As Expression(Of Func(Of T, Boolean))) As QueryResult
            Dim queryProperties = New List(Of QueryParameter)()
            Dim body = CType(expression.Body, BinaryExpression)
            Dim expando As IDictionary(Of String, Object) = New ExpandoObject()
            Dim builder = New StringBuilder()

            ' walk the tree and build up a list of query parameter objects
            ' from the left and right branches of the expression tree
            WalkTree(body, ExpressionType.Default, queryProperties)

            ' convert the query parms into a SQL string and dynamic property object
            builder.Append("SELECT * FROM ")
            builder.Append(tableName)
            builder.Append(" WHERE ")

            For i = 0 To queryProperties.Count() - 1
                Dim item = queryProperties(i)

                If Not String.IsNullOrEmpty(item.LinkingOperator) AndAlso i > 0 Then
                    builder.Append(String.Format("{0} {1} {2} @{1} ", item.LinkingOperator, item.PropertyName, item.QueryOperator))
                Else
                    builder.Append(String.Format("{0} {1} @{0} ", item.PropertyName, item.QueryOperator))
                End If

                expando(item.PropertyName) = item.PropertyValue
            Next

            Return New QueryResult(builder.ToString().TrimEnd(), expando)
        End Function

        ''' <summary>
        ''' Walks the tree.
        ''' </summary>
        ''' <paramname="body">The body.</param>
        ''' <paramname="linkingType">Type of the linking.</param>
        ''' <paramname="queryProperties">The query properties.</param>
        Private Shared Sub WalkTree(ByVal body As BinaryExpression, ByVal linkingType As ExpressionType, ByRef queryProperties As List(Of QueryParameter))
            If body.NodeType <> ExpressionType.AndAlso AndAlso body.NodeType <> ExpressionType.OrElse Then
                Dim propertyName = GetPropertyName(body)
                Dim propertyValue As Object = body.Right
                Dim opr = GetOperator(body.NodeType)
                Dim link = GetOperator(linkingType)
                queryProperties.Add(New QueryParameter(link, propertyName, propertyValue.Value, opr))
            Else
                WalkTree(CType(body.Left, BinaryExpression), body.NodeType, queryProperties)
                WalkTree(CType(body.Right, BinaryExpression), body.NodeType, queryProperties)
            End If
        End Sub

        ''' <summary>
        ''' Gets the name of the property.
        ''' </summary>
        ''' <paramname="body">The body.</param>
        ''' <returns>The property name for the property expression.</returns>
        Private Shared Function GetPropertyName(ByVal body As BinaryExpression) As String
            Dim propertyName As String = body.Left.ToString().Split(New Char() {"."c})(1)

            If body.Left.NodeType = ExpressionType.Convert Then
                ' hack to remove the trailing ) when convering.
                propertyName = propertyName.Replace(")", String.Empty)
            End If

            Return propertyName
        End Function

        ''' <summary>
        ''' Gets the operator.
        ''' </summary>
        ''' <paramname="type">The type.</param>
        ''' <returns>
        ''' The expression types SQL server equivalent operator.
        ''' </returns>
        ''' <exceptioncref="System.NotImplementedException"></exception>
        Private Shared Function GetOperator(ByVal type As ExpressionType) As String
            Select Case type
                Case ExpressionType.Equal
                    Return "="
                Case ExpressionType.NotEqual
                    Return "!="
                Case ExpressionType.LessThan
                    Return "<"
                Case ExpressionType.GreaterThan
                    Return ">"
                Case ExpressionType.AndAlso, ExpressionType.And
                    Return "AND"
                Case ExpressionType.Or, ExpressionType.OrElse
                    Return "OR"
                Case ExpressionType.Default
                    Return String.Empty
                Case Else
                    Throw New NotImplementedException()
            End Select
        End Function
    End Class

    ''' <summary>
    ''' Class that models the data structure in coverting the expression tree into SQL and Params.
    ''' </summary>
    Friend Class QueryParameter
        Public Property LinkingOperator As String
        Public Property PropertyName As String
        Public Property PropertyValue As Object
        Public Property QueryOperator As String

        ''' <summary>
        ''' Initializes a new instance of the <seecref="QueryParameter"/> class.
        ''' </summary>
        ''' <paramname="linkingOperator">The linking operator.</param>
        ''' <paramname="propertyName">Name of the property.</param>
        ''' <paramname="propertyValue">The property value.</param>
        ''' <paramname="queryOperator">The query operator.</param>
        Friend Sub New(ByVal linkingOperator As String, ByVal propertyName As String, ByVal propertyValue As Object, ByVal queryOperator As String)
            Me.LinkingOperator = linkingOperator
            Me.PropertyName = propertyName
            Me.PropertyValue = propertyValue
            Me.QueryOperator = queryOperator
        End Sub
    End Class
End Namespace
