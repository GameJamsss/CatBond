using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.VersionControl;

namespace Assets.Scripts.Domain
{
    public class Line
    {
        private readonly string _line;

        public Line(string line)
        {
            _line = line;
        }

        public string GetLine()
        {
            return _line;
        }

    }

    public class EndingLine : Line
    {
        private readonly string _line;
        public EndingLine(string line) : base(line)
        {
            _line = line;
        }
    }

    public class Dialog
    {
        private readonly Queue<Line> _dialogLines;
        public Dialog(Queue<Line> dialogLines)
        {
            _dialogLines = dialogLines;
        }

        public Dialog Copy()
        {
            return new Dialog(new Queue<Line>(_dialogLines));
        }
        public Line Next()
        {
            return _dialogLines.Count > 1 ?
                _dialogLines.Dequeue() :
                _dialogLines.Count == 1 ?
                    new EndingLine(_dialogLines.Dequeue().GetLine()) :
                    new EndingLine("Developers have ate shit and made an empty dialog");
        }
    }
}
