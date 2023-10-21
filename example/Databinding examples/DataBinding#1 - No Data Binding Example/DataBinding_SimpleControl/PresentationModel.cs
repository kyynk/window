using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataBinding_SimpleControl
{
    public class PresentationModel
    {
        bool isAddButtonEnabled = false;
        bool isEditMode = false;

        public void categoryTextBoxTextChanged(string text)
        {
            isAddButtonEnabled = text != "";
        }

        public void clickEditButton()
        {
            isEditMode = !isEditMode;
        }

        public bool IsAddButtonEnabled
        {
            get
            {
                return isAddButtonEnabled;
            }
        }

        public string AddButtonText
        {
            get
            {
                if (isEditMode)
                    return "修改";
                return "新增";
            }
        }
    }
}
