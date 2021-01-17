(function ( $ ) {


    // Function for Bootstrap Nav */
    $.fn.bootstrapNav = function (options) {
        var o = $.extend({
            dropdownToggle: true,
            breakpoint: 1199,

            self: this,
            containers: $('.collapse.navbar-collapse ul:not(li ul)', this),
            listItem: $('li:has(ul li)', this),
            dropDownAnchor: '.dropdown>a',
            childContainer: $('ul:first-child li ul ', this),
            childContainerItem: $('ul:first-child li ul li ', this)


        }, options);


        var getNavbar = $(o.self).find(o.containers);

        getNavbar.each(function () {
            $(this).addClass('nav navbar-nav');

            // Add dropdown class and data toggles
            if ($(this).has('li ul')) {

                $(o.listItem).each(function () {
                    $(this).addClass('dropdown');
                    $('ul', this).addClass('dropdown-menu').attr('role', 'menu');

                    // Add Dropdown
                    $('li.dropdown>a').addClass('dropdown-toggle');
                    // Add the Carret.
                    $('a.dropdown-toggle', this).append('&nbsp;<span class="caret"></span>')


                });
            }
        });

        if (o.dropdownToggle == false && $(window).width() > o.breakpoint) {
            $(document).on({
                mouseenter: function () {
                    $('ul.dropdown-menu', this).css('display', 'block');
                },
                mouseleave: function () {
                    $('ul.dropdown-menu', this).removeAttr('style');
                }
            }, "li.dropdown"); //pass the element as an argument to .on
        } else {
            $(document).off('mouseenter',"li.dropdown"); //pass the element as an argument to .on
            $(document).off('mouseleave',"li.dropdown"); //pass the element as an argument to .on
            
            $('a.dropdown-toggle').each(function(){
                $(this).attr('data-toggle', 'dropdown'); 
            });
        }
        $(window).resize(function() {
            if (o.dropdownToggle == false && $(window).width() > o.breakpoint) {
                $(document).on({
                    mouseenter: function () {
                        $('ul.dropdown-menu', this).css('display', 'block');
                    },
                    mouseleave: function () {
                        $('ul.dropdown-menu', this).removeAttr('style');
                    }
                }, "li.dropdown"); //pass the element as an argument to .on
            } else {
                $(document).off('mouseenter',"li.dropdown"); //pass the element as an argument to .on
                $(document).off('mouseleave',"li.dropdown"); //pass the element as an argument to .on
                $('a.dropdown-toggle').each(function(){
                    $(this).attr('data-toggle', 'dropdown'); 
                });
            }
        });
        
        var url_path = document.location.pathname;
        var navbarItemsClicked = $(getNavbar).find('li');

        navbarItemsClicked.each(function() {
            if ($('a', this).attr('href') == url_path) {
                //alert('true');
                $(this).addClass('active');
                $(this).closest('li.dropdown').addClass('active');

            }

        });


    }

}( jQuery ));
/*
// Add this code below inside your html site to activate the plugin.

$('#top_navbar').bootstrapNav({
    // Dropdown Choice: Toggle(Bootstrap Default) = true, Hover Dropdown = false.
    dropdownToggle: true,
    // At what point should navbar go back to Toggle for Mobile Devices.
    breakpoint: 1199
});
*/