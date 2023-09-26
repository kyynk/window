using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    class Model
    {
        private const double ZERO = 0.0;
        private const string POINT = ".";
        private const string PLUS = "+";
        private const string MINUS = "-";
        private const string MULTIPLE = "*";
        private const string DIVISION = "/";
        private double _memory;
        private string _buffer;
        private string _operation;
        private bool _isNotEqual;
        private bool _isNewNumber;

        public Model(double memory, string buffer, string operation, bool initialBoolean)
        {
            _memory = memory;
            _buffer = buffer;
            _operation = operation;
            _isNotEqual = initialBoolean;
            _isNewNumber = initialBoolean;

        }

        // clear _memory _buffer _operation
        public void Clear()
        {
            _memory = ZERO;
            _buffer = ZERO.ToString();
            _operation = PLUS;
            _isNewNumber = true;
        }

        // clear _buffer
        public void ClearEntry()
        {
            _buffer = ZERO.ToString();
        }

        // add number to buffer
        // if add number after equal and not set operator
        // will Clear() first then add number
        public void AddNumber(string number)
        {
            if (!_isNotEqual)
            {
                Clear();
            }
            if (_buffer == ZERO.ToString())
            {
                _buffer = number;
            }
            else
            {
                _buffer += number;
            }
            _isNewNumber = false;
        }

        // add point to buffer
        public void AddPoint()
        {
            if (!_buffer.Contains(POINT))
            {
                _buffer += POINT;
            }
            _isNewNumber = false;
        }

        // get buffer
        public string GetBuffer()
        {
            return _buffer;
        }

        // get memory
        public double GetMemory()
        {
            return _memory;
        }
        
        // memory add buffer
        public void Add()
        {
            _memory += Convert.ToDouble(_buffer);
        }
        
        // memory minus buffer
        public void Subtract()
        {
            _memory -= Convert.ToDouble(_buffer);
        }
        
        // memory multiply buffer
        public void Multiply()
        {
            _memory *= Convert.ToDouble(_buffer);
        }
        
        // memory divide buffer
        public void Divide()
        {
            if (_buffer != ZERO.ToString())
            {
                _memory /= Convert.ToDouble(_buffer);
            }
        }
        
        // perform operation with +-*/
        public void PerformOperations()
        {
            if (_operation == PLUS)
            {
                Add();
            }
            else if (_operation == MINUS)
            {
                Subtract();
            }
            else if (_operation == MULTIPLE)
            {
                Multiply();
            }
            else if (_operation == DIVISION)
            {
                Divide();
            }
            _isNewNumber = true;
        }

        // set operation (+-*/)
        public void SetOperation(string operation)
        {
            if (!_isNewNumber)
            {
                PerformOperations();
            }
            ClearEntry();
            _isNotEqual = true;
            _operation = operation;
        }

        // equal (use former operator and buffer)
        public void Equal()
        {
            PerformOperations();
            _isNotEqual = false;
        }
    }
}