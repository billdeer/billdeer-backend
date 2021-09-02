﻿using Billdeer.Core.Entities.Abstract;
using Billdeer.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.Entities.Concrete
{
    public class User : EntityBase<long>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// This property will be used for BAN etc situations.
        /// </summary>
        public bool Status { get; set; }

        public virtual ICollection<OperationClaim> OperationClaims { get; set; }
    }
}
