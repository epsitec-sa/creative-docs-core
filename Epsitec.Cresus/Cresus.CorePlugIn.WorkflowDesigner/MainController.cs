ÿþ/ / 	 C o p y r i g h t   ©   2 0 1 0 - 2 0 1 1 ,   E P S I T E C   S A ,   C H - 1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
  
 u s i n g   E p s i t e c . C r e s u s . C o r e . E n t i t i e s ;  
  
 u s i n g   E p s i t e c . C r e s u s . C o r e P l u g I n . W o r k f l o w D e s i g n e r . W i d g e t s ;  
  
 u s i n g   S y s t e m . T e x t . R e g u l a r E x p r e s s i o n s ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e P l u g I n . W o r k f l o w D e s i g n e r  
 {  
 	 p u b l i c   s e a l e d   c l a s s   M a i n C o n t r o l l e r  
 	 {  
 	 	 p u b l i c   M a i n C o n t r o l l e r ( C o r e . B u s i n e s s . B u s i n e s s C o n t e x t   b u s i n e s s C o n t e x t ,   W o r k f l o w D e f i n i t i o n E n t i t y   w o r k f l o w E n t i t y )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t   =   b u s i n e s s C o n t e x t ;  
 	 	 	 t h i s . w o r k f l o w E n t i t y   =   w o r k f l o w E n t i t y ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   C r e a t e U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 F r a m e B o x   e d i t o r G r o u p   =   n e w   F r a m e B o x ( p a r e n t ) ;  
 	 	 	 e d i t o r G r o u p . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 / / 	 C r é e   l e s   g r a n d s   b l o c s   d e   w i d g e t s .  
 	 	 	 F r a m e B o x   b a n d   =   n e w   F r a m e B o x ( e d i t o r G r o u p ) ;  
 	 	 	 b a n d . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 t h i s . e d i t o r   =   n e w   E d i t o r ( b a n d ) ;  
 	 	 	 t h i s . e d i t o r . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . e d i t o r . A r e a S i z e   =   t h i s . a r e a S i z e ;  
 	 	 	 t h i s . e d i t o r . Z o o m   =   t h i s . Z o o m ;  
 	 	 	 t h i s . e d i t o r . S i z e C h a n g e d   + =   n e w   E v e n t H a n d l e r < D e p e n d e n c y P r o p e r t y C h a n g e d E v e n t A r g s > ( t h i s . H a n d l e E d i t o r S i z e C h a n g e d ) ;  
 	 	 	 t h i s . e d i t o r . A r e a S i z e C h a n g e d   + =   t h i s . H a n d l e E d i t o r A r e a S i z e C h a n g e d ;  
 	 	 	 t h i s . e d i t o r . A r e a O f f s e t C h a n g e d   + =   t h i s . H a n d l e E d i t o r A r e a O f f s e t C h a n g e d ;  
 	 	 	 t h i s . e d i t o r . E d i t i n g S t a t e C h a n g e d   + =   t h i s . H a n d l e E d i t o r E d i t i n g S t a t e C h a n g e d ;  
 	 	 	 t h i s . e d i t o r . Z o o m C h a n g e d   + =   t h i s . H a n d l e E d i t o r Z o o m C h a n g e d ;  
 	 	 	 T o o l T i p . D e f a u l t . R e g i s t e r D y n a m i c T o o l T i p H o s t ( t h i s . e d i t o r ) ;     / /   p o u r   v o i r   l e s   t o o l t i p s   d y n a m i q u e s  
  
 	 	 	 t h i s . v s c r o l l e r   =   n e w   V S c r o l l e r ( b a n d ) ;  
 	 	 	 t h i s . v s c r o l l e r . I s I n v e r t e d   =   t r u e ;  
 	 	 	 t h i s . v s c r o l l e r . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . v s c r o l l e r . V a l u e C h a n g e d   + =   t h i s . H a n d l e S c r o l l e r V a l u e C h a n g e d ;  
 	 	 	 t h i s . e d i t o r . V S c r o l l e r   =   t h i s . v s c r o l l e r ;  
  
 	 	 	 t h i s . g r o u p T o o l b a r   =   n e w   W i d g e t s . R e s e t B o x ( e d i t o r G r o u p ) ;  
 	 	 	 t h i s . g r o u p T o o l b a r . I s P a t c h   =   f a l s e ;  
 	 	 	 t h i s . g r o u p T o o l b a r . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 t h i s . g r o u p T o o l b a r . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   5 ,   0 ) ;  
  
 	 	 	 t h i s . t o o l b a r   =   n e w   H T o o l B a r ( t h i s . g r o u p T o o l b a r . G r o u p B o x ) ;  
 	 	 	 t h i s . t o o l b a r . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 t h i s . h s c r o l l e r   =   n e w   H S c r o l l e r ( e d i t o r G r o u p ) ;  
 	 	 	 t h i s . h s c r o l l e r . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   t h i s . v s c r o l l e r . P r e f e r r e d W i d t h ,   0 ,   0 ) ;  
 	 	 	 t h i s . h s c r o l l e r . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 t h i s . h s c r o l l e r . V a l u e C h a n g e d   + =   t h i s . H a n d l e S c r o l l e r V a l u e C h a n g e d ;  
  
 	 	 	 / / 	 P e u p l e   l a   t o o l b a r .  
 	 	 	 t h i s . b u t t o n Z o o m P a g e   =   n e w   I c o n B u t t o n ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . I c o n U r i   =   M i s c . I c o n ( " Z o o m P a g e " ) ;  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . C l i c k e d   + =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n Z o o m P a g e ,   " Z o o m   s u r   l a   p a g e " ) ;  
  
 	 	 	 t h i s . b u t t o n Z o o m M i n   =   n e w   I c o n B u t t o n ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . I c o n U r i   =   M i s c . I c o n ( " Z o o m M i n " ) ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . C l i c k e d   + =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n Z o o m M i n ,   " Z o o m   m i n i m a l " ) ;  
  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t   =   n e w   I c o n B u t t o n ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . I c o n U r i   =   M i s c . I c o n ( " Z o o m D e f a u l t " ) ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . C l i c k e d   + =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n Z o o m D e f a u l t ,   " Z o o m   1 : 1 " ) ;  
  
 	 	 	 t h i s . b u t t o n Z o o m M a x   =   n e w   I c o n B u t t o n ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . I c o n U r i   =   M i s c . I c o n ( " Z o o m M a x " ) ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . C l i c k e d   + =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n Z o o m M a x ,   " Z o o m   m a x i m a l " ) ;  
  
 	 	 	 t h i s . f i e l d Z o o m   =   n e w   S t a t u s F i e l d ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . f i e l d Z o o m . P r e f e r r e d W i d t h   =   5 0 ;  
 	 	 	 t h i s . f i e l d Z o o m . M a r g i n s   =   n e w   M a r g i n s ( 5 ,   5 ,   1 ,   1 ) ;  
 	 	 	 t h i s . f i e l d Z o o m . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . f i e l d Z o o m . C l i c k e d   + =   t h i s . H a n d l e F i e l d Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . f i e l d Z o o m ,   " C h o i x   d u   z o o m " ) ;  
  
 	 	 	 t h i s . s l i d e r Z o o m   =   n e w   H S l i d e r ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . s l i d e r Z o o m . M i n V a l u e   =   ( d e c i m a l )   M a i n C o n t r o l l e r . z o o m M i n ;  
 	 	 	 t h i s . s l i d e r Z o o m . M a x V a l u e   =   ( d e c i m a l )   M a i n C o n t r o l l e r . z o o m M a x ;  
 	 	 	 t h i s . s l i d e r Z o o m . S m a l l C h a n g e   =   ( d e c i m a l )   0 . 1 ;  
 	 	 	 t h i s . s l i d e r Z o o m . L a r g e C h a n g e   =   ( d e c i m a l )   0 . 2 ;  
 	 	 	 t h i s . s l i d e r Z o o m . R e s o l u t i o n   =   ( d e c i m a l )   0 . 0 1 ;  
 	 	 	 t h i s . s l i d e r Z o o m . P r e f e r r e d W i d t h   =   9 0 ;  
 	 	 	 t h i s . s l i d e r Z o o m . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   4 ,   4 ) ;  
 	 	 	 t h i s . s l i d e r Z o o m . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . s l i d e r Z o o m . V a l u e C h a n g e d   + =   t h i s . H a n d l e S l i d e r Z o o m V a l u e C h a n g e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . s l i d e r Z o o m ,   " M o d i f i e   l e   z o o m " ) ;  
  
 	 	 	 t h i s . b u t t o n S a v e I m a g e   =   n e w   I c o n B u t t o n   ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n S a v e I m a g e . I c o n U r i   =   M i s c . I c o n   ( " S a v e " ) ;  
 	 	 	 t h i s . b u t t o n S a v e I m a g e . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n S a v e I m a g e . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . b u t t o n S a v e I m a g e . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S a v e I m a g e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . b u t t o n S a v e I m a g e ,   " E n r e g i s t r e   l ' i m a g e   d a n s   u n   f i c h i e r   b i t m a p " ) ;  
  
 	 	 	 t h i s . b u t t o n E x p o r t   =   n e w   I c o n B u t t o n   ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n E x p o r t . I c o n U r i   =   M i s c . I c o n   ( " E x p o r t " ) ;  
 	 	 	 t h i s . b u t t o n E x p o r t . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n E x p o r t . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . b u t t o n E x p o r t . M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   1 0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n E x p o r t . C l i c k e d   + =   t h i s . H a n d l e B u t t o n E x p o r t C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . b u t t o n E x p o r t ,   " E x p o r t e   c e   w o r k f l o w " ) ;  
  
 	 	 	 t h i s . b u t t o n I m p o r t   =   n e w   I c o n B u t t o n   ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n I m p o r t . I c o n U r i   =   M i s c . I c o n   ( " I m p o r t " ) ;  
 	 	 	 t h i s . b u t t o n I m p o r t . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n I m p o r t . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . b u t t o n I m p o r t . C l i c k e d   + =   t h i s . H a n d l e B u t t o n I m p o r t C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . b u t t o n I m p o r t ,   " I m p o r t e   u n   w o r k f l o w " ) ;  
  
 	 	 	 t h i s . A r e a S i z e   =   n e w   S i z e   ( 1 0 0 ,   1 0 0 ) ;  
  
 	 	 	 t h i s . e d i t o r . S e t B u s i n e s s C o n t e x t   ( t h i s . b u s i n e s s C o n t e x t ) ;  
 	 	 	 t h i s . e d i t o r . W o r k f l o w D e f i n i t i o n E n t i t y   =   t h i s . w o r k f l o w E n t i t y ;  
  
 	 	 	 t h i s . e d i t o r . U p d a t e G e o m e t r y ( ) ;  
 	 	 	 t h i s . U p d a t e E d i t   ( ) ;  
 	 	 	 t h i s . U p d a t e Z o o m   ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   S a v e D e s i g n ( )  
 	 	 {  
 	 	 	 t h i s . e d i t o r . S a v e D e s i g n   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   S i z e   A r e a S i z e  
 	 	 {  
 	 	 	 / / 	 D i m e n s i o n s   d e   l a   s u r f a c e   p o u r   r e p r é s e n t e r   l e s   b o î t e s   e t   l e s   l i a i s o n s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . a r e a S i z e ;  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . a r e a S i z e   ! =   v a l u e )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . a r e a S i z e   =   v a l u e ;  
  
 	 	 	 	 	 t h i s . e d i t o r . A r e a S i z e   =   t h i s . a r e a S i z e ;  
 	 	 	 	 	 t h i s . U p d a t e S c r o l l e r ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   d o u b l e   Z o o m  
 	 	 {  
 	 	 	 / / 	 Z o o m   p o u r   r e p r é s e n t e r   l e s   b o î t e s   e t   l e s   l i a i s o n s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 i f   ( M a i n C o n t r o l l e r . i s Z o o m P a g e )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   t h i s . Z o o m P a g e ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   M a i n C o n t r o l l e r . z o o m ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 i f   ( M a i n C o n t r o l l e r . z o o m   ! =   v a l u e )  
 	 	 	 	 {  
 	 	 	 	 	 M a i n C o n t r o l l e r . z o o m   =   v a l u e ;  
  
 	 	 	 	 	 t h i s . U p d a t e Z o o m ( ) ;  
 	 	 	 	 	 t h i s . U p d a t e S c r o l l e r ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   d o u b l e   Z o o m P a g e  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   z o o m   p e r m e t t a n t   d e   v o i r   t o u t e   l a   s u r f a c e   d e   t r a v a i l .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 d o u b l e   z x   =   t h i s . e d i t o r . C l i e n t . B o u n d s . W i d t h     /   t h i s . e d i t o r . A r e a S i z e . W i d t h ;  
 	 	 	 	 d o u b l e   z y   =   t h i s . e d i t o r . C l i e n t . B o u n d s . H e i g h t   /   t h i s . e d i t o r . A r e a S i z e . H e i g h t ;  
 	 	 	 	 d o u b l e   z o o m   =   S y s t e m . M a t h . M i n   ( z x ,   z y ) ;  
  
 	 	 	 	 z o o m   =   S y s t e m . M a t h . M a x ( z o o m ,   M a i n C o n t r o l l e r . z o o m M i n ) ;  
 	 	 	 	 z o o m   =   S y s t e m . M a t h . M i n ( z o o m ,   M a i n C o n t r o l l e r . z o o m M a x ) ;  
 	 	 	 	  
 	 	 	 	 z o o m   =   S y s t e m . M a t h . F l o o r ( z o o m * 1 0 0 ) / 1 0 0 ;     / /   4 5 . 8 %   - >   4 6 %  
 	 	 	 	 r e t u r n   z o o m ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e Z o o m ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   t o u t   c e   q u i   d é p e n d   d u   z o o m .  
 	 	 	 t h i s . e d i t o r . Z o o m   =   t h i s . Z o o m ;  
  
 	 	 	 t h i s . f i e l d Z o o m . T e x t   =   s t r i n g . C o n c a t ( S y s t e m . M a t h . F l o o r ( t h i s . Z o o m * 1 0 0 ) . T o S t r i n g ( ) ,   " % " ) ;  
  
 	 	 	 t h i s . i g n o r e C h a n g e   =   t r u e ;  
 	 	 	 t h i s . s l i d e r Z o o m . V a l u e   =   ( d e c i m a l )   t h i s . Z o o m ;  
 	 	 	 t h i s . i g n o r e C h a n g e   =   f a l s e ;  
  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . A c t i v e S t a t e         =   ( M a i n C o n t r o l l e r . i s Z o o m P a g e                             )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . A c t i v e S t a t e           =   ( t h i s . Z o o m   = =   M a i n C o n t r o l l e r . z o o m M i n         )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . A c t i v e S t a t e   =   ( t h i s . Z o o m   = =   M a i n C o n t r o l l e r . z o o m D e f a u l t )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . A c t i v e S t a t e           =   ( t h i s . Z o o m   = =   M a i n C o n t r o l l e r . z o o m M a x         )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e S c r o l l e r ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e s   a s c e n s e u r s ,   e n   f o n c t i o n   d u   z o o m   c o u r a n t   e t   d e   l a   t a i l l e   d e   l ' é d i t e u r .  
 	 	 	 d o u b l e   w   =   t h i s . a r e a S i z e . W i d t h * t h i s . Z o o m   -   t h i s . e d i t o r . C l i e n t . S i z e . W i d t h ;  
 	 	 	 i f   ( w   < =   0   | |   t h i s . e d i t o r . C l i e n t . S i z e . W i d t h   < =   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . h s c r o l l e r . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . h s c r o l l e r . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . h s c r o l l e r . M i n V a l u e   =   ( d e c i m a l )   0 ;  
 	 	 	 	 t h i s . h s c r o l l e r . M a x V a l u e   =   ( d e c i m a l )   w ;  
 	 	 	 	 t h i s . h s c r o l l e r . S m a l l C h a n g e   =   ( d e c i m a l )   ( w / 1 0 ) ;  
 	 	 	 	 t h i s . h s c r o l l e r . L a r g e C h a n g e   =   ( d e c i m a l )   ( w / 5 ) ;  
 	 	 	 	 t h i s . h s c r o l l e r . V i s i b l e R a n g e R a t i o   =   ( d e c i m a l )   ( t h i s . e d i t o r . C l i e n t . S i z e . W i d t h   /   ( t h i s . a r e a S i z e . W i d t h * t h i s . Z o o m ) ) ;  
 	 	 	 }  
  
 	 	 	 d o u b l e   h   =   t h i s . a r e a S i z e . H e i g h t * t h i s . Z o o m   -   t h i s . e d i t o r . C l i e n t . S i z e . H e i g h t ;  
 	 	 	 i f   ( h   < =   0   | |   t h i s . e d i t o r . C l i e n t . S i z e . H e i g h t   < =   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . v s c r o l l e r . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . v s c r o l l e r . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . v s c r o l l e r . M i n V a l u e   =   ( d e c i m a l )   0 ;  
 	 	 	 	 t h i s . v s c r o l l e r . M a x V a l u e   =   ( d e c i m a l )   h ;  
 	 	 	 	 t h i s . v s c r o l l e r . S m a l l C h a n g e   =   ( d e c i m a l )   ( h / 1 0 ) ;  
 	 	 	 	 t h i s . v s c r o l l e r . L a r g e C h a n g e   =   ( d e c i m a l )   ( h / 5 ) ;  
 	 	 	 	 t h i s . v s c r o l l e r . V i s i b l e R a n g e R a t i o   =   ( d e c i m a l )   ( t h i s . e d i t o r . C l i e n t . S i z e . H e i g h t   /   ( t h i s . a r e a S i z e . H e i g h t * t h i s . Z o o m ) ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . e d i t o r . I s S c r o l l e r E n a b l e   =   t h i s . h s c r o l l e r . E n a b l e   | |   t h i s . v s c r o l l e r . E n a b l e ;  
 	 	 	 t h i s . H a n d l e S c r o l l e r V a l u e C h a n g e d ( n u l l ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e E d i t ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e s   l i g n e s   é d i t a b l e s   e n   f o n c t i o n   d e   l a   s é l e c t i o n   d a n s   l e   t a b l e a u .  
 	 	 	 t h i s . e d i t o r . C r e a t e I n i t i a l W o r k f l o w   ( ) ;  
 	 	 	 t h i s . Z o o m   =   t h i s . Z o o m ;  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   G e t s   t h e   s u g g e s t e d   f i l e   n a m e   f o r   t h e   c u r r e n t l y   a c t i v e   w o r k f l o w   e n t i t y .  
 	 	 / / /   T h e   f i l e   n a m e   w i l l   b e   a d j u s t e d   t o   r e p l a c e   s o m e   s p e c i a l   c h a r a c t e r s .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < p a r a m   n a m e = " e x t e n s i o n " > T h e   o p t i o n a l   f i l e   e x t e n s i o n   ( e . g .   < c > " . p n g " < / c > ) . < / p a r a m >  
 	 	 / / /   < r e t u r n s > T h e   f i l e   n a m e . < / r e t u r n s >  
 	 	 p r i v a t e   s t r i n g   G e t D e f a u l t F i l e N a m e ( s t r i n g   e x t e n s i o n   =   n u l l )  
 	 	 {  
 	 	 	 s t r i n g   p a t h   =   t h i s . w o r k f l o w E n t i t y . W o r k f l o w N a m e . T o S i m p l e T e x t   ( ) ;  
  
 	 	 	 p a t h   =   p a t h . R e p l a c e   ( ' / ' ,   ' - ' ) ;  
 	 	 	 p a t h   =   p a t h . R e p l a c e   ( ' : ' ,   ' - ' ) ;  
  
 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y   ( e x t e n s i o n ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   p a t h ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 r e t u r n   p a t h   +   e x t e n s i o n ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 # r e g i o n   E v e n t s   h a n d l e r s  
 	 	 p r i v a t e   v o i d   H a n d l e E d i t o r S i z e C h a n g e d ( o b j e c t   s e n d e r ,   D e p e n d e n c y P r o p e r t y C h a n g e d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l a   t a i l l e   d e   l a   f e n ê t r e   d e   l ' é d i t e u r   c h a n g e .  
 	 	 	 t h i s . U p d a t e S c r o l l e r ( ) ;  
 	 	 	 t h i s . U p d a t e Z o o m ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e E d i t o r A r e a S i z e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l e s   d i m e n s i o n s   d e   l a   z o n e   d e   t r a v a i l   o n t   c h a n g é .  
 	 	 	 t h i s . A r e a S i z e   =   t h i s . e d i t o r . A r e a S i z e ;  
 	 	 	 t h i s . U p d a t e Z o o m ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e E d i t o r A r e a O f f s e t C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l ' o f f s e t   d e   l a   z o n e   d e   t r a v a i l   a   c h a n g é .  
 	 	 	 P o i n t   o f f s e t   =   t h i s . e d i t o r . A r e a O f f s e t ;  
  
 	 	 	 i f   ( t h i s . h s c r o l l e r . E n a b l e )  
 	 	 	 {  
 	 	 	 	 o f f s e t . X   =   S y s t e m . M a t h . M a x ( o f f s e t . X ,   ( d o u b l e )   t h i s . h s c r o l l e r . M i n V a l u e / t h i s . Z o o m ) ;  
 	 	 	 	 o f f s e t . X   =   S y s t e m . M a t h . M i n ( o f f s e t . X ,   ( d o u b l e )   t h i s . h s c r o l l e r . M a x V a l u e / t h i s . Z o o m ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 o f f s e t . X   =   0 ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . v s c r o l l e r . E n a b l e )  
 	 	 	 {  
 	 	 	 	 o f f s e t . Y   =   S y s t e m . M a t h . M a x ( o f f s e t . Y ,   ( d o u b l e )   t h i s . v s c r o l l e r . M i n V a l u e / t h i s . Z o o m ) ;  
 	 	 	 	 o f f s e t . Y   =   S y s t e m . M a t h . M i n ( o f f s e t . Y ,   ( d o u b l e )   t h i s . v s c r o l l e r . M a x V a l u e / t h i s . Z o o m ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 o f f s e t . Y   =   0 ;  
 	 	 	 }  
  
 	 	 	 t h i s . e d i t o r . A r e a O f f s e t   =   o f f s e t ;  
  
 	 	 	 t h i s . h s c r o l l e r . V a l u e   =   ( d e c i m a l )   ( o f f s e t . X * t h i s . Z o o m ) ;  
 	 	 	 t h i s . v s c r o l l e r . V a l u e   =   ( d e c i m a l )   ( o f f s e t . Y * t h i s . Z o o m ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e E d i t o r E d i t i n g S t a t e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l ' é t a t   d ' é d i t i o n   a   c h a n g é .  
 	 	 	 i f   ( t h i s . e d i t o r . I s E d i t i n g )  
 	 	 	 {  
 	 	 	 	 t h i s . h s c r o l l e r . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . v s c r o l l e r . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . U p d a t e S c r o l l e r   ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . t o o l b a r . E n a b l e   =   ! t h i s . e d i t o r . I s E d i t i n g ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S c r o l l e r V a l u e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n   a s c e n s e u r   a   é t é   b o u g é .  
 	 	 	 d o u b l e   o x   =   0 ;  
 	 	 	 i f   ( t h i s . h s c r o l l e r . I s E n a b l e d )  
 	 	 	 {  
 	 	 	 	 o x   =   ( d o u b l e )   t h i s . h s c r o l l e r . V a l u e / t h i s . Z o o m ;  
 	 	 	 }  
  
 	 	 	 d o u b l e   o y   =   0 ;  
 	 	 	 i f   ( t h i s . v s c r o l l e r . I s E n a b l e d )  
 	 	 	 {  
 	 	 	 	 o y   =   ( d o u b l e )   t h i s . v s c r o l l e r . V a l u e / t h i s . Z o o m ;  
 	 	 	 }  
  
 	 	 	 t h i s . e d i t o r . A r e a O f f s e t   =   n e w   P o i n t ( o x ,   o y ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n Z o o m C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n   b o u t o n   d e   z o o m   p r é d é f i n i   e s t   c l i q u é .  
 	 	 	 i f   ( s e n d e r   = =   t h i s . b u t t o n Z o o m P a g e )  
 	 	 	 {  
 	 	 	 	 M a i n C o n t r o l l e r . i s Z o o m P a g e   =   t r u e ;  
 	 	 	 	 t h i s . Z o o m   =   0 ;  
 	 	 	 }  
  
 	 	 	 i f   ( s e n d e r   = =   t h i s . b u t t o n Z o o m M i n )  
 	 	 	 {  
 	 	 	 	 M a i n C o n t r o l l e r . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 	 t h i s . Z o o m   =   M a i n C o n t r o l l e r . z o o m M i n ;  
 	 	 	 }  
  
 	 	 	 i f   ( s e n d e r   = =   t h i s . b u t t o n Z o o m D e f a u l t )  
 	 	 	 {  
 	 	 	 	 M a i n C o n t r o l l e r . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 	 t h i s . Z o o m   =   M a i n C o n t r o l l e r . z o o m D e f a u l t ;  
 	 	 	 }  
 	 	 	  
 	 	 	 i f   ( s e n d e r   = =   t h i s . b u t t o n Z o o m M a x )  
 	 	 	 {  
 	 	 	 	 M a i n C o n t r o l l e r . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 	 t h i s . Z o o m   =   M a i n C o n t r o l l e r . z o o m M a x ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e F i e l d Z o o m C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l e   c h a m p   d u   z o o m   a   é t é   c l i q u é .  
 	 	 	 S t a t u s F i e l d   s f   =   s e n d e r   a s   S t a t u s F i e l d ;  
 	 	 	 i f   ( s f   = =   n u l l )     r e t u r n ;  
 	 	 	 V M e n u   m e n u   =   Z o o m M e n u . C r e a t e Z o o m M e n u   ( M a i n C o n t r o l l e r . z o o m D e f a u l t ,   t h i s . Z o o m ,   t h i s . Z o o m P a g e ,   t h i s . H a n d l e M e n u Z o o m V a l u e C h a n g e d ) ;  
 	 	 	 m e n u . H o s t   =   s f . W i n d o w ;  
 	 	 	 T e x t F i e l d C o m b o . A d j u s t C o m b o S i z e ( s f ,   m e n u ,   f a l s e ) ;  
 	 	 	 m e n u . S h o w A s C o m b o L i s t ( s f ,   P o i n t . Z e r o ,   s f ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e M e n u Z o o m V a l u e C h a n g e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 M e n u I t e m   i t e m   =   s e n d e r   a s   M e n u I t e m ;  
 	 	 	 M a i n C o n t r o l l e r . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 t h i s . Z o o m   =   d o u b l e . P a r s e   ( i t e m . N a m e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S l i d e r Z o o m V a l u e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l e   s l i d e r   d u   z o o m   a   é t é   b o u g é .  
 	 	 	 i f   ( t h i s . i g n o r e C h a n g e )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 H S l i d e r   s l i d e r   =   s e n d e r   a s   H S l i d e r ;  
 	 	 	 M a i n C o n t r o l l e r . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 t h i s . Z o o m   =   ( d o u b l e )   s l i d e r . V a l u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e E d i t o r Z o o m C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l e   z o o m   a   c h a n g é   d e p u i s   l ' é d i t e u r .  
 	 	 	 M a i n C o n t r o l l e r . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 t h i s . Z o o m   =   t h i s . e d i t o r . Z o o m ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n I m p o r t C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 s t r i n g   p a t h ;  
  
 	 	 	 i f   ( t h i s . I m p o r t D i a l o g   ( o u t   p a t h ) )  
 	 	 	 {  
 	 	 	 	 t h i s . e d i t o r . I m p o r t   ( p a t h ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n E x p o r t C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 s t r i n g   p a t h ;  
 	 	 	 b o o l   e x p o r t A l l ;  
  
 	 	 	 i f   ( t h i s . E x p o r t D i a l o g   ( o u t   p a t h ,   o u t   e x p o r t A l l ) )  
 	 	 	 {  
 	 	 	 	 t h i s . e d i t o r . E x p o r t   ( p a t h ,   e x p o r t A l l ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n S a v e I m a g e C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 s t r i n g   p a t h ;  
 	 	 	 d o u b l e   z o o m ;  
  
 	 	 	 i f   ( t h i s . S a v e I m a g e D i a l o g   ( o u t   p a t h ,   o u t   z o o m ) )  
 	 	 	 {  
 	 	 	 	 t h i s . e d i t o r . S a v e I m a g e   ( p a t h ,   z o o m ) ;  
 	 	 	 }  
 	 	 }  
 	 	 # e n d r e g i o n  
  
 	 	 # r e g i o n   D i a l o g s  
 	 	 p r i v a t e   b o o l   I m p o r t D i a l o g ( o u t   s t r i n g   p a t h )  
 	 	 {  
 	 	 	 v a r   d i a l o g   =   n e w   D i a l o g s . F i l e I m p o r t D i a l o g   ( t h i s . e d i t o r ) ;  
 	 	 	 d i a l o g . S h o w D i a l o g   ( ) ;  
  
 	 	 	 i f   ( d i a l o g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 p a t h   =   n u l l ;  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 d i a l o g . P a t h M e m o r i z e   ( ) ;  
  
 	 	 	 p a t h   =   d i a l o g . F i l e N a m e ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   E x p o r t D i a l o g ( o u t   s t r i n g   p a t h ,   o u t   b o o l   e x p o r t A l l )  
 	 	 {  
 	 	 	 v a r   d i a l o g   =   n e w   D i a l o g s . F i l e E x p o r t D i a l o g   ( t h i s . e d i t o r )  
 	 	 	 {  
 	 	 	 	 I n i t i a l F i l e N a m e   =   t h i s . G e t D e f a u l t F i l e N a m e   ( )  
 	 	 	 } ;  
 	 	 	  
 	 	 	 d i a l o g . S h o w D i a l o g   ( ) ;  
  
 	 	 	 i f   ( d i a l o g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 p a t h   =   n u l l ;  
 	 	 	 	 e x p o r t A l l   =   f a l s e ;  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 d i a l o g . P a t h M e m o r i z e   ( ) ;  
  
 	 	 	 p a t h   =   d i a l o g . F i l e N a m e ;  
 	 	 	 e x p o r t A l l   =   d i a l o g . E x p o r t A l l ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   S a v e I m a g e D i a l o g ( o u t   s t r i n g   p a t h ,   o u t   d o u b l e   z o o m )  
 	 	 {  
 	 	 	 v a r   d i a l o g   =   n e w   D i a l o g s . F i l e S a v e I m a g e D i a l o g   ( t h i s . e d i t o r )  
 	 	 	 {  
 	 	 	 	 I m a g e S i z e   =   t h i s . e d i t o r . A r e a S i z e ,  
 	 	 	 	 I n i t i a l F i l e N a m e   =   t h i s . G e t D e f a u l t F i l e N a m e   ( " . p n g " )  
 	 	 	 } ;  
  
 	 	 	 d i a l o g . S h o w D i a l o g   ( ) ;  
  
 	 	 	 i f   ( d i a l o g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 p a t h   =   n u l l ;  
 	 	 	 	 z o o m   =   0 ;  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 d i a l o g . P a t h M e m o r i z e   ( ) ;  
  
 	 	 	 p a t h   =   d i a l o g . F i l e N a m e ;  
 	 	 	 z o o m   =   d i a l o g . Z o o m ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
 	 	 # e n d r e g i o n  
  
  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e 	 	 	 	 	 z o o m M i n   =   0 . 2 ;  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e 	 	 	 	 	 z o o m M a x   =   2 . 0 ;  
 	 	 p r i v a t e   s t a t i c   r e a d o n l y   d o u b l e 	 	 	 	 	 z o o m D e f a u l t   =   1 . 0 ;  
  
 	 	 p r i v a t e   r e a d o n l y   C o r e . B u s i n e s s . B u s i n e s s C o n t e x t 	 b u s i n e s s C o n t e x t ;  
 	 	 p r i v a t e   r e a d o n l y   W o r k f l o w D e f i n i t i o n E n t i t y 	 	 w o r k f l o w E n t i t y ;  
 	 	  
 	 	 p r i v a t e   s t a t i c   d o u b l e 	 	 	 	 	 	 	 z o o m   =   M a i n C o n t r o l l e r . z o o m D e f a u l t ;  
 	 	 p r i v a t e   s t a t i c   b o o l 	 	 	 	 	 	 	 	 i s Z o o m P a g e ;  
  
 	 	 p r i v a t e   E d i t o r 	 	 	 	 	 	 	 	 	 e d i t o r ;  
 	 	 p r i v a t e   V S c r o l l e r 	 	 	 	 	 	 	 	 v s c r o l l e r ;  
 	 	 p r i v a t e   H S c r o l l e r 	 	 	 	 	 	 	 	 h s c r o l l e r ;  
 	 	 p r i v a t e   S i z e 	 	 	 	 	 	 	 	 	 a r e a S i z e ;  
 	 	 p r i v a t e   W i d g e t s . R e s e t B o x 	 	 	 	 	 	 g r o u p T o o l b a r ;  
 	 	 p r i v a t e   H T o o l B a r 	 	 	 	 	 	 	 	 t o o l b a r ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 	 	 b u t t o n Z o o m P a g e ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 	 	 b u t t o n Z o o m M i n ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 	 	 b u t t o n Z o o m D e f a u l t ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 	 	 b u t t o n Z o o m M a x ;  
 	 	 p r i v a t e   S t a t u s F i e l d 	 	 	 	 	 	 	 	 f i e l d Z o o m ;  
 	 	 p r i v a t e   H S l i d e r 	 	 	 	 	 	 	 	 	 s l i d e r Z o o m ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 	 	 b u t t o n I m p o r t ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 	 	 b u t t o n E x p o r t ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 	 	 b u t t o n S a v e I m a g e ;  
 	 	 p r i v a t e   b o o l 	 	 	 	 	 	 	 	 	 i g n o r e C h a n g e ;  
 	 }  
 }  
 