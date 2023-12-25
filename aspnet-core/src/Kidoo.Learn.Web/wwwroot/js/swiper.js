
var mySwiper = new Swiper ('.slider-two', {
    speed: 400,
    initialSlide: 2,
    //truewrapper adoptsheight of active slide
    autoHeight: false,
    // Optional parameters
    direction: 'horizontal',
    loop: true,
    // delay between transitions in ms
    autoplay: 5000,
    autoplayStopOnLast: false, // loop false also
    // If we need pagination
    pagination: '.swiper-pagination',
    paginationType: "fraction",
    
    // Navigation arrows
    nextButton: '.swiper-button-next',
    prevButton: '.swiper-button-prev',
    
    // And if we need scrollbar
    //scrollbar: '.swiper-scrollbar',
    // "slide", "fade", "cube", "coverflow" or "flip"
    effect: 'slide',
    // Distance between slides in px.
    spaceBetween:30,
    slidesPerView: 4,
    //
    breakpoints: {  
      '420': {
        slidesPerView: 1,
        spaceBetween: 15,},
        '567': {
          slidesPerView: 1.5,
          spaceBetween: 30,},
          '991': {
            slidesPerView: 2,
            spaceBetween: 30,},
        '1199': {
          slidesPerView: 3.5,
          spaceBetween: 30, }
     },
    centeredSlides: true,
    // //
   slidesOffsetBefore: 0,
    //
    grabCursor: true
  })