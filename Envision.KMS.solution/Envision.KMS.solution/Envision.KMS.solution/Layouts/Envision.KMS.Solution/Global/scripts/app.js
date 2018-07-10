﻿
var App = function () {
	// We extend jQuery by method hasAttr
	$.fn.hasAttr = function(name) {
	  return this.attr(name) !== undefined;
	};

	// Fixed Header
	function handleHeader() {
		jQuery(window).scroll(function() {
		  if (jQuery(window).scrollTop() > 100) {
			jQuery('.header-fixed .header-sticky').addClass('header-fixed-shrink');
		  } else {
			jQuery('.header-fixed .header-sticky').removeClass('header-fixed-shrink');
		  }
		});
	}

	var handleFullscreen = function () {
	    var WindowHeight = $(window).height();
	    HeaderHeight = $("#nav").height();

	    $(".project-frame-main").css("height", WindowHeight - HeaderHeight);
	    $(".focuswin-content").css("height", WindowHeight - HeaderHeight);

	    $(window).resize(function () {
	        var WindowHeight = $(window).height();
	        $(".project-frame-main").css("height", WindowHeight - HeaderHeight);
	        $(".focuswin-content").css("height", WindowHeight - HeaderHeight);

	    });
	}

	// Header Mega Menu
	function handleMegaMenu() {
		jQuery(document).on('click', '.mega-menu .dropdown-menu', function(e) {
			e.stopPropagation();
		})
	}

	// Search Box (Header)
	function handleSearch() {
		jQuery('.search').on("click", function () {
			if(jQuery('.search-btn').hasClass('fa-search')){
				jQuery('.search-open').fadeIn(500);
				jQuery('.search-btn').removeClass('fa-search');
				jQuery('.search-btn').addClass('fa-times');
			} else {
				jQuery('.search-open').fadeOut(500);
				jQuery('.search-btn').addClass('fa-search');
				jQuery('.search-btn').removeClass('fa-times');
			}
		});
	}

	// Equal Height Columns
	function handleEqualHeightColumns() {
		var EqualHeightColumns = function () {
			$(".equal-height-columns").each(function() {
				heights = [];
				$(".equal-height-column", this).each(function() {
					$(this).removeAttr("style");
					heights.push($(this).height()); // write column's heights to the array
				});
				$(".equal-height-column", this).height(Math.max.apply(Math, heights)); //find and set max
			});
		}

		EqualHeightColumns();
		$(window).resize(function() {
			EqualHeightColumns();
		});
		$(window).load(function() {
			EqualHeightColumns();
		});
	}

	// Equal Height Image-Columns
	function handleEqualHeightColumns__Images() {
		var EqualHeightColumns__Images = function () {
			$('.equal-height-columns-v2').each(function() {
				var heights = [];
				$('.equal-height-column-v2', this).each(function() {
					$(this).removeAttr('style');
					heights.push($(this).height()); // Write column's heights to the array
				});
				$('.equal-height-column-v2', this).height(Math.max.apply(Math, heights)); // Find and set max

				$('.equal-height-column-v2', this).each(function() {
					if ($(this).hasAttr('data-image-src')) {
						$(this).css('background', 'url('+$(this).attr('data-image-src')+') no-repeat scroll 50% 0 / cover');
					}
				});
			});
		}
    $('.equal-height-columns-v2').ready(function() {
      EqualHeightColumns__Images();
    });
		$(window).resize(function() {
			EqualHeightColumns__Images();
		});
	}

	// Align Middle
	var handleValignMiddle = function() {
		$(".valign__middle").each(function() {
		  $(this).css("padding-top", $(this).parent().height() / 2 - $(this).height() / 2);
		});
		$(window).resize(function() {
		  $(".valign__middle").each(function() {
			$(this).css("padding-top", $(this).parent().height() / 2 - $(this).height() / 2);
		  });
		});
	}

	// Hover Selector
	function handleHoverSelector() {
		// $('.hoverSelector').on('hover', function(e) {
		// 	$('.hoverSelectorBlock', this).toggleClass('show');
		// 	e.stopPropagation();
		// });
	    $('.hoverSelector').on('click', function(e) {
	      if (jQuery(this).children('ul').hasClass('languages')) {
	        if (jQuery(this).children('ul').hasClass('languages-visible')) {
	          jQuery(this).children('.languages').slideUp();
	          jQuery(this).children('.languages').removeClass('languages-visible');
	        } else {
	          jQuery(this).children('.languages').slideDown();
	          jQuery(this).children('.languages').addClass('languages-visible');
	        }
	      }
	      //e.preventDefault();
	    });
	}

	// Bootstrap Tooltips and Popovers
	function handleBootstrap() {
		/* Bootstrap Carousel */
		jQuery('.carousel').carousel({
			interval: 15000,
			pause: 'hover'
		});

		/* Tooltips */
		jQuery('.tooltips').tooltip();
		jQuery('.tooltips-show').tooltip('show');
		jQuery('.tooltips-hide').tooltip('hide');
		jQuery('.tooltips-toggle').tooltip('toggle');
		jQuery('.tooltips-destroy').tooltip('destroy');

		/* Popovers */
		jQuery('.popovers').popover();
		jQuery('.popovers-show').popover('show');
		jQuery('.popovers-hide').popover('hide');
		jQuery('.popovers-toggle').popover('toggle');
		jQuery('.popovers-destroy').popover('destroy');
	}

	return {
		init: function () {
			handleBootstrap();
			handleHoverSelector();
			handleFullscreen();
			handleValignMiddle();
			handleEqualHeightColumns();
			handleEqualHeightColumns__Images();
		},

		// Counters
		initCounter: function () {
			jQuery('.counter').counterUp({
				delay: 10,
				time: 1000
			});
		},

		// Scroll Bar
		initScrollBar: function () {
			jQuery('.mCustomScrollbar').mCustomScrollbar({
				theme:"minimal",
				scrollInertia: 200,
				scrollEasing: "linear"
			});
		},

		// Sidebar Menu Dropdown
		initSidebarMenuDropdown: function() {
		  function SidebarMenuDropdown() {
			jQuery('.header-v7 .dropdown-toggle').on('click', function() {
			  jQuery('.header-v7 .dropdown-menu').stop(true, false).slideUp();
			  jQuery('.header-v7 .dropdown').removeClass('open');

			  if (jQuery(this).siblings('.dropdown-menu').is(":hidden") == true) {
				jQuery(this).siblings('.dropdown-menu').stop(true, false).slideDown();
				jQuery(this).parents('.dropdown').addClass('open');
			  }
			});
		  }
		  SidebarMenuDropdown();
		},

		// Animate Dropdown
		initAnimateDropdown: function() {
		  function MenuMode() {
			jQuery('.dropdown').on('show.bs.dropdown', function() {
			  jQuery(this).find('.dropdown-menu').first().stop(true, true).slideDown();
			});
			jQuery('.dropdown').on('hide.bs.dropdown', function() {
			  jQuery(this).find('.dropdown-menu').first().stop(true, true).slideUp();
			});
		  }

		  jQuery(window).resize(function() {
			if (jQuery(window).width() > 768) {
			  MenuMode();
			}
		  });

		  if (jQuery(window).width() > 768) {
			MenuMode();
		  }
		},
	};
}();

