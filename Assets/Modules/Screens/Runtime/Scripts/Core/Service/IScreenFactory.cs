using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scaffold.Screens.Core
{
    public interface IScreenFactory
    {
        public IScreen Create(Type screenType, Transform screenHolder);
    }
}
