﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Domain
{
    public class SecretIdentity
    {
        public int Id { get; set; }
        public string RealName { get; set; }
        public int SamuraiId { get; set; }
    }
}
