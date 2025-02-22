﻿namespace SportEventManager.Domain.Common
{
    public abstract class BaseEntity : IAuditableEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
