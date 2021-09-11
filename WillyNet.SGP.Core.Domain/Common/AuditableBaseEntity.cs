﻿using System;

namespace WillyNet.SGP.Core.Domain.Common
{
    public abstract class AuditableBaseEntity
    {        
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
