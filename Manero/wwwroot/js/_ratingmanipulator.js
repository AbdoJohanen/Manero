function selectMultipleStarsManipulator() {
    const ratingInputs = document.querySelectorAll('.form-rating-input');
    const ratingLabels = document.querySelectorAll('.form-rating-label');

    ratingInputs.forEach((input, index) => {
        input.addEventListener('click', function () {
            if (input.checked) {
                ratingLabels.forEach(label => label.classList.remove('selected'));

                for (let i = 0; i <= index; i++) {
                    ratingLabels[i].classList.add('selected');
                }
            }
        });
    });
}

export { selectMultipleStarsManipulator };