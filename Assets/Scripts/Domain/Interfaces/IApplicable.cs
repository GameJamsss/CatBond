﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Domain.Items
{ //DELETE
    public interface IApplicable
    {
        public bool Apply(int objId);
    }
}
