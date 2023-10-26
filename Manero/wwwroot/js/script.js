

//Image Upload Selection of Main Image
document.addEventListener("DOMContentLoaded", function () {
    console.log('DOM fully loaded and parsed');

    const inputElement = document.querySelector('input[name="Images"]');
    console.log('Input Element:', inputElement);

    if (inputElement) {
        inputElement.addEventListener('change', function (event) {
            // Clear existing previews
            const previewContainer = document.querySelector('.image-preview-container');
            previewContainer.innerHTML = '';

            const files = event.target.files;
            console.log('Files:', files);

            for (let i = 0; i < files.length; i++) {
                const file = files[i];

                const reader = new FileReader();
                reader.addEventListener('load', function (loadEvent) {

                    const imageDiv = document.createElement('div');
                    imageDiv.classList.add('m-3');

                    const imgElement = document.createElement('img');
                    imgElement.src = loadEvent.target.result;
                    imgElement.alt = file.name;
                    imgElement.style.width = '100px';
                    imgElement.classList.add('me-3');

                    const radioButton = document.createElement('input');
                    radioButton.type = 'radio';
                    radioButton.name = 'MainImageFileName';
                    radioButton.value = file.name;
                    if (i === 0) {
                        radioButton.checked = true;
                    }

                    const label = document.createElement('label');
                    label.innerText = ` Set as Main Product Image`;
                    label.classList.add('ms-1');


                    imageDiv.appendChild(imgElement);
                    imageDiv.appendChild(radioButton);
                    imageDiv.appendChild(label);
                    previewContainer.appendChild(imageDiv);
                });

                reader.readAsDataURL(file);
            }
        });
    } else {
        console.error('Input element not found. Check if the selector is correct.');
    }
});
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