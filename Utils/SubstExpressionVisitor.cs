// <copyright file="SubstExpressionVisitor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Mongorize.Utils;

using System.Linq.Expressions;

/// <summary>
/// Represents a class for building expressions with linq for
/// mongodb query. Extends linq <see cref="ExpressionVisitor"/>.
/// </summary>
internal class SubstExpressionVisitor : ExpressionVisitor
{
    /// <summary>
    /// Gets the dictionary of expressions.
    /// </summary>
    public Dictionary<Expression, Expression> Subst { get; } = new Dictionary<Expression, Expression>();

    /// <summary>
    /// Descend the tree of expression.
    /// </summary>
    /// <param name="node">The parameter expression node.</param>
    /// <returns>The resulting expression.</returns>
    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (this.Subst.TryGetValue(node, out Expression newValue))
        {
            return newValue;
        }

        return node;
    }
}