// Hands out the classes small and large in the correct order depending on what breakpoint/screen size there is on the current window
// Example on screens smaller than sm breakpoint we get the following repeating pattern "small small large"
// On screens that are from sm to md we get "small small small large"
// On screens bigger than md we get "small small small large large"
// This is to make the category-grid layout work dynamically and adapt

const categoryGridModule = (function() {
    function updateClasses() {
        const categoryGrid = document.querySelector(".category-grid");
        const categoryItems = categoryGrid.querySelectorAll(".category-item");
    
        if (window.matchMedia("(max-width: 575px)").matches) { // For screens smaller than the sm breakpoint
            for (let i = 0; i < categoryItems.length; i++) {
                categoryItems[i].classList.remove("small", "large");
                if (i % 3 === 2) {
                    categoryItems[i].classList.add("large");
                } else {
                    categoryItems[i].classList.add("small");
                }
            }
        } else if (window.matchMedia("(min-width: 576px) and (max-width: 768px)").matches) { // For sm to md breakpoint
            for (let i = 0; i < categoryItems.length; i++) {
                categoryItems[i].classList.remove("small", "large");
                if (i % 4 === 3) {
                    categoryItems[i].classList.add("large");
                } else {
                    categoryItems[i].classList.add("small");
                }
            }
        } else { // For screens larger than md breakpoint
            for (let i = 0; i < categoryItems.length; i++) {
                categoryItems[i].classList.remove("small", "large");
                if (i % 6 === 4 || i % 6 === 5) {
                    categoryItems[i].classList.add("large");
                } else {
                    categoryItems[i].classList.add("small");
                }
            }
        }
    }
    return {
        updateClasses
    }
})();


// Listen for resize events to update classes when breakpoints change
window.addEventListener("resize", categoryGridModule.updateClasses);

export default categoryGridModule;
