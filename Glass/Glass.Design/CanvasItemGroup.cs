using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Design.Interfaces;

namespace Glass.Design
{
    public class CanvasItemGroup : ChildrenExpandableCanvasItem
    {        
        public CanvasItemGroup(IList<ICanvasItem> children) : base(children)
        {            
        }
    }
}