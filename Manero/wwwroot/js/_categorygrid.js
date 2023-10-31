// Hands out the classes small and large in the correct order depending on what breakpoint/screen size there is on the current window, examples:
// On screens smaller than sm breakpoint we get the following repeating pattern "small small large"
// On screens that are from sm to md we get "small small small large"
// On screens bigger than md we get "small small small small large large"
// This is to make the category-grid layout work dynamically and adapt
// It also hands out the correct .container or ._container class to .category-container element depending on if the screen is md or lower or not

const categoryGridModule = (function() {
    function updateCategoryGridClasses() {
        // Check if the categoryGrid element exists to only run the script when the partial is visible
        const categoryGrid = document.querySelector(".category-grid");
        if (!categoryGrid) {
            return;
        }

        const categoryContainer = categoryGrid.querySelector(".category-container")
        const categoryItems = categoryGrid.querySelectorAll(".category-item");
    
        if (window.matchMedia("(max-width: 575px)").matches) { // For screens smaller than the sm breakpoint

            // Checks if the category-container has the .container or ._container class and adjust accordingly
            if (categoryContainer.classList.contains("container")) {
                categoryContainer.classList.remove("container");
            }
            if (!categoryContainer.classList.contains("_container")){
                categoryContainer.classList.add("_container")
            }

            // Checks if the category-item has the correct layout of small and large boxes and adjusts accordingly
            if (!categoryItems[2].classList.contains("large")) {
                for (let i = 0; i < categoryItems.length; i++) {
                    categoryItems[i].classList.remove("small", "large");
                    if (i % 3 === 2) {
                        categoryItems[i].classList.add("large");
                    } else {
                        categoryItems[i].classList.add("small");
                    }
                }
            }
        } else if (window.matchMedia("(min-width: 576px) and (max-width: 767px)").matches) { // For sm to md breakpoint

            // Checks if the category-container has the .container or ._container class and adjust accordingly
            if (categoryContainer.classList.contains("container")) {
                categoryContainer.classList.remove("container");
            }
            if (!categoryContainer.classList.contains("_container")){
                categoryContainer.classList.add("_container")
            }

            // Checks if the category-item has the correct layout of small and large boxes and adjusts accordingly
            if (!categoryItems[3].classList.contains("large")) {
                for (let i = 0; i < categoryItems.length; i++) {
                    categoryItems[i].classList.remove("small", "large");
                    if (i % 4 === 3) {
                        categoryItems[i].classList.add("large");
                    } else {
                        categoryItems[i].classList.add("small");
                    }
                }
            }
        } else if (window.matchMedia("(min-width: 768px) and (max-width: 991px)").matches) { // For md to lg breakpoint

            // Checks if the category-container has the .container or ._container class and adjust accordingly
            if (categoryContainer.classList.contains("container")) {
                categoryContainer.classList.remove("container");
            }
            if (!categoryContainer.classList.contains("_container")){
                categoryContainer.classList.add("_container")
            }

            // Checks if the category-item has the correct layout of small and large boxes and adjusts accordingly
            if (!categoryItems[4].classList.contains("large")) {
                for (let i = 0; i < categoryItems.length; i++) {
                    categoryItems[i].classList.remove("small", "large");
                    if (i % 6 === 4 || i % 6 === 5) {
                        categoryItems[i].classList.add("large");
                    } else {
                        categoryItems[i].classList.add("small");
                    }
                }
            }
        } else { // For screens larger than lg breakpoint

            // Checks if the category-container has the .container or ._container class and adjust accordingly
            if (categoryContainer.classList.contains("_container")) {
                categoryContainer.classList.remove("_container");
            }
            if (!categoryContainer.classList.contains("container")){
                categoryContainer.classList.add("container")
            }

            // Checks if the category-item has the correct layout of small and large boxes and adjusts accordingly
            if (!categoryItems[4].classList.contains("large")) {
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
    }
    return {
        updateCategoryGridClasses
    }
})();

function categoryGridResizeListener() {
    // Check if the categoryGrid element exists to only run the script when the partial is visible
    const categoryGrid = document.querySelector(".category-grid");
    if (categoryGrid) {
        categoryGridModule.updateCategoryGridClasses();

        // Listen for resize events to update classes when breakpoints change
        window.addEventListener("resize", categoryGridModule.updateCategoryGridClasses);
    }
}


// Call the categoryGridResizeListener function when the page loads
window.addEventListener("DOMContentLoaded", categoryGridResizeListener);

export default categoryGridModule;
