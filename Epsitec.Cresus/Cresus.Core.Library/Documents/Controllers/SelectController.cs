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
 u s i n g   E p s i t e c . C r e s u s . C o r e . W i d g e t s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . L i b r a r y ;  
  
 u s i n g   S y s t e m . T e x t . R e g u l a r E x p r e s s i o n s ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . R e s o l v e r s ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . D o c u m e n t O p t i o n s C o n t r o l l e r  
 {  
 	 p u b l i c   c l a s s   S e l e c t C o n t r o l l e r  
 	 {  
 	 	 p u b l i c   S e l e c t C o n t r o l l e r ( B u s i n e s s C o n t e x t   b u s i n e s s C o n t e x t ,   D o c u m e n t O p t i o n s E n t i t y   d o c u m e n t O p t i o n s E n t i t y ,   P r i n t i n g O p t i o n D i c t i o n a r y   o p t i o n s )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t               =   b u s i n e s s C o n t e x t ;  
 	 	 	 t h i s . d o c u m e n t O p t i o n s E n t i t y   =   d o c u m e n t O p t i o n s E n t i t y ;  
 	 	 	 t h i s . o p t i o n s                               =   o p t i o n s ;  
  
 	 	 	 t h i s . a l l O p t i o n s   =   V e r b o s e D o c u m e n t O p t i o n . G e t A l l   ( ) . T o L i s t   ( ) ;  
 	 	 	 t h i s . v i s i b l e O p t i o n s   =   n e w   L i s t < V e r b o s e D o c u m e n t O p t i o n >   ( ) ;  
  
 	 	 	 t h i s . d o c u m e n t T y p e F i l t e r   =   D o c u m e n t T y p e . U n k n o w n ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   C r e a t e U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   l e f t F r a m e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   3 3 0 ,  
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
 	 	 	 / / 	 C o n n e x i o n   d e s   é v é n e m e n t s .  
 	 	 	 t h i s . t a b l e . S e l e c t i o n C h a n g e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . U p d a t e W i d g e t s   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . n o B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . A c t i o n U s e   ( f a l s e ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . y e s B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . A c t i o n U s e   ( t r u e ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . n o n e B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . A c t i o n A l l   ( f a l s e ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . a l l B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . A c t i o n A l l   ( t r u e ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . U p d a t e T a b l e   ( ) ;  
 	 	 	 t h i s . U p d a t e W i d g e t s   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C r e a t e L e f t U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 {  
 	 	 	 	 v a r   t o p   =   n e w   F r a m e B o x  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   1 0 ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 n e w   S t a t i c T e x t  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   t o p ,  
 	 	 	 	 	 T e x t   =   " F i l t r e " ,  
 	 	 	 	 	 P r e f e r r e d W i d t h   =   3 5 ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 } ;  
  
 	 	 	 	 v a r   c o m b o   =   n e w   T e x t F i e l d C o m b o  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   t o p ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 I s R e a d O n l y   =   t r u e ,  
 	 	 	 	 } ;  
  
 	 	 	 	 v a r   t y p e s   =   E n u m K e y V a l u e s . F r o m E n u m < D o c u m e n t T y p e >   ( ) ;  
 	 	 	 	 f o r e a c h   ( v a r   t y p e   i n   t y p e s )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t y p e . K e y   = =   D o c u m e n t T y p e . N o n e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 c o m b o . I t e m s . A d d   ( D o c u m e n t T y p e . N o n e . T o S t r i n g   ( ) ,   " M o n t r e r   l e s   o p t i o n s   p o u r   t o u s   l e s   d o c u m e n t s " ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( t y p e . K e y   = =   D o c u m e n t T y p e . U n k n o w n )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 c o n t i n u e ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   d e s c r i p t i o n   =   s t r i n g . F o r m a t   ( " S e u l e m e n t   l e s   o p t i o n s   p o u r   \ " { 0 } \ " " ,   t y p e . V a l u e s [ 0 ] ) ;  
 	 	 	 	 	 	 c o m b o . I t e m s . A d d   ( t y p e . K e y . T o S t r i n g ( ) ,   d e s c r i p t i o n ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . D o c u m e n t T y p e F i l t e r   =   D o c u m e n t T y p e . N o n e ;  
 	 	 	 	 c o m b o . S e l e c t e d I t e m I n d e x   =   0 ;     / /   s é l e c t i o n n e   " M o n t r e r   t o u s   l e s   d o c u m e n t s "  
  
 	 	 	 	 c o m b o . S e l e c t e d I t e m C h a n g e d   + =   d e l e g a t e  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   k e y   =   c o m b o . I t e m s . G e t K e y   ( c o m b o . S e l e c t e d I t e m I n d e x ) ;  
 	 	 	 	 	 t h i s . D o c u m e n t T y p e F i l t e r   =   ( D o c u m e n t T y p e )   S y s t e m . E n u m . P a r s e   ( t y p e o f   ( D o c u m e n t T y p e ) ,   k e y ) ;  
 	 	 	 	 	 t h i s . U p d a t e T a b l e   ( ) ;  
 	 	 	 	 	 t h i s . U p d a t e W i d g e t s   ( ) ;  
 	 	 	 	 } ;  
 	 	 	 }  
  
 	 	 	 t h i s . t a b l e   =   n e w   C e l l T a b l e  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 S t y l e H   =   C e l l A r r a y S t y l e s . S e p a r a t o r   |   C e l l A r r a y S t y l e s . H e a d e r ,  
 	 	 	 	 S t y l e V   =   C e l l A r r a y S t y l e s . S c r o l l N o r m   |   C e l l A r r a y S t y l e s . S e p a r a t o r   |   C e l l A r r a y S t y l e s . S e l e c t L i n e   |   C e l l A r r a y S t y l e s . S e l e c t M u l t i ,  
 	 	 	 } ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C r e a t e R i g h t U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 {  
 	 	 	 	 v a r   t i t l e   =   n e w   S t a t i c T e x t  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 	 T e x t   =   " A d a p t é   a u x   d o c u m e n t s   s u i v a n t s   : " ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   2 ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 v a r   b o x   =   n e w   F r a m e B o x  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 	 P r e f e r r e d H e i g h t   =   2 0 0 ,  
 	 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 1 0 ,   1 0 ,   1 ,   1 ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . d o c u m e n t T y p e s T e x t   =   n e w   S t a t i c T e x t  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 C o n t e n t A l i g n m e n t   =   C o m m o n . D r a w i n g . C o n t e n t A l i g n m e n t . T o p L e f t ,  
 	 	 	 	 	 T e x t B r e a k M o d e   =   C o m m o n . D r a w i n g . T e x t B r e a k M o d e . H y p h e n a t e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 } ;  
 	 	 	 }  
  
 	 	 	 {  
 	 	 	 	 v a r   b u t t o n s B o x   =   n e w   F r a m e B o x  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . B o t t o m ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   1 0 ,   0 ) ,  
 	 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 1 0 ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . n o B u t t o n   =   n e w   R a d i o B u t t o n  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   b u t t o n s B o x ,  
 	 	 	 	 	 T e x t   =   " N ' u t i l i s e   p a s " ,  
 	 	 	 	 	 A u t o T o g g l e   =   f a l s e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . y e s B u t t o n   =   n e w   R a d i o B u t t o n  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   b u t t o n s B o x ,  
 	 	 	 	 	 T e x t   =   " U t i l i s e " ,  
 	 	 	 	 	 A u t o T o g g l e   =   f a l s e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . n o n e B u t t o n   =   n e w   B u t t o n  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   b u t t o n s B o x ,  
 	 	 	 	 	 T e x t   =   " N ' u t i l i s e   a u c u n " ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   5 ,   0 ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . a l l B u t t o n   =   n e w   B u t t o n  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   b u t t o n s B o x ,  
 	 	 	 	 	 T e x t   =   " U t i l i s e   t o u t " ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   1 ,   0 ) ,  
 	 	 	 	 } ;  
 	 	 	 }  
  
 	 	 	 {  
 	 	 	 	 v a r   b o x   =   n e w   F r a m e B o x  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 	 P r e f e r r e d H e i g h t   =   4 + 1 4 * 6 ,  
 	 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . B o t t o m ,  
 	 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 1 0 ,   1 0 ,   1 ,   1 ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 v a r   t i t l e   =   n e w   S t a t i c T e x t  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 	 T e x t   =   " T y p e   d e   l a   v a l e u r   : " ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . B o t t o m ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   5 ,   2 ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . v a l u e T y p e T e x t   =   n e w   S t a t i c T e x t  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 C o n t e n t A l i g n m e n t   =   C o m m o n . D r a w i n g . C o n t e n t A l i g n m e n t . T o p L e f t ,  
 	 	 	 	 	 T e x t B r e a k M o d e   =   C o m m o n . D r a w i n g . T e x t B r e a k M o d e . H y p h e n a t e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 } ;  
 	 	 	 }  
  
 	 	 	 {  
 	 	 	 	 v a r   b o x   =   n e w   F r a m e B o x  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 	 P r e f e r r e d H e i g h t   =   4 + 1 4 * 3 ,  
 	 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . B o t t o m ,  
 	 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 1 0 ,   1 0 ,   1 ,   1 ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 v a r   t i t l e   =   n e w   S t a t i c T e x t  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 	 T e x t   =   " D e s c r i p t i o n   d e   l ' o p t i o n   : " ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . B o t t o m ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   5 ,   2 ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . v a l u e D e s c r i p t i o n   =   n e w   S t a t i c T e x t  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 C o n t e n t A l i g n m e n t   =   C o m m o n . D r a w i n g . C o n t e n t A l i g n m e n t . T o p L e f t ,  
 	 	 	 	 	 T e x t B r e a k M o d e   =   C o m m o n . D r a w i n g . T e x t B r e a k M o d e . H y p h e n a t e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 } ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   U p d a t e ( )  
 	 	 {  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   A c t i o n U s e ( b o o l   v a l u e )  
 	 	 {  
 	 	 	 f o r   ( i n t   r o w   =   0 ;   r o w   <   t h i s . v i s i b l e O p t i o n s . C o u n t ;   r o w + + )  
 	 	 	 {  
 	 	 	 	 i f   ( t a b l e . I s C e l l S e l e c t e d   ( r o w ,   0 ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . S e t U s e d O p t i o n   ( r o w ,   v a l u e ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e T a b l e   ( ) ;  
 	 	 	 t h i s . U p d a t e W i d g e t s   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   A c t i o n A l l ( b o o l   v a l u e )  
 	 	 {  
 	 	 	 f o r   ( i n t   s e l   =   0 ;   s e l   <   t h i s . v i s i b l e O p t i o n s . C o u n t ;   s e l + + )  
 	 	 	 {  
 	 	 	 	 t h i s . S e t U s e d O p t i o n   ( s e l ,   v a l u e ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e T a b l e   ( ) ;  
 	 	 	 t h i s . U p d a t e W i d g e t s   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   U p d a t e T a b l e ( )  
 	 	 {  
 	 	 	 i n t   r o w s   =   t h i s . v i s i b l e O p t i o n s . C o u n t ;  
 	 	 	 t h i s . t a b l e . S e t A r r a y S i z e   ( 3 ,   r o w s ) ;  
  
 	 	 	 t h i s . t a b l e . S e t W i d t h C o l u m n   ( 0 ,   2 2 0 ) ;  
 	 	 	 t h i s . t a b l e . S e t W i d t h C o l u m n   ( 1 ,   4 0 ) ;  
 	 	 	 t h i s . t a b l e . S e t W i d t h C o l u m n   ( 2 ,   5 0 ) ;  
  
 	 	 	 t h i s . t a b l e . S e t H e a d e r T e x t H   ( 0 ,   " D e s c r i p t i o n " ) ;  
 	 	 	 t h i s . t a b l e . S e t H e a d e r T e x t H   ( 1 ,   " T y p e " ) ;  
 	 	 	 t h i s . t a b l e . S e t H e a d e r T e x t H   ( 2 ,   " U t i l i s é " ) ;  
  
 	 	 	 C o n t e n t A l i g n m e n t [ ]   a l i g n m e n t s   =  
 	 	 	 {  
 	 	 	 	 C o n t e n t A l i g n m e n t . M i d d l e L e f t ,  
 	 	 	 	 C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ,  
 	 	 	 	 C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ,  
 	 	 	 } ;  
  
 	 	 	 f o r   ( i n t   r o w = 0 ;   r o w < r o w s ;   r o w + + )  
 	 	 	 {  
 	 	 	 	 t h i s . t a b l e . F i l l R o w   ( r o w ,   a l i g n m e n t s ) ;  
 	 	 	 	 t h i s . t a b l e . U p d a t e R o w   ( r o w ,   t h i s . G e t R o w T e x t s   ( r o w ) ) ;  
 	 	 	 	 t h i s . t a b l e . U p d a t e R o w   ( r o w ,   t h i s . G e t R o w C o l o r s   ( r o w ) ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g [ ]   G e t R o w T e x t s ( i n t   r o w )  
 	 	 {  
 	 	 	 v a r   r e s u l t   =   n e w   s t r i n g [ 3 ] ;  
  
 	 	 	 i f   ( t h i s . I s T i t l e   ( r o w ) )  
 	 	 	 {  
 	 	 	 	 r e s u l t [ 0 ]   =   T e x t F o r m a t t e r . F o r m a t T e x t   ( s t r i n g . C o n c a t   ( t h i s . G e t D e s c r i p t i o n   ( r o w ) ,   "   : " ) ) . A p p l y I t a l i c   ( ) . T o S t r i n g   ( ) ;  
 	 	 	 	 r e s u l t [ 1 ]   =   n u l l ;  
 	 	 	 	 r e s u l t [ 2 ]   =   n u l l ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 r e s u l t [ 0 ]   =   t h i s . G e t D e s c r i p t i o n   ( r o w ) ;  
 	 	 	 	 r e s u l t [ 1 ]   =   t h i s . G e t T y p e   ( r o w ) ;  
 	 	 	 	 r e s u l t [ 2 ]   =   t h i s . G e t U s e d O p t i o n   ( r o w )   ?   " o u i "   :   " n o n " ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   r e s u l t ;  
 	 	 }  
  
 	 	 p r i v a t e   C o l o r [ ]   G e t R o w C o l o r s ( i n t   r o w )  
 	 	 {  
 	 	 	 v a r   r e s u l t   =   n e w   C o l o r [ 3 ] ;  
  
 	 	 	 i f   ( t h i s . I s T i t l e   ( r o w ) )  
 	 	 	 {  
 	 	 	 	 r e s u l t [ 0 ]   =   C o l o r . F r o m B r i g h t n e s s   ( 0 . 9 ) ;  
 	 	 	 	 r e s u l t [ 1 ]   =   C o l o r . F r o m B r i g h t n e s s   ( 0 . 9 ) ;  
 	 	 	 	 r e s u l t [ 2 ]   =   C o l o r . F r o m B r i g h t n e s s   ( 0 . 9 ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 r e s u l t [ 0 ]   =   C o l o r . E m p t y ;  
 	 	 	 	 r e s u l t [ 1 ]   =   C o l o r . E m p t y ;  
 	 	 	 	 r e s u l t [ 2 ]   =   C o l o r . E m p t y ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   r e s u l t ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e W i d g e t s ( )  
 	 	 {  
 	 	 	 i n t   c o u n t   =   0 ;  
 	 	 	 i n t   u s e d C o u n t   =   0 ;  
 	 	 	 i n t   s e l C o u n t   =   0 ;  
 	 	 	 i n t   s e l   =   - 1 ;  
  
 	 	 	 f o r   ( i n t   r o w   =   0 ;   r o w   <   t h i s . v i s i b l e O p t i o n s . C o u n t ;   r o w + + )  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . I s T i t l e   ( r o w ) )  
 	 	 	 	 {  
 	 	 	 	 	 c o n t i n u e ;  
 	 	 	 	 }  
  
 	 	 	 	 b o o l   s t a t e   =   t h i s . G e t U s e d O p t i o n   ( r o w ) ;  
  
 	 	 	 	 i f   ( s t a t e )  
 	 	 	 	 {  
 	 	 	 	 	 s e l C o u n t + + ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t a b l e . I s C e l l S e l e c t e d   ( r o w ,   0 ) )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( s t a t e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 u s e d C o u n t + + ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 c o u n t + + ;  
  
 	 	 	 	 	 i f   ( s e l   = =   - 1 )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s e l   =   r o w ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 b o o l   d i f f   =   ( c o u n t   >   1   & &   u s e d C o u n t   ! =   0   & &   c o u n t   ! =   u s e d C o u n t ) ;  
  
 	 	 	 i f   ( s e l   = =   - 1   | |   c o u n t   = =   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . d o c u m e n t T y p e s T e x t . T e x t   =   n u l l ;  
 	 	 	 	 t h i s . v a l u e D e s c r i p t i o n . T e x t     =   n u l l ;  
 	 	 	 	 t h i s . v a l u e T y p e T e x t . T e x t           =   n u l l ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( c o u n t   >   1 )  
 	 	 	 {  
 	 	 	 	 t h i s . d o c u m e n t T y p e s T e x t . F o r m a t t e d T e x t   =   T e x t F o r m a t t e r . F o r m a t T e x t   ( " S é l e c t i o n   m u l t i p l e " ) . A p p l y I t a l i c   ( ) ;  
 	 	 	 	 t h i s . v a l u e D e s c r i p t i o n . F o r m a t t e d T e x t     =   T e x t F o r m a t t e r . F o r m a t T e x t   ( " S é l e c t i o n   m u l t i p l e " ) . A p p l y I t a l i c   ( ) ;  
 	 	 	 	 t h i s . v a l u e T y p e T e x t . F o r m a t t e d T e x t           =   T e x t F o r m a t t e r . F o r m a t T e x t   ( " S é l e c t i o n   m u l t i p l e " ) . A p p l y I t a l i c   ( ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d o c u m e n t T y p e s T e x t . T e x t   =   t h i s . v i s i b l e O p t i o n s [ s e l ] . D o c u m e n t T y p e D e s c r i p t i o n ;  
 	 	 	 	 t h i s . v a l u e D e s c r i p t i o n . T e x t     =   t h i s . G e t L o n g D e s c r i p t i o n   ( s e l ) ;  
 	 	 	 	 t h i s . v a l u e T y p e T e x t . T e x t           =   t h i s . G e t V a l u e T y p e D e s c r i p t i o n   ( s e l ) ;  
 	 	 	 }  
  
 	 	 	 i f   ( c o u n t   = =   0   | |   t h i s . I s T i t l e   ( s e l ) )  
 	 	 	 {  
 	 	 	 	 t h i s . n o B u t t o n . E n a b l e     =   f a l s e ;  
 	 	 	 	 t h i s . y e s B u t t o n . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . n o B u t t o n . E n a b l e     =   t r u e ;  
 	 	 	 	 t h i s . y e s B u t t o n . E n a b l e   =   t r u e ;  
 	 	 	 }  
  
 	 	 	 i f   ( s e l   = =   - 1   | |   d i f f   | |   t h i s . I s T i t l e   ( s e l ) )  
 	 	 	 {  
 	 	 	 	 t h i s . n o B u t t o n . A c t i v e S t a t e     =   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . y e s B u t t o n . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 b o o l   s t a t e   =   t h i s . G e t U s e d O p t i o n   ( s e l ) ;  
 	 	 	 	 t h i s . n o B u t t o n . A c t i v e S t a t e     =   ! s t a t e   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . y e s B u t t o n . A c t i v e S t a t e   =     s t a t e   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 t h i s . n o n e B u t t o n . E n a b l e   =   ( s e l C o u n t   ! =   0 ) ;  
 	 	 	 t h i s . a l l B u t t o n . E n a b l e     =   ( s e l C o u n t   ! =   t h i s . v i s i b l e O p t i o n s . C o u n t ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   G e t D e s c r i p t i o n ( i n t   r o w )  
 	 	 {  
 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( t h i s . v i s i b l e O p t i o n s [ r o w ] . S h o r t D e s c r i p t i o n ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . v i s i b l e O p t i o n s [ r o w ] . S h o r t D e s c r i p t i o n ;  
 	 	 	 }  
  
 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( t h i s . v i s i b l e O p t i o n s [ r o w ] . T i t l e ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . v i s i b l e O p t i o n s [ r o w ] . T i t l e ;  
 	 	 	 }  
  
 	 	 	 s t r i n g   g r o u p   =   t h i s . v i s i b l e O p t i o n s [ r o w ] . G r o u p ;  
 	 	 	 r e t u r n   t h i s . v i s i b l e O p t i o n s . W h e r e   ( x   = >   x . G r o u p   = =   g r o u p ) . S e l e c t   ( x   = >   x . T i t l e ) . F i r s t O r D e f a u l t   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   G e t L o n g D e s c r i p t i o n ( i n t   r o w )  
 	 	 {  
 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( t h i s . v i s i b l e O p t i o n s [ r o w ] . L o n g D e s c r i p t i o n ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . v i s i b l e O p t i o n s [ r o w ] . L o n g D e s c r i p t i o n ;  
 	 	 	 }  
  
 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( t h i s . v i s i b l e O p t i o n s [ r o w ] . S h o r t D e s c r i p t i o n ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . v i s i b l e O p t i o n s [ r o w ] . S h o r t D e s c r i p t i o n ;  
 	 	 	 }  
  
 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( t h i s . v i s i b l e O p t i o n s [ r o w ] . T i t l e ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . v i s i b l e O p t i o n s [ r o w ] . T i t l e ;  
 	 	 	 }  
  
 	 	 	 s t r i n g   g r o u p   =   t h i s . v i s i b l e O p t i o n s [ r o w ] . G r o u p ;  
 	 	 	 r e t u r n   t h i s . v i s i b l e O p t i o n s . W h e r e   ( x   = >   x . G r o u p   = =   g r o u p ) . S e l e c t   ( x   = >   x . T i t l e ) . F i r s t O r D e f a u l t   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   G e t T y p e ( i n t   r o w )  
 	 	 {  
 	 	 	 s w i t c h   ( t h i s . v i s i b l e O p t i o n s [ r o w ] . T y p e )  
 	 	 	 {  
 	 	 	 	 c a s e   D o c u m e n t O p t i o n V a l u e T y p e . E n u m e r a t i o n :  
 	 	 	 	 	 r e t u r n   t h i s . v i s i b l e O p t i o n s [ r o w ] . I s B o o l e a n   ?   " b o o l "   :   " e n u m " ;  
  
 	 	 	 	 c a s e   D o c u m e n t O p t i o n V a l u e T y p e . D i s t a n c e :  
 	 	 	 	 c a s e   D o c u m e n t O p t i o n V a l u e T y p e . S i z e :  
 	 	 	 	 	 r e t u r n   " m m " ;  
  
 	 	 	 	 c a s e   D o c u m e n t O p t i o n V a l u e T y p e . T e x t :  
 	 	 	 	 c a s e   D o c u m e n t O p t i o n V a l u e T y p e . T e x t M u l t i l i n e :  
 	 	 	 	 	 r e t u r n   " t e x t e " ;  
  
 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 r e t u r n   n u l l ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   G e t V a l u e T y p e D e s c r i p t i o n ( i n t   r o w )  
 	 	 {  
 	 	 	 s w i t c h   ( t h i s . v i s i b l e O p t i o n s [ r o w ] . T y p e )  
 	 	 	 {  
 	 	 	 	 c a s e   D o c u m e n t O p t i o n V a l u e T y p e . E n u m e r a t i o n :  
 	 	 	 	 	 i f   ( t h i s . v i s i b l e O p t i o n s [ r o w ] . I s B o o l e a n )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   " V r a i   o u   f a u x " ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   s t r i n g . C o n c a t   ( " E n u m é r a t i o n   : < b r / > Ï%  " ,   s t r i n g . J o i n   ( " < b r / > Ï%  " ,   t h i s . v i s i b l e O p t i o n s [ r o w ] . E n u m e r a t i o n D e s c r i p t i o n ) ) ;  
 	 	 	 	 	 }  
  
 	 	 	 	 c a s e   D o c u m e n t O p t i o n V a l u e T y p e . D i s t a n c e :  
 	 	 	 	 	 r e t u r n   " D i s t a n c e   e n   m m " ;  
  
 	 	 	 	 c a s e   D o c u m e n t O p t i o n V a l u e T y p e . S i z e :  
 	 	 	 	 	 r e t u r n   " D i m e n s i o n   e n   m m " ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   n u l l ;  
 	 	 }  
  
  
 	 	 p r i v a t e   b o o l   I s T i t l e ( i n t   r o w )  
 	 	 {  
 	 	 	 i f   ( r o w   = =   - 1 )  
 	 	 	 {  
 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . v i s i b l e O p t i o n s [ r o w ] . I s T i t l e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S e t U s e d O p t i o n ( i n t   r o w ,   b o o l   v a l u e )  
 	 	 {  
 	 	 	 i f   ( t h i s . v i s i b l e O p t i o n s [ r o w ] . I s T i t l e )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 v a r   o p t i o n   =   t h i s . v i s i b l e O p t i o n s [ r o w ] . O p t i o n ;  
  
 	 	 	 i f   ( v a l u e )     / /   u t i l i s e   l ' o p t i o n   ?  
 	 	 	 {  
 	 	 	 	 i f   ( ! t h i s . G e t U s e d O p t i o n   ( r o w ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . o p t i o n s [ o p t i o n ]   =   t h i s . v i s i b l e O p t i o n s [ r o w ] . D e f a u l t V a l u e ;  
 	 	 	 	 	 t h i s . S e t D i r t y   ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 e l s e     / /   n ' u t i l i s e   p a s   l ' o p t i o n   ?  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . G e t U s e d O p t i o n   ( r o w ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . o p t i o n s [ o p t i o n ]   =   n u l l ;  
 	 	 	 	 	 t h i s . S e t D i r t y   ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   G e t U s e d O p t i o n ( i n t   r o w )  
 	 	 {  
 	 	 	 v a r   o p t i o n   =   t h i s . v i s i b l e O p t i o n s [ r o w ] . O p t i o n ;  
 	 	 	 r e t u r n   t h i s . o p t i o n s . C o n t a i n s O p t i o n   ( o p t i o n ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   D o c u m e n t T y p e   D o c u m e n t T y p e F i l t e r  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . d o c u m e n t T y p e F i l t e r ;  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . d o c u m e n t T y p e F i l t e r   ! =   v a l u e )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . d o c u m e n t T y p e F i l t e r   =   v a l u e ;  
 	 	 	 	 	 t h i s . U p d a t e V i s i b l e O p t i o n s   ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e V i s i b l e O p t i o n s ( )  
 	 	 {  
 	 	 	 t h i s . v i s i b l e O p t i o n s . C l e a r   ( ) ;  
 	 	 	 t h i s . v i s i b l e O p t i o n s . A d d R a n g e   ( t h i s . a l l O p t i o n s ) ;  
  
 	 	 	 i f   ( t h i s . d o c u m e n t T y p e F i l t e r   ! =   D o c u m e n t T y p e . N o n e )  
 	 	 	 {  
 	 	 	 	 v a r   o p t i o n s   =   E n t i t y P r i n t e r F a c t o r y R e s o l v e r . F i n d R e q u i r e d D o c u m e n t O p t i o n s   ( t h i s . d o c u m e n t T y p e F i l t e r ) ;  
  
 	 	 	 	 / / 	 O n   p a r t   d e   l a   l i s t e   c o m p l è t e ,   à   l a q u e l l e   o n   e n l è v e   l e s   o p t i o n s   i n u t i l e s .  
 	 	 	 	 / / 	 I l   r e s t e r a   d o n c   a u   m i n i m u n   t o u s   l e s   t i t r e s .  
 	 	 	 	 i n t   i   =   0 ;  
 	 	 	 	 w h i l e   ( i   <   t h i s . v i s i b l e O p t i o n s . C o u n t )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ! t h i s . v i s i b l e O p t i o n s [ i ] . I s T i t l e   & &  
 	 	 	 	 	 	 ( o p t i o n s   = =   n u l l   | |   ! o p t i o n s . C o n t a i n s   ( t h i s . v i s i b l e O p t i o n s [ i ] . O p t i o n ) ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . v i s i b l e O p t i o n s . R e m o v e A t   ( i ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 i + + ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 / / 	 O n   e n l è v e   l e s   t i t r e s   i n u t i l e s ,   c ' e s t - à - d i r e   s u i v i s   d ' u n   a u t r e   t i t r e  
 	 	 	 	 / / 	 o u   p l a c é s   à   l a   f i n .  
 	 	 	 	 i   =   0 ;  
 	 	 	 	 w h i l e   ( i   <   t h i s . v i s i b l e O p t i o n s . C o u n t )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t h i s . v i s i b l e O p t i o n s [ i ] . I s T i t l e   & &  
 	 	 	 	 	 	 ( i   = =   t h i s . v i s i b l e O p t i o n s . C o u n t - 1   | |   t h i s . v i s i b l e O p t i o n s [ i + 1 ] . I s T i t l e ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . v i s i b l e O p t i o n s . R e m o v e A t   ( i ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 i + + ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   S e t D i r t y ( )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t . N o t i f y E x t e r n a l C h a n g e s   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   r e a d o n l y   B u s i n e s s C o n t e x t 	 	 	 	 	 b u s i n e s s C o n t e x t ;  
 	 	 p r i v a t e   r e a d o n l y   D o c u m e n t O p t i o n s E n t i t y 	 	 	 	 d o c u m e n t O p t i o n s E n t i t y ;  
 	 	 p r i v a t e   r e a d o n l y   P r i n t i n g O p t i o n D i c t i o n a r y 	 	 	 o p t i o n s ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < V e r b o s e D o c u m e n t O p t i o n > 	 	 a l l O p t i o n s ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < V e r b o s e D o c u m e n t O p t i o n > 	 	 v i s i b l e O p t i o n s ;  
  
 	 	 p r i v a t e   C e l l T a b l e 	 	 	 	 	 	 	 	 	 t a b l e ;  
 	 	 p r i v a t e   S t a t i c T e x t 	 	 	 	 	 	 	 	 	 d o c u m e n t T y p e s T e x t ;  
 	 	 p r i v a t e   S t a t i c T e x t 	 	 	 	 	 	 	 	 	 v a l u e D e s c r i p t i o n ;  
 	 	 p r i v a t e   S t a t i c T e x t 	 	 	 	 	 	 	 	 	 v a l u e T y p e T e x t ;  
 	 	 p r i v a t e   R a d i o B u t t o n 	 	 	 	 	 	 	 	 	 n o B u t t o n ;  
 	 	 p r i v a t e   R a d i o B u t t o n 	 	 	 	 	 	 	 	 	 y e s B u t t o n ;  
 	 	 p r i v a t e   B u t t o n 	 	 	 	 	 	 	 	 	 	 n o n e B u t t o n ;  
 	 	 p r i v a t e   B u t t o n 	 	 	 	 	 	 	 	 	 	 a l l B u t t o n ;  
 	 	 p r i v a t e   D o c u m e n t T y p e 	 	 	 	 	 	 	 	 d o c u m e n t T y p e F i l t e r ;  
 	 }  
 }  
 