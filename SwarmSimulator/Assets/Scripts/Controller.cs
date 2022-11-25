namespace Something
{
    public class Controller
    {
        Model model;
        int stepInterval = 300; //ms

        public Controller(){
            model = new Model(4,5,6);
            model.step();
        }

        public void start(){
            if (running || !stopped)
                return;
            stopped = true;
            backgroundTask();
        }

        public void stop(){
            stopped = true;
        }

        public void step(){
            model.step();
            // TODO update view
        }

        public void reset(){
            stopped = true;
            model.reset();
        }

        private bool stopped = true;
        private bool running = false;
        private async Task backgroundTask(){
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