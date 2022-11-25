using System.Threading.Tasks;

namespace Something
{
    public class Controller
    {
        private readonly WorldSpaceGrid _model;
        private int _stepInterval = 300; //ms

        public Controller(WorldSpaceGrid grid) {
            _model = grid;
            _model.Step();
        }

        public void Start() {
            if (_running || !_stopped)
                return;
            _stopped = true;
            BackgroundTask();
        }

        public void Stop() {
            _stopped = true;
        }

        public void Step() {
            _model.Step();
            // TODO update view
        }

        public void Reset() {
            _stopped = true;
            _model.Reset();
        }

        private bool _stopped = true;
        private bool _running = false;
        private async Task BackgroundTask() {
            _running = true;
            while(!_stopped){
                _model.Step();
                // TODO update view
                await Task.Delay(_stepInterval);
            }
            _running = false;
        }
    }
}