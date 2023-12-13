﻿using Homework.Model;
using System;

namespace Homework.Command
{
    public class DrawCommand : ICommand
    {
        Shape _shape;
        Model.Model _model;
        int _shapeIndex;
        double _panelWidth;

        public DrawCommand(Model.Model model, Shape shape, int index)
        {
            _shape = shape;
            _model = model;
            _shapeIndex = index;
            _panelWidth = -1;
        }

        // execute
        public void Execute(double width)
        {
            AdjustWithPanelWidth(width);
            _model.InsertShape(_shape, _shapeIndex);
        }

        // unexcute
        public void Undo(double width)
        {
            SetPanelWidth(width);
            _model.DeleteShape(_shapeIndex);
        }

        // store panel width
        public void SetPanelWidth(double width)
        {
            _panelWidth = width;
            //Console.WriteLine("Store");
            //Console.WriteLine("width" + width + " panel width" + _panelWidth);
        }

        // adjust panel width
        public void AdjustWithPanelWidth(double width)
        {
            //Console.WriteLine("Adjust");
            //Console.WriteLine("width" + width + " panel width" + _panelWidth);
            double ratio = width / _panelWidth;
            _shape.ResizeForPanel(ratio);
            SetPanelWidth(width);
        }
    }
}