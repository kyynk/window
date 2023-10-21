﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        double _firstPointX;
        double _firstPointY;
        bool _isPressed = false;
        List<Line> _lines = new List<Line>();
        Line _hint = new Line();

        // pointer pressed
        public void PointerPressed(double x, double y)
        {
            if (x > 0 && y > 0)
            {
                _firstPointX = x;
                _firstPointY = y;
                _hint.x1 = _firstPointX;
                _hint.y1 = _firstPointY;
                _isPressed = true;
            }
        }

        // pointer moved
        public void PointerMoved(double x, double y)
        {
            if (_isPressed)
            {
                _hint.x2 = x;
                _hint.y2 = y;
                NotifyModelChanged();
            }
        }

        // pointer released
        public void PointerReleased(double x, double y)
        {
            if (_isPressed)
            {
                _isPressed = false;
                Line hint = new Line();
                hint.x1 = _firstPointX;
                hint.y1 = _firstPointY;
                hint.x2 = x;
                hint.y2 = y;
                _lines.Add(hint);
                NotifyModelChanged();
            }
        }

        // clear
        public void Clear()
        {
            _isPressed = false;
            _lines.Clear();
            NotifyModelChanged();
        }
        
        // draw
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Line aLine in _lines)
                aLine.Draw(graphics);
            if (_isPressed)
                _hint.Draw(graphics);
        }

        // notify model changed
        void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }
    }
}