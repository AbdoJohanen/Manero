window.addEventListener('load', function () {
    const hasVisitedBefore = localStorage.getItem('hasVisitedBefore');

    if (!hasVisitedBefore) {
        setTimeout(function () {
            const loadingScreen = document.querySelector('.loading-screen');
            loadingScreen.style.opacity = 0;

            const welcome = document.querySelector('.welcome');
            welcome.style.opacity = 1;

            const content = document.querySelector('.content');
            content.setAttribute('hidden', "");


            // Hide the loading screen after the transition
            setTimeout(function () {
                loadingScreen.style.display = 'none';
                welcome.style.display = 'block';
            }, 1000); // 1000 milliseconds (1 second)
        }, 3000); // Delay the transition for a smoother effect

        localStorage.setItem('hasVisitedBefore', true);

    } else {
        const loadingScreen = document.querySelector('.loading-screen');
        loadingScreen.setAttribute('hidden', "");

        const welcome = document.querySelector('.welcome');
        welcome.setAttribute('hidden', "");

        const content = document.querySelector('.content');
        content.removeAttribute('hidden', "");
    }
});