using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Glass.Design.Pcl.Canvas;
using PostSharp.Patterns.Recording;
using PostSharp.Patterns.Recording.Operations;

namespace Glass.Design.WpfTester
{
    class MyOperationFormatter : OperationFormatter
    {
        public MyOperationFormatter( OperationFormatter next ) : base( next )
        {
        }

        protected override string FormatOperationDescriptor( IOperationDescriptor operation )
        {
            if ( operation.OperationKind == OperationKind.Method )
            {
                MethodExecutionOperationDescriptor descriptor = (MethodExecutionOperationDescriptor) operation;
                if ( descriptor.Method != null &&
                     (descriptor.Method.Attributes & MethodAttributes.SpecialName) != 0 &&
                     descriptor.Method.Name.StartsWith( "set_" ) )
                {
                    ICanvasItem canvasItem = (ICanvasItem) descriptor.Target;
                    return string.Format( "Changing {0} of {1}", descriptor.Method.Name.Substring( 4 ), canvasItem.GetName() );
                }
            }

            return null;
        }
    }
}
