ÿþ/ / 	 C o p y r i g h t   ©   2 0 1 0 ,   E P S I T E C   S A ,   C H - 1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
 u s i n g   E p s i t e c . C o m m o n . D i a l o g s ;  
  
 u s i n g   E p s i t e c . C r e s u s . C o r e . B u s i n e s s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . D o c u m e n t s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . D o c u m e n t s . V e r b o s e ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . E n t i t i e s ;  
  
 u s i n g   S y s t e m . T e x t . R e g u l a r E x p r e s s i o n s ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . D o c u m e n t O p t i o n s C o n t r o l l e r  
 {  
 	 p u b l i c   c l a s s   V a l u e s C o n t r o l l e r  
 	 {  
 	 	 p u b l i c   V a l u e s C o n t r o l l e r ( B u s i n e s s C o n t e x t   b u s i n e s s C o n t e x t ,   D o c u m e n t O p t i o n s E n t i t y   d o c u m e n t O p t i o n s E n t i t y ,   P r i n t i n g O p t i o n D i c t i o n a r y   o p t i o n s )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t               =   b u s i n e s s C o n t e x t ;  
 	 	 	 t h i s . d o c u m e n t O p t i o n s E n t i t y   =   d o c u m e n t O p t i o n s E n t i t y ;  
 	 	 	 t h i s . o p t i o n s                               =   o p t i o n s ;  
  
 	 	 	 t h i s . a l l O p t i o n s       =   V e r b o s e D o c u m e n t O p t i o n . G e t A l l   ( ) . W h e r e   ( x   = >   ! x . I s T i t l e ) . T o L i s t   ( ) ;  
 	 	 	 t h i s . t i t l e O p t i o n s   =   V e r b o s e D o c u m e n t O p t i o n . G e t A l l   ( ) . W h e r e   ( x   = >   x . I s T i t l e ) . T o L i s t   ( ) ;  
  
 	 	 	 t h i s . e d i t a b l e W i d g e t s   =   n e w   L i s t < W i d g e t >   ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   C r e a t e U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   l e f t F r a m e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   V a l u e s C o n t r o l l e r . D o c u m e n t O p t i o n s W i d t h ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   1 0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   r i g h t F r a m e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . C r e a t e L e f t U I   ( l e f t F r a m e ) ;  
 	 	 	 t h i s . C r e a t e R i g h t U I   ( r i g h t F r a m e ) ;  
  
 	 	 	 t h i s . U p d a t e   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C r e a t e L e f t U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 t h i s . o p t i o n s F r a m e   =   n e w   S c r o l l a b l e  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 H o r i z o n t a l S c r o l l e r M o d e   =   S c r o l l a b l e S c r o l l e r M o d e . H i d e A l w a y s ,  
 	 	 	 	 V e r t i c a l S c r o l l e r M o d e   =   S c r o l l a b l e S c r o l l e r M o d e . A u t o ,  
 	 	 	 	 P a i n t V i e w p o r t F r a m e   =   f a l s e ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . o p t i o n s F r a m e . V i e w p o r t . I s A u t o F i t t i n g   =   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C r e a t e R i g h t U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   t i t l e   =   n e w   S t a t i c T e x t  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 T e x t   =   " A d a p t é   a u x   d o c u m e n t s   s u i v a n t s   : " ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   2 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   b o x   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 P r e f e r r e d H e i g h t   =   2 0 0 ,  
 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 1 0 ,   1 0 ,   1 ,   1 ) ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . d o c u m e n t T y p e s T e x t   =   n e w   S t a t i c T e x t  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 C o n t e n t A l i g n m e n t   =   C o m m o n . D r a w i n g . C o n t e n t A l i g n m e n t . T o p L e f t ,  
 	 	 	 	 T e x t B r e a k M o d e   =   C o m m o n . D r a w i n g . T e x t B r e a k M o d e . H y p h e n a t e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   U p d a t e ( )  
 	 	 {  
 	 	 	 t h i s . U p d a t e O p t i o n B u t t o n s   ( ) ;  
 	 	 	 t h i s . U p d a t e D o c u m e n t T y p e s T e x t   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e O p t i o n B u t t o n s ( )  
 	 	 {  
 	 	 	 v a r   c o n t r o l l e r   =   n e w   O p t i o n s C o n t r o l l e r   ( n u l l ,   t h i s . o p t i o n s ) ;  
 	 	 	 c o n t r o l l e r . C r e a t e U I   ( t h i s . o p t i o n s F r a m e . V i e w p o r t ,   t h i s . S e t D i r t y ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e D o c u m e n t T y p e s T e x t ( )  
 	 	 {  
 	 	 	 t h i s . d o c u m e n t T y p e s T e x t . F o r m a t t e d T e x t   =   t h i s . o p t i o n s . G e t D o c u m e n t T y p e s S u m m a r y   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   S e t D i r t y ( )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t . N o t i f y E x t e r n a l C h a n g e s   ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e   D o c u m e n t O p t i o n s W i d t h   =   3 0 0 ;  
  
 	 	 p r i v a t e   r e a d o n l y   B u s i n e s s C o n t e x t 	 	 	 	 	 b u s i n e s s C o n t e x t ;  
 	 	 p r i v a t e   r e a d o n l y   D o c u m e n t O p t i o n s E n t i t y 	 	 	 	 d o c u m e n t O p t i o n s E n t i t y ;  
 	 	 p r i v a t e   r e a d o n l y   P r i n t i n g O p t i o n D i c t i o n a r y 	 	 	 o p t i o n s ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < V e r b o s e D o c u m e n t O p t i o n > 	 	 a l l O p t i o n s ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < V e r b o s e D o c u m e n t O p t i o n > 	 	 t i t l e O p t i o n s ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < W i d g e t > 	 	 	 	 	 	 e d i t a b l e W i d g e t s ;  
  
 	 	 p r i v a t e   S c r o l l a b l e 	 	 	 	 	 	 	 	 	 o p t i o n s F r a m e ;  
 	 	 p r i v a t e   S t a t i c T e x t 	 	 	 	 	 	 	 	 	 d o c u m e n t T y p e s T e x t ;  
 	 }  
 }  
 