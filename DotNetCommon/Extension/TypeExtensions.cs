using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace DotNetCommon.Extension
{
    /// <summary>
    /// 类型的扩展方法
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 是否可以为 null 的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableValueType(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }
        /// <summary>
        /// 是否允许null值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool AllowsNullValue(this Type type)
        {
            return (!type.IsValueType || IsNullableValueType(type));
        }

        /// <summary>
        /// 获取某个类型默认的值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefaultValue(this Type type)
        {
            return (AllowsNullValue(type)) ? null : Activator.CreateInstance(type);
        }

        /// <summary>
        /// 根据一个类型生成一个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="objects">参数</param>
        /// <returns>返回实体</returns>
        public static T CreateInstance<T>(this Type type, params object[] objects)
        {
            Type[] typeArray = objects.Select(obj => obj.GetType()).ToArray();
            Func<object[], object> deleObj = BuildDeletgateObj(type, typeArray);
            return (T)deleObj(objects);
        }

        /// <summary>
        /// 判断类型是否是空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type)
        {
            return (((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }

        private static Func<object[], object> BuildDeletgateObj(Type type, Type[] typeList)
        {
            ConstructorInfo constructor = type.GetConstructor(typeList);
            ParameterExpression paramExp = Expression.Parameter(typeof(object[]), "args_");
            Expression[] expList = GetExpressionArray(typeList, paramExp);

            NewExpression newExp = Expression.New(constructor, expList);

            Expression<Func<object[], object>> expObj = Expression.Lambda<Func<object[], object>>(newExp, paramExp);
            return expObj.Compile();
        }

        private static Expression[] GetExpressionArray(Type[] typeList, ParameterExpression paramExp)
        {
            List<Expression> expList = new List<Expression>();
            for (int i = 0; i < typeList.Length; i++)
            {
                var paramObj = Expression.ArrayIndex(paramExp, Expression.Constant(i));
                var expObj = Expression.Convert(paramObj, typeList[i]);
                expList.Add(expObj);
            }

            return expList.ToArray();
        }
    }
}
