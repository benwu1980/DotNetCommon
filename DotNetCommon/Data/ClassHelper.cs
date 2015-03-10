using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Concurrent;

namespace DotNetCommon.Data
{
    public class ClassHelper
    {
        public PropertyInfo[] Members { get; private set; }

        public ClassHelper(Type type)
        {
            Members = type.GetProperties();
        }


        private static ConcurrentDictionary<Type, Delegate> Jar = new ConcurrentDictionary<Type, Delegate>();
        public static Func<IDataRecord, T> DynamicDataReader<T>()
        {
            Delegate resDelegate;
            if (!Jar.TryGetValue(typeof(T), out resDelegate))
            {
                var indexerProperty = typeof(IDataRecord).GetProperty("Item", new[] { typeof(string) });
                var statements = new List<Expression>();

                ParameterExpression instanceParam = Expression.Variable(typeof(T));
                ParameterExpression readerParam = Expression.Parameter(typeof(IDataRecord));
                BinaryExpression createInstance = Expression.Assign(instanceParam, Expression.New(typeof(T)));

                statements.Add(createInstance);

                foreach (var property in typeof(T).GetProperties())
                {
                    MemberExpression getProperty = Expression.Property(instanceParam, property);
                    IndexExpression readValue = Expression.MakeIndex(readerParam, indexerProperty, new[] { Expression.Constant(property.Name) });
                    BinaryExpression assignProperty = Expression.Assign(getProperty, Expression.Convert(readValue, property.PropertyType));

                    statements.Add(assignProperty);
                }
                var returnStatement = instanceParam;
                statements.Add(returnStatement);

                var body = Expression.Block(instanceParam.Type, new[] { instanceParam }, statements.ToArray());

                var lambda = Expression.Lambda<Func<IDataRecord, T>>(body, readerParam);
                resDelegate = lambda.Compile();


                Jar[typeof(T)] = resDelegate;
            }
            return (Func<IDataRecord, T>)resDelegate;
        }


        private static ConcurrentDictionary<Type, Delegate> JarDR = new ConcurrentDictionary<Type, Delegate>();
        public static Func<DataRow, T> DynamicDataRow<T>()
        {
            Delegate resDelegate;
            if (!JarDR.TryGetValue(typeof(T), out resDelegate))
            {
                //获取由名称指定的列中的数据的索引器
                var indexerProperty = typeof(DataRow).GetProperty("Item", new[] { typeof(string) });
                var statements = new List<Expression>(); //定交一个表达式组

                ParameterExpression instanceParam = Expression.Variable(typeof(T)); //定义一个T类型的变量
                BinaryExpression createInstance = Expression.Assign(instanceParam, Expression.New(typeof(T))); //new一个T对象赋值给变量

                ParameterExpression readerParam = Expression.Parameter(typeof(DataRow));

                statements.Add(createInstance);

                foreach (var property in typeof(T).GetProperties())
                {
                    //获取T对象的一个属性,准备给它赋值
                    MemberExpression getProperty = Expression.Property(instanceParam, property);







                    //根据上面属性的名称，在dateRow的索引器中获取值
                    IndexExpression readValue = Expression.MakeIndex(readerParam, indexerProperty, new[] { Expression.Constant(property.Name) });

                    //将获取的值转化为上面属性的类型，并且赋值给属性
                    BinaryExpression assignProperty = Expression.Assign(getProperty, Expression.Convert(readValue, property.PropertyType));

                    statements.Add(assignProperty);
                }
                var returnStatement = instanceParam;
                statements.Add(returnStatement);

                var body = Expression.Block(instanceParam.Type, new[] { instanceParam }, statements.ToArray());

                var lambda = Expression.Lambda<Func<DataRow, T>>(body, readerParam);
                resDelegate = lambda.Compile();


                JarDR[typeof(T)] = resDelegate;
            }
            return (Func<DataRow, T>)resDelegate;
        }

    }
}
