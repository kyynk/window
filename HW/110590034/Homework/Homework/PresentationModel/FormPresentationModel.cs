﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Homework
{
    public class FormPresentationModel
    {
        public event Model.ModelChangedEventHandler _modelChanged;
        private readonly Model _model;
        private bool _isLineEnabled;
        private bool _isRectangleEnabled;
        private bool _isEllipseEnabled;
        private bool _isDefaultCursorEnabled;

        public FormPresentationModel(Model model)
        {
            _model = model;
            _isLineEnabled = false;
            _isRectangleEnabled = false;
            _isEllipseEnabled = false;
            _isDefaultCursorEnabled = true;
            _model._modelChanged += HandleModelChanged;
        }

        // get shapes
        public BindingList<Shape> GetShapes()
        {
            return _model.GetShapes();
        }

        // create shape
        public void CreateShape(string shapeType)
        {
            _model.Create(shapeType);
        }

        // remove shape by index
        public void DeleteShape(int index)
        {
            _model.Delete(index);
        }

        // pointer press
        public void PressPointer(double mouseX, double mouseY)
        {
            _model.PressPointer(mouseX, mouseY);
        }

        // pointer move
        public void MovePointer(double mouseX, double mouseY)
        {
            _model.MovePointer(mouseX, mouseY);
        }

        // pointer press
        public void ReleasePointer(double mouseX, double mouseY)
        {
            _model.ReleasePointer(mouseX, mouseY);
            EnableDefaultCursor();
        }

        // handle model change
        public void HandleModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        // draw
        public void Draw(System.Drawing.Graphics graphics)
        {
            // graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用graphics，這樣DoubleBuffer才能正確運作
            // 因此，Adaptor不能重複使用，每次都要重新new
            _model.Draw(new FormGraphicsAdaptor(graphics));
        }

        // draw
        public void DrawOnButton(System.Drawing.Graphics graphics, Size buttonSize, Size canvasSize)
        {
            // graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用graphics，這樣DoubleBuffer才能正確運作
            // 因此，Adaptor不能重複使用，每次都要重新new
            // Calculate scaling factors
            float scaleX = (float)buttonSize.Width / canvasSize.Width;
            float scaleY = (float)buttonSize.Height / canvasSize.Height;
            // Apply scaling to the graphics object
            graphics.ScaleTransform(scaleX, scaleY);

            _model.Draw(new FormGraphicsAdaptor(graphics));
        }

        // line enable
        public void EnableLine()
        {
            _isLineEnabled = true;
            _isRectangleEnabled = false;
            _isEllipseEnabled = false;
            _isDefaultCursorEnabled = false;
            _model.SetMode(Model.Mode.DrawLine);
        }

        // line enable
        public void EnableRectangle()
        {
            _isLineEnabled = false;
            _isRectangleEnabled = true;
            _isEllipseEnabled = false;
            _isDefaultCursorEnabled = false;
            _model.SetMode(Model.Mode.DrawRectangle);
        }

        // line enable
        public void EnableEllipse()
        {
            _isLineEnabled = false;
            _isRectangleEnabled = false;
            _isEllipseEnabled = true;
            _isDefaultCursorEnabled = false;
            _model.SetMode(Model.Mode.DrawEllipse);
        }

        // default cursor enable
        public void EnableDefaultCursor()
        {
            _isLineEnabled = false;
            _isRectangleEnabled = false;
            _isEllipseEnabled = false;
            _isDefaultCursorEnabled = true;
            _model.SetMode(Model.Mode.Pointer);
        }

        // is line enabled
        public bool IsLineEnabled()
        {
            return _isLineEnabled;
        }

        // is rectangle enabled
        public bool IsRectangleEnabled()
        {
            return _isRectangleEnabled;
        }

        // is ellipse enabled
        public bool IsEllipseEnabled()
        {
            return _isEllipseEnabled;
        }

        // is default cursor enabled
        public bool IsDefaultCursorEnabled()
        {
            return _isDefaultCursorEnabled;
        }
    }
}
