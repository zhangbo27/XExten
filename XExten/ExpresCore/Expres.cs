﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace XExten.ExpresCore
{
    sealed class Expres : IExpres
    {
        public T GetAttributeType<T, K>(Expression<Func<K, object>> Express)
        {
            if (Express == null) return default(T);
            MemberExpression Exp = (MemberExpression)Express.Body;
            var Attribute = (T)Exp.Member.GetCustomAttributes(typeof(T), true).FirstOrDefault();
            return Attribute;
        }

        public Expression<Func<T, object>> GetExpression<T>(Type AnonmouseType) where T : class, new()
        {
            List<Expression> Exps = new List<Expression>();
            ParameterExpression Parameter = Expression.Parameter(typeof(T), "t");
            ConstructorInfo Constructor  = AnonmouseType.GetType().GetConstructors().FirstOrDefault();
            typeof(T).GetProperties().ToList().ForEach(x =>
            {
                MemberExpression PropertyExpress = Expression.Property(Parameter, x.Name);
                UnaryExpression ConvterExpress = Expression.Convert(PropertyExpress, typeof(object));
                Exps.Add(ConvterExpress);
            });
            return Expression.Lambda<Func<T, object>>(Expression.New(Constructor, Exps), Parameter);
        }

        public void SetProptertiesValue<T>(Dictionary<String, Object> JsonValue, T Param) where T : class, new()
        {
            var type = typeof(T);
            foreach (var NameValue in JsonValue)
            {
                var property = type.GetProperty(NameValue.Key);

                var objectParameterExpression = Expression.Parameter(typeof(object), "obj");
                var objectUnaryExpression = Expression.Convert(objectParameterExpression, type);

                var valueParameterExpression = Expression.Parameter(typeof(object), "val");
                var valueUnaryExpression = Expression.Convert(valueParameterExpression, property.PropertyType);

                // 调用给属性赋值的方法
                var body = Expression.Call(objectUnaryExpression, property.GetSetMethod(), valueUnaryExpression);
                var expression = Expression.Lambda<Action<T, object>>(body, objectParameterExpression, valueParameterExpression);

                var Actions = expression.Compile();
                Actions(Param, NameValue.Value);
            };
        }
    }
}