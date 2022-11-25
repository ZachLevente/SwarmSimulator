using System.Threading.Tasks;

namespace Something
{
    public class Controller
    {
        WorldSpaceGrid model;
        int stepInterval = 300; //ms

        public Controller(WorldSpaceGrid grid) {
            model = grid;
            model.step();
        }

        public void start() {
            if (running || !stopped)
                return;
            stopped = true;
            backgroundTask();
        }

        public void stop() {
            stopped = true;
        }

        public void step() {
            model.step();
            // TODO update view
        }

        public void reset() {
            stopped = true;
            model.reset();
        }

        private bool stopped = true;
        private bool running = false;
        private async Task backgroundTask() {
            running = true;
            while(!stopped){
                model.step();
                // TODO update view
                await Task.Delay(stepInterval);
            }
            running = false;
        }
    }
}