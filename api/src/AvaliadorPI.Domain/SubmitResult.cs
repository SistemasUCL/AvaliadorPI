using FluentValidation.Results;
using System;

namespace AvaliadorPI.Domain
{
    public class SubmitResult<TEntity> where TEntity : Entity<TEntity>, new()
    {
        public SubmitResult(TEntity entity, ValidationResult result)
        {
            Entity = entity;
            Result = result;
        }

        public Guid Id => Entity.Id;
        public bool IsValid => Result.IsValid;

        public TEntity Entity { get; }
        public ValidationResult Result { get; }
    }
}
