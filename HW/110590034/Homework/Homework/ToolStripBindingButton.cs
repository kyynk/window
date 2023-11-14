using System.Windows.Forms;

namespace Homework
{
    public class ToolStripBindingButton : ToolStripButton, IBindableComponent
    {
        private ControlBindingsCollection _dataBindings;
        private BindingContext _bindingContext;

        public ControlBindingsCollection DataBindings
        {
            get
            {
                if (_dataBindings == null) 
                    _dataBindings = new ControlBindingsCollection(this);
                return _dataBindings;
            }
        }

        public BindingContext BindingContext
        {
            get
            {
                if (_bindingContext == null) 
                    _bindingContext = new BindingContext();
                return _bindingContext;
            }
            set
            { 
                _bindingContext = value;
            }
        }
    }
}
