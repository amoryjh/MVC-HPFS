$('document').ready(function() { 
  $('.icon').click(function(){
    var icon = $('i.icon');
    if($(this).hasClass('closed')){
      $('.admin-aside-panel').animate({
        marginLeft: '0px'
      }, 500);
      $(this).removeClass('closed');
      $(this).addClass('open');
      icon.addClass('fa-chevron-left');
      icon.removeClass('fa-chevron-right');   
    }
    else{
      $('.admin-aside-panel').animate({
        marginLeft: '-285px'
      }, 500);
      
      $(this).removeClass('open');
      $(this).addClass('closed');
      icon.addClass('fa-chevron-right');
      icon.removeClass('fa-chevron-left');    
    }
  });
});