using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Scaffold.Screens.Core
{
    public interface IScreenProvider
    {
        public StackedScreen GetScreen<T>() where T : IScreen;

        public StackedScreen GetScreen(Type screenType);
    }
}
