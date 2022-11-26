using System.Threading.Tasks;

namespace Something
{
    public class Controller
    {
        private readonly WorldSpaceGrid _model;

        public Controller(WorldSpaceGrid grid) {
            _model = grid;
        }

        public void Step() {
            _model.Step();
        }

        public void Reset() {
            _model.Reset();
        }
    }
}