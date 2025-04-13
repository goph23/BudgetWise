// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Initialize all components
document.addEventListener('DOMContentLoaded', function () {
    
    // Bootstrap initialization for navbar toggler
    var navbarToggler = document.getElementById('navbar-toggler');
    var navbarCollapse = document.getElementById('navbarNav');
    
    if (navbarToggler && navbarCollapse) {
        var bsCollapse = new bootstrap.Collapse(navbarCollapse, {
            toggle: false
        });
        
        navbarToggler.addEventListener('click', function() {
            bsCollapse.toggle();
        });
    }
    
    // Sidebar functionality
    var sidebar = document.getElementById('sidebar');
    var sidebarToggler = document.getElementById('sidebar-toggler');
    var desktopSidebarToggler = document.getElementById('desktop-sidebar-toggler');
    
    // Ensure sidebar is collapsed initially on mobile
    if (window.innerWidth <= 768 && sidebar && sidebar.ej2_instances && sidebar.ej2_instances[0]) {
        var sidebarObj = sidebar.ej2_instances[0];
        sidebarObj.hide();
        sidebar.classList.add('e-close');
        sidebar.style.transform = 'translateX(-100%)';
        sidebar.style.left = '-100%';
        sidebar.style.visibility = 'hidden';
    }
    
    // Function to toggle sidebar
    function toggleSidebar(e) {
        if (e) e.stopPropagation();
        
        if (sidebar && sidebar.ej2_instances && sidebar.ej2_instances[0]) {
            var sidebarObj = sidebar.ej2_instances[0];
            var isMobile = window.innerWidth <= 768;
            var transformValue = isMobile ? '-100%' : '0px';
            
            if (sidebarObj.isOpen) {
                // Mark sidebar as transitioning
                sidebar.classList.add('transitioning');
                
                // Remove the class which triggers the CSS transition for main content
                document.body.classList.remove('sidebar-open');
                
                // First animate the sidebar out
                sidebar.style.transform = 'translateX(' + transformValue + ')';
                
                // Then hide the sidebar with a delay that matches CSS transition timing
                setTimeout(function() {
                    sidebarObj.hide();
                    sidebar.classList.add('e-close');
                    sidebar.style.left = transformValue;
                    
                    // For mobile, hide completely when closed
                    if (isMobile) {
                        sidebar.style.visibility = 'hidden';
                    }
                    
                    sidebar.classList.remove('transitioning');
                }, 300); // Match the CSS transition duration
            } else {
                // Mark sidebar as transitioning
                sidebar.classList.add('transitioning');
                
                // First make the sidebar visible but off-screen
                sidebarObj.show();
                sidebar.classList.remove('e-close');
                sidebar.style.left = '0';
                sidebar.style.transform = 'translateX(' + transformValue + ')';
                
                // Set visibility and width for mobile
                if (isMobile) {
                    sidebar.style.width = '100%';
                    sidebar.style.visibility = 'visible';
                    
                    // Ensure it's positioned correctly on mobile
                    sidebar.style.position = 'fixed';
                    sidebar.style.zIndex = '1029';
                }
                
                // Force browser to recognize the element before animating
                setTimeout(function() {
                    // Then animate it in
                    sidebar.style.transform = 'translateX(0)';
                    
                    // Add class to trigger main content transitions
                    document.body.classList.add('sidebar-open');
                    
                    // Remove transitioning class when animation completes
                    setTimeout(function() {
                        sidebar.classList.remove('transitioning');
                        
                        // Make sure mobile sidebar is fully visible
                        if (isMobile) {
                            sidebar.style.visibility = 'visible';
                        }
                    }, 300);
                }, 10);
            }
        }
    }
    
    // Add click event to sidebar togglers
    if (sidebarToggler) {
        sidebarToggler.addEventListener('click', toggleSidebar);
    }
    
    if (desktopSidebarToggler) {
        desktopSidebarToggler.addEventListener('click', toggleSidebar);
    }
    
    // Backdrop functionality disabled
    var sidebarBackdrop = document.querySelector('.sidebar-backdrop');
    
    // Close sidebar when clicking outside on mobile
    document.addEventListener('click', function(e) {
        if (window.innerWidth <= 768 &&
            sidebar &&
            sidebar.ej2_instances &&
            sidebar.ej2_instances[0] &&
            sidebar.ej2_instances[0].isOpen &&
            !sidebar.contains(e.target) &&
            sidebarToggler !== e.target && 
            !sidebarToggler?.contains(e.target) &&
            desktopSidebarToggler !== e.target && 
            !desktopSidebarToggler?.contains(e.target)) {
            
            toggleSidebar();
        }
    });
});