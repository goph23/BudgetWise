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
    
    // Function to toggle sidebar
    function toggleSidebar(e) {
        if (e) e.stopPropagation();
        
        if (sidebar && sidebar.ej2_instances && sidebar.ej2_instances[0]) {
            var sidebarObj = sidebar.ej2_instances[0];
            
            if (sidebarObj.isOpen) {
                sidebarObj.hide();
                document.body.classList.remove('sidebar-open');
            } else {
                sidebarObj.show();
                document.body.classList.add('sidebar-open');
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