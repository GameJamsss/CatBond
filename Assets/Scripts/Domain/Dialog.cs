using System.Collections.Generic;
using System.Linq;

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
        public static Dialog build(params string[] quotes)
        {
            Queue<Line> q = new Queue<Line>();
            quotes.ToList().Select(s => new Line(s)).ToList().ForEach(line => q.Enqueue(line));
            return new Dialog(q);
        }

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
