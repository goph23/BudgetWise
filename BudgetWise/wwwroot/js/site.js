// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Initialize all components
// Add desktop-view class to html element if screen is desktop
function updateViewportClass() {
    var htmlElement = document.documentElement;
    if (window.innerWidth > 768) {
        htmlElement.classList.add('desktop-view');
    } else {
        htmlElement.classList.remove('desktop-view');
    }
}

// Run on page load and when the window is resized
window.addEventListener('resize', updateViewportClass);
updateViewportClass(); // Run immediately

// Function to initialize breadcrumbs
function initializeBreadcrumbs() {
    const breadcrumbElement = document.getElementById('breadcrumb');
    if (!breadcrumbElement || !breadcrumbElement.ej2_instances || !breadcrumbElement.ej2_instances[0]) return;
    
    const breadcrumbInstance = breadcrumbElement.ej2_instances[0];
    
    // Get current path information
    const path = window.location.pathname;
    const pathSegments = path.split('/').filter(segment => segment !== '');
    
    // Create breadcrumb items
    const breadcrumbItems = [];
    
    // Always add home as first item with just an icon
    breadcrumbItems.push({
        text: '',
        url: '/',
        iconCss: 'fa-solid fa-home'
    });
    
    // Build breadcrumb based on path segments
    let currentPath = '';
    
    for (let i = 0; i < pathSegments.length; i++) {
        const segment = pathSegments[i];
        currentPath += '/' + segment;
        
        // Skip segments that represent IDs (numbers) or actions
        if (!isNaN(segment)) continue;
        
        // Skip the "Home" controller in the breadcrumb since we already have the home icon
        if (segment.toLowerCase() === 'home') continue;
        
        // Format the display text
        let displayText = segment.charAt(0).toUpperCase() + segment.slice(1);
        
        // Handle special cases with mobile-friendly shorter text
        if (segment.toLowerCase() === 'category') displayText = 'Categories';
        if (segment.toLowerCase() === 'transaction') displayText = 'Transactions';
        if (segment.toLowerCase() === 'addoredit' && i > 0) {
            const parentSegment = pathSegments[i-1].toLowerCase();
            if (parentSegment === 'category') {
                displayText = window.innerWidth <= 576 ? 'Edit' : 'Manage Category';
            } else if (parentSegment === 'transaction') {
                // Ensure "Manage Transaction" fits properly on all screen sizes
                displayText = window.innerWidth <= 576 ? 'Edit' : 
                            (window.innerWidth <= 992 ? 'Edit Transaction' : 'Manage Transaction');
            }
        }
        
        // Add the item
        if (i === pathSegments.length - 1) {
            // Last item doesn't have a URL
            breadcrumbItems.push({
                text: displayText
            });
        } else {
            breadcrumbItems.push({
                text: displayText,
                url: currentPath
            });
        }
    }
    
    // Set the breadcrumb items
    breadcrumbInstance.items = breadcrumbItems;
}

// Update breadcrumbs when window resizes
window.addEventListener('resize', function() {
    // Only reinitialize if we cross the mobile breakpoint
    initializeBreadcrumbs();
});

document.addEventListener('DOMContentLoaded', function () {
    // Initialize breadcrumbs
    initializeBreadcrumbs();
    
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
    
    // Initialize sidebar based on screen size
    if (sidebar && sidebar.ej2_instances && sidebar.ej2_instances[0]) {
        var sidebarObj = sidebar.ej2_instances[0];
        
        if (window.innerWidth <= 768) {
            // Mobile: keep sidebar closed regardless of class
            sidebarObj.hide();
            sidebar.classList.add('e-close');
            sidebar.style.transform = 'translateX(-100%)';
            sidebar.style.left = '-100%';
            sidebar.style.visibility = 'hidden';
            
            // Remove desktop open classes on mobile
            document.body.classList.remove('sidebar-open-desktop');
            document.documentElement.classList.remove('sidebar-open-desktop');
        } else {
            // Check if we should show the sidebar (based on classes)
            var shouldShowSidebar = document.body.classList.contains('sidebar-open-desktop') || 
                                   document.documentElement.classList.contains('sidebar-open-desktop');
            
            if (shouldShowSidebar) {
                // Desktop: set sidebar open by default without animation
                sidebar.style.transition = 'none'; // Disable transitions temporarily
                document.body.style.transition = 'none'; // Disable transitions for body too
                
                // Show sidebar
                sidebarObj.show();
                sidebar.classList.remove('e-close');
                sidebar.classList.add('e-open');
                sidebar.style.transform = 'translateX(0)';
                sidebar.style.left = '0';
                sidebar.style.visibility = 'visible';
                
                // Re-enable transitions after a short delay
                setTimeout(function() {
                    sidebar.style.transition = 'transform 300ms cubic-bezier(0.4, 0, 0.2, 1)';
                    document.body.style.transition = 'all 300ms cubic-bezier(0.4, 0, 0.2, 1)';
                }, 100);
            }
        }
    }
    
    // Function to toggle sidebar
    function toggleSidebar(e) {
        if (e) e.stopPropagation();
        
        if (sidebar && sidebar.ej2_instances && sidebar.ej2_instances[0]) {
            var sidebarObj = sidebar.ej2_instances[0];
            var isMobile = window.innerWidth <= 768;
            var htmlElement = document.documentElement;
            
            // Add a class to enable transitions only during manual toggling
            document.body.classList.add('sidebar-toggling');
            htmlElement.classList.add('sidebar-toggling');
            
            if (sidebarObj.isOpen) {
                // Close sidebar
                document.body.classList.remove('sidebar-open');
                document.body.classList.remove('sidebar-open-desktop');
                htmlElement.classList.remove('sidebar-open');
                htmlElement.classList.remove('sidebar-open-desktop');
                
                if (isMobile) {
                    sidebar.style.transform = 'translateX(-100%)';
                    
                    setTimeout(function() {
                        sidebarObj.hide();
                        sidebar.classList.add('e-close');
                        sidebar.style.left = '-100%';
                        sidebar.style.visibility = 'hidden';
                    }, 300);
                } else {
                    sidebar.style.transform = 'translateX(-250px)';
                    
                    setTimeout(function() {
                        sidebarObj.hide();
                        sidebar.classList.add('e-close');
                        sidebar.style.left = '-250px';
                    }, 300);
                }
            } else {
                // Open sidebar
                sidebarObj.show();
                sidebar.classList.remove('e-close');
                sidebar.classList.add('e-open');
                sidebar.style.left = '0';
                sidebar.style.visibility = 'visible';
                
                if (isMobile) {
                    sidebar.style.width = '100%';
                    sidebar.style.position = 'fixed';
                    sidebar.style.zIndex = '1029';
                    
                    setTimeout(function() {
                        sidebar.style.transform = 'translateX(0)';
                    }, 10);
                } else {
                    setTimeout(function() {
                        sidebar.style.transform = 'translateX(0)';
                    }, 10);
                }
                
                // Add the open class to both body and html
                document.body.classList.add('sidebar-open');
                htmlElement.classList.add('sidebar-open');
                
                // Also add desktop class if we're on desktop
                if (!isMobile) {
                    document.body.classList.add('sidebar-open-desktop');
                    htmlElement.classList.add('sidebar-open-desktop');
                }
            }
            
            // Remove toggling class after transition completes
            setTimeout(function() {
                document.body.classList.remove('sidebar-toggling');
                htmlElement.classList.remove('sidebar-toggling');
            }, 350);
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