var silderEntity = function (data,parent) {
    var entity = this;
    entity.Id = ko.observable();
    entity.Title = ko.observable();
    entity.Description = ko.observable();
    entity.Link = ko.observable();
    entity.ImgUrl = ko.observable();
    entity.ClickContact = function () {
        parent.LoadContact(entity.Id());
    }
    if (data)
    {
        entity.Id(data.Id);
        entity.Title(data.Title);
        entity.Description(data.Description);
        entity.Link(data.Link);
        entity.ImgUrl(data.ImgUrl);
    }
}

var contactEntity = function (data) {
    var entity = this;
    entity.Id = ko.observable();
    entity.Title = ko.observable();
    entity.Name = ko.observable();
    entity.Contact = ko.observable();
    entity.Extend1 = ko.observable();
    entity.Extend2 = ko.observable();
    entity.Extend3 = ko.observable();
    if (data) {
        entity.Id(data.Id);
        entity.Title(data.Title);
        entity.Name(data.Name);
        entity.Contact(data.Contact);
        entity.Extend1(data.Extend1);
        entity.Extend2(data.Extend2);
        entity.Extend3(data.Extend3);
    }
}

var sliderMgr = function () {
    var self = this;
    self.DataAttr = ko.observableArray();
    self.ContactAttr = ko.observableArray();
    self.ModelDisplay = ko.observable();
    self.ContactDisplay = ko.observable();
    self.ContactPhoneDisplay = ko.observable();
    self.LinkDisplay = ko.observable();
    self.ContactUsDisplay = ko.observable();
    self.Extend1Text = ko.observable();
    self.Extend2Text = ko.observable();
    self.Extend3Text = ko.observable();
    self.ClickContactUs = function () { }
    self.Load = function () {
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Site Name',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.ModelDisplay(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Contact Us',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.ContactUsDisplay(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Link To',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.LinkDisplay(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Contact',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.ContactDisplay(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Contact Phone',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.ContactPhoneDisplay(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Extend 1',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.Extend1Text(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Extend 2',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.Extend2Text(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Extend 3',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.Extend3Text(d);
        });

        self.LoadContact();

        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Slider/items";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "GlobalSlider",
            Language: SP2013.O365.currentLang()
        })).then(function (d) {
            self.DataAttr.removeAll();
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.DataAttr.push(new silderEntity(item,self));
                });
                var frameHeight = $(".project-frame-main").height();
                var frameWidth = $(".project-frame-main").width();
                RevolutionSlider.initRSfullWidth(frameWidth, frameHeight);
                $.each(self.DataAttr(), function (i, item) {
                    $("#slider" + item.Id()).backstretch(item.ImgUrl());
                });
            }
        });
    }


    self.LoadContact = function (Id) {
        self.ContactAttr.removeAll();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Contact/items";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "GlobalContact",
            Language: SP2013.O365.currentLang(),
            CategoryId:Id
        })).then(function (d) {
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.ContactAttr.push(new contactEntity(item));
                });
                $('#contactModel').modal('show');
            }
        });
    }

}

