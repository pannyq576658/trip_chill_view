class loading {
    constructor() {
        this.loadingTask = 0;
        this.loadAnimationTimer = null;        
    }

    Startup() {
        this.loadingTask++;
        $('body').loading({
            theme: 'dark'
        })
        $('.loading-overlay-content').attr('style', ' font-size: 1.4em;')
        var load_dot = ''
        this.loadAnimationTimer = setInterval(function () {

            $('.loading-overlay-content').text('loading' + load_dot)
            load_dot += '.'
            if (load_dot == '....')
                load_dot = ''
        }, 500);
    }
    Stop() {
        $('body').loading("stop")
        clearTimeout(this.loadAnimationTimer);
    }
    TaskAdd() {
        this.loadingTask++;
    }
    TaskSub() {
        this.loadingTask--;
        if (this.loadingTask == 0)
            this.Stop()
    }
}