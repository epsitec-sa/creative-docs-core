ÿþ/ / 	 C o p y r i g h t   ©   2 0 1 0 ,   E P S I T E C   S A ,   C H - 1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
 u s i n g   E p s i t e c . C o m m o n . D i a l o g s ;  
  
 u s i n g   E p s i t e c . C r e s u s . C o r e . E n t i t i e s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . B u s i n e s s . F i n a n c e . P r i c e C a l c u l a t o r s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . W i d g e t s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . W i d g e t s . T i l e s ;  
  
 u s i n g   S y s t e m . T e x t . R e g u l a r E x p r e s s i o n s ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . T a b l e D e s i g n e r  
 {  
 	 p u b l i c   c l a s s   T a b l e C o n t r o l l e r  
 	 {  
 	 	 p u b l i c   T a b l e C o n t r o l l e r ( C o r e . B u s i n e s s . B u s i n e s s C o n t e x t   b u s i n e s s C o n t e x t ,   D e s i g n e r T a b l e   t a b l e )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t   =   b u s i n e s s C o n t e x t ;  
 	 	 	 t h i s . t a b l e                       =   t a b l e ;  
  
 	 	 	 t h i s . c o l u m n D i m e n s i o n S e l e c t e d   =   0 ;  
 	 	 	 t h i s . r o w D i m e n s i o n S e l e c t e d         =   1 ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   C r e a t e U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   f r a m e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 1 0 ) ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . d i m e n s i o n s P a n e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   f r a m e ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   2 4 + 2 4 + 1 5 0 + 1 0 0 + 1 0 + 1 + W i d g e t s . T i l e s . T i l e A r r o w . B r e a d t h ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   1 0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   r i g h t P a n e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   f r a m e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . t o o l b a r   =   n e w   H T o o l B a r  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   r i g h t P a n e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   2 ) ,  
 	 	 	 	 V i s i b i l i t y   =   f a l s e ,  
 	 	 	 } ;  
  
 	 	 	 v a r   v a l u e s P a n e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   r i g h t P a n e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . C r e a t e D i m e n s i o n s T a b l e U I   ( t h i s . d i m e n s i o n s P a n e ) ;  
 	 	 	 t h i s . C r e a t e V a l u e s T a b l e U I   ( v a l u e s P a n e ) ;  
 	 	 	 t h i s . C r e a t e T o o l b a r U I   ( t h i s . t o o l b a r ) ;  
  
 	 	 	 t h i s . U p d a t e   ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   U p d a t e ( )  
 	 	 {  
 	 	 	 i f   ( t h i s . t a b l e . D i m e n s i o n s . C o u n t   = =   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . c o l u m n D i m e n s i o n S e l e c t e d   =   - 1 ;  
 	 	 	 	 t h i s . r o w D i m e n s i o n S e l e c t e d         =   - 1 ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( t h i s . t a b l e . D i m e n s i o n s . C o u n t   = =   1 )  
 	 	 	 {  
 	 	 	 	 / / 	 A v e c   u n e   s e u l e   d i m e n s i o n ,   o n   p r é f è r e   u t i l i s e r   l e s   l i g n e s .  
 	 	 	 	 t h i s . c o l u m n D i m e n s i o n S e l e c t e d   =   - 1 ;  
  
 	 	 	 	 i f   ( t h i s . r o w D i m e n s i o n S e l e c t e d   > =   t h i s . t a b l e . D i m e n s i o n s . C o u n t )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . r o w D i m e n s i o n S e l e c t e d   =   t h i s . t a b l e . D i m e n s i o n s . C o u n t - 1 ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . c o l u m n D i m e n s i o n S e l e c t e d   > =   t h i s . t a b l e . D i m e n s i o n s . C o u n t )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . c o l u m n D i m e n s i o n S e l e c t e d   =   t h i s . t a b l e . D i m e n s i o n s . C o u n t - 1 ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . r o w D i m e n s i o n S e l e c t e d   > =   t h i s . t a b l e . D i m e n s i o n s . C o u n t )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . r o w D i m e n s i o n S e l e c t e d   =   t h i s . t a b l e . D i m e n s i o n s . C o u n t - 1 ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e D i m e n s i o n s T a b l e   ( ) ;  
 	 	 	 t h i s . R e f r e s h D i m e n s i o n s T a b l e   ( ) ;  
  
 	 	 	 t h i s . U p d a t e V a l u e s T a b l e   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   C r e a t e D i m e n s i o n s T a b l e U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   t i l e   =   n e w   A r r o w e d T i l e F r a m e   ( D i r e c t i o n . R i g h t )  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 P a d d i n g   =   W i d g e t s . T i l e s . T i l e A r r o w . G e t C o n t a i n e r P a d d i n g   ( D i r e c t i o n . R i g h t )   +   n e w   M a r g i n s   ( 5 ) ,  
 	 	 	 } ;  
  
 	 	 	 t i l e . S e t S e l e c t e d   ( t r u e ) ;     / /   c o n t e n e u r   o r a n g e  
  
 	 	 	 t h i s . d i m e n s i o n s T a b l e   =   n e w   C e l l T a b l e  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t i l e ,  
 	 	 	 	 S t y l e H   =   C e l l A r r a y S t y l e s . S c r o l l M a g i c   |   C e l l A r r a y S t y l e s . S e p a r a t o r   |   C e l l A r r a y S t y l e s . H e a d e r ,  
 	 	 	 	 S t y l e V   =   C e l l A r r a y S t y l e s . S c r o l l M a g i c   |   C e l l A r r a y S t y l e s . S e p a r a t o r ,  
 	 	 	 	 D e f H e i g h t   =   2 4 ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C r e a t e V a l u e s T a b l e U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   l e f t   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   T a b l e C o n t r o l l e r . l e g e n d H e i g h t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   - 1 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   s w a p B o x   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   l e f t ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( T a b l e C o n t r o l l e r . l e g e n d H e i g h t - 3 ,   T a b l e C o n t r o l l e r . l e g e n d H e i g h t - 3 ) ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   3 ,   0 ,   2 ) ,  
 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 4 ,   1 ,   4 ,   1 ) ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . s w a p B u t t o n   =   n e w   G l y p h B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   s w a p B o x ,  
 	 	 	 	 B u t t o n S t y l e   =   B u t t o n S t y l e . S l i d e r ,  
 	 	 	 	 G l y p h S h a p e   =   G l y p h S h a p e . H o r i z o n t a l M o v e ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . s w a p B u t t o n ,   " P e r m u t e   l e s   l i g n e s   e t   l e s   c o l o n n e s " ) ;  
  
 	 	 	 v a r   r o w s H e a d e r   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   l e f t ,  
 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 B a c k C o l o r   =   W i d g e t s . T i l e s . T i l e C o l o r s . S u r f a c e S e l e c t e d C o n t a i n e r C o l o r s . F i r s t   ( ) ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   T a b l e C o n t r o l l e r . l e g e n d H e i g h t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 v a r   r i g h t   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 v a r   c o l u m n s H e a d e r   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   r i g h t ,  
 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 B a c k C o l o r   =   W i d g e t s . T i l e s . T i l e C o l o r s . S u r f a c e S e l e c t e d C o n t a i n e r C o l o r s . F i r s t   ( ) ,  
 	 	 	 	 P r e f e r r e d H e i g h t   =   T a b l e C o n t r o l l e r . l e g e n d H e i g h t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   - 1 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   t a b l e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   r i g h t ,  
 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 B a c k C o l o r   =   W i d g e t s . T i l e s . T i l e C o l o r s . S u r f a c e S e l e c t e d C o n t a i n e r C o l o r s . F i r s t   ( ) ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 5 ) ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . v a l u e s T a b l e   =   n e w   C e l l T a b l e  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t a b l e ,  
 	 	 	 	 D e f H e i g h t   =   2 4 ,  
 	 	 	 	 H e a d e r W i d t h   =   8 0 ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 / / 	 M e t   l e s   t e x t e s   s t a t i q u e s   d e s   l é g e n d e s   d a n s   l e s   b a n d e s   c o r r e s p o n d a n t e s .  
 	 	 	 t h i s . r o w s L e g e n d   =   n e w   V S t a t i c T e x t  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   r o w s H e a d e r ,  
 	 	 	 	 C o n t e n t A l i g n m e n t   =   C o m m o n . D r a w i n g . C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . c o l u m n s L e g e n d   =   n e w   S t a t i c T e x t  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   c o l u m n s H e a d e r ,  
 	 	 	 	 C o n t e n t A l i g n m e n t   =   C o m m o n . D r a w i n g . C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
  
 	 	 	 t h i s . d i m e n s i o n s B u t t o n   =   n e w   G l y p h B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   r o w s H e a d e r ,  
 	 	 	 	 B u t t o n S t y l e   =   B u t t o n S t y l e . S l i d e r ,  
 	 	 	 	 G l y p h S h a p e   =   G l y p h S h a p e . T r i a n g l e L e f t ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( 2 0 ,   2 0 ) ,  
 	 	 	 	 A n c h o r   =   A n c h o r S t y l e s . B o t t o m L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 4 ,   0 ,   0 ,   4 ) ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . t o o l b a r B u t t o n   =   n e w   G l y p h B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   c o l u m n s H e a d e r ,  
 	 	 	 	 B u t t o n S t y l e   =   B u t t o n S t y l e . S l i d e r ,  
 	 	 	 	 G l y p h S h a p e   =   G l y p h S h a p e . T r i a n g l e D o w n ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( 2 0 ,   2 0 ) ,  
 	 	 	 	 A n c h o r   =   A n c h o r S t y l e s . T o p R i g h t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   4 ,   4 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . d i m e n s i o n s B u t t o n ,   " M o n t r e   o u   c a c h e   l a   l i s t e   d e s   a x e s " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . t o o l b a r B u t t o n ,         " M o n t r e   o u   c a c h e   l a   b a r r e   d ' o u t i l s " ) ;  
  
 	 	 	 / / 	 C o n n e x i o n   d e s   é v é n e m e n t s .  
 	 	 	 t h i s . s w a p B u t t o n . C l i c k e d   + =   n e w   E v e n t H a n d l e r < M e s s a g e E v e n t A r g s >   ( t h i s . H a n d l e S w a p B u t t o n C l i c k e d ) ;  
  
 	 	 	 t h i s . d i m e n s i o n s B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . d i m e n s i o n s P a n e . V i s i b i l i t y   =   ! t h i s . d i m e n s i o n s P a n e . V i s i b i l i t y ;  
 	 	 	 	 t h i s . d i m e n s i o n s B u t t o n . G l y p h S h a p e   =   t h i s . d i m e n s i o n s P a n e . V i s i b i l i t y   ?   G l y p h S h a p e . T r i a n g l e L e f t   :   G l y p h S h a p e . T r i a n g l e R i g h t ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . t o o l b a r B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . t o o l b a r . V i s i b i l i t y   =   ! t h i s . t o o l b a r . V i s i b i l i t y ;  
 	 	 	 	 t h i s . t o o l b a r B u t t o n . G l y p h S h a p e   =   t h i s . t o o l b a r . V i s i b i l i t y   ?   G l y p h S h a p e . T r i a n g l e U p   :   G l y p h S h a p e . T r i a n g l e D o w n ;  
 	 	 	 } ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C r e a t e T o o l b a r U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   c u t B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " C l i p b o a r d . C u t " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   c o p y B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " C l i p b o a r d . C o p y " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   p a s t e B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " C l i p b o a r d . P a s t e " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   1 5 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
  
 	 	 	 v a r   e x p o r t B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " E x p o r t " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   i m p o r t B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " I m p o r t " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
  
 	 	 	 n e w   S e p a r a t o r  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   1 ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 2 0 ,   2 0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
  
 	 	 	 v a r   d e s e l e c t A l l B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " T a b l e . D e s e l e c t A l l " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   s e l e c t A l l B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " T a b l e . S e l e c t A l l " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   s e l e c t C o l u m n B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " T a b l e . S e l e c t C o l u m n " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   s e l e c t R o w B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " T a b l e . S e l e c t R o w " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   s e l e c t I n v e r t B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " T a b l e . S e l e c t I n v e r t " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   1 5 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
  
 	 	 	 v a r   v a l u e   =   n e w   T e x t F i e l d  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 T e x t   =   " 1 " ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   5 0 ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   - 1 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   o p e r F r a m e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   1 5 ,   0 ,   0 ) ,  
 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 2 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   e q u a l B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   o p e r F r a m e ,  
 	 	 	 	 T e x t   =   " = " ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( 1 8 ,   1 8 ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 } ;  
  
 	 	 	 v a r   a d d B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   o p e r F r a m e ,  
 	 	 	 	 T e x t   =   " + " ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( 1 8 ,   1 8 ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 } ;  
  
 	 	 	 v a r   s u b B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   o p e r F r a m e ,  
 	 	 	 	 T e x t   =   " "" ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( 1 8 ,   1 8 ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 } ;  
  
 	 	 	 v a r   m u l B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   o p e r F r a m e ,  
 	 	 	 	 T e x t   =   " × " ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( 1 8 ,   1 8 ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 } ;  
  
 	 	 	 v a r   d i v B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   o p e r F r a m e ,  
 	 	 	 	 T e x t   =   " ÷ " ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( 1 8 ,   1 8 ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 } ;  
  
  
 	 	 	 v a r   c l e a r B u t t o n   =   n e w   I c o n B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 I c o n U r i   =   M i s c . I c o n P r o v i d e r . G e t R e s o u r c e I c o n U r i   ( " D e l e t e " ) ,  
 	 	 	 	 A u t o F o c u s   =   f a l s e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( c u t B u t t o n ,                     " D é p l a c e   t o u t e   l a   t a b e l l e   d a n s   l e   b l o c - n o t e s " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( c o p y B u t t o n ,                   " C o p i e   t o u t e   l a   t a b e l l e   d a n s   l e   b l o c - n o t e s " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( p a s t e B u t t o n ,                 " C o l l e   l e   c o n t e n u   d u   b l o c - n o t e s   d a n s   l a   t a b e l l e " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( e x p o r t B u t t o n ,               " E x p o r t e   l a   t a b e l l e   d e   p r i x   d a n s   u n   f i c h i e r   t e x t e   t a b u l é " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( i m p o r t B u t t o n ,               " I m p o r t e   l a   t a b e l l e   d e   p r i x   à   p a r t i r   d ' u n   f i c h i e r   t e x t e   t a b u l é " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( d e s e l e c t A l l B u t t o n ,     " D é s é l e c t i o n n e   t o u t " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( s e l e c t A l l B u t t o n ,         " S é l e c t i o n n e   t o u t " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( s e l e c t C o l u m n B u t t o n ,   " S é l e c t i o n n e   t o u t e   l a   c o l o n n e " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( s e l e c t R o w B u t t o n ,         " S é l e c t i o n n e   t o u t e   l a   l i g n e " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( s e l e c t I n v e r t B u t t o n ,   " I n v e r s e   l a   s é l e c t i o n " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( v a l u e ,                             " V a l e u r   p o u r   m o d i f i e r   l e s   p r i x   d e   l a   t a b e l l e " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( e q u a l B u t t o n ,                 " A s s i g n e   l a   v a l e u r   d a n s   l a   t a b e l l e " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( a d d B u t t o n ,                     " A u g m e n t e   l e s   p r i x   d e   l a   t a b e l l e   d e   l a   v a l e u r " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( s u b B u t t o n ,                     " D i m i n u e   l e s   p r i x   d e   l a   t a b e l l e   d e   l a   v a l e u r " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( m u l B u t t o n ,                     " M u l t i p l i e   l e s   p r i x   d e   l a   t a b e l l e   p a r   l a   v a l e u r " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( d i v B u t t o n ,                     " D i v i s e   l e s   p r i x   d e   l a   t a b e l l e   p a r   l a   v a l e u r " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( c l e a r B u t t o n ,                 " E f f a c e   l e s   p r i x " ) ;  
  
 	 	 	 / / 	 C o n n e x i o n   d e s   é v é n e m e n t s .  
 	 	 	 c l e a r B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . C l e a r   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 c u t B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . C l i p b o a r d C u t   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 c o p y B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . C l i p b o a r d C o p y   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 p a s t e B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . C l i p b o a r d P a s t e   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 e x p o r t B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . E x p o r t   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 i m p o r t B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . I m p o r t   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 d e s e l e c t A l l B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . D e s e l e c t A l l   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 s e l e c t A l l B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . S e l e c t A l l   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 s e l e c t C o l u m n B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . S e l e c t C o l u m n   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 s e l e c t R o w B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . S e l e c t R o w   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 s e l e c t I n v e r t B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . S e l e c t I n v e r t   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 e q u a l B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 d e c i m a l   d ;  
 	 	 	 	 i f   ( d e c i m a l . T r y P a r s e   ( v a l u e . T e x t ,   o u t   d ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . A s s i g n   ( d ) ;  
 	 	 	 	 }  
 	 	 	 } ;  
  
 	 	 	 a d d B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 d e c i m a l   d ;  
 	 	 	 	 i f   ( d e c i m a l . T r y P a r s e   ( v a l u e . T e x t ,   o u t   d )   & &   d   ! =   0 )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C o m p u t e   ( d ,   1 ) ;  
 	 	 	 	 }  
 	 	 	 } ;  
  
 	 	 	 s u b B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 d e c i m a l   d ;  
 	 	 	 	 i f   ( d e c i m a l . T r y P a r s e   ( v a l u e . T e x t ,   o u t   d )   & &   d   ! =   0 )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C o m p u t e   ( - d ,   1 ) ;  
 	 	 	 	 }  
 	 	 	 } ;  
  
 	 	 	 m u l B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 d e c i m a l   d ;  
 	 	 	 	 i f   ( d e c i m a l . T r y P a r s e   ( v a l u e . T e x t ,   o u t   d )   & &   d   ! =   1 )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C o m p u t e   ( 0 ,   d ) ;  
 	 	 	 	 }  
 	 	 	 } ;  
  
 	 	 	 d i v B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 d e c i m a l   d ;  
 	 	 	 	 i f   ( d e c i m a l . T r y P a r s e   ( v a l u e . T e x t ,   o u t   d )   & &   d   ! =   1   & &   d   ! =   0 )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C o m p u t e   ( 0 ,   1 M / d ) ;  
 	 	 	 	 }  
 	 	 	 } ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   U p d a t e D i m e n s i o n s T a b l e ( )  
 	 	 {  
 	 	 	 i n t   c o u n t   =   t h i s . t a b l e . D i m e n s i o n s . C o u n t ;  
  
 	 	 	 t h i s . d i m e n s i o n s B u t t o n . V i s i b i l i t y   =   ( c o u n t   >   2 ) ;     / /   i n u t i l e   a v e c   m o i n s   d e   3   d i m e n s i o n s   !  
 	 	 	 t h i s . d i m e n s i o n s P a n e . V i s i b i l i t y       =   ( c o u n t   >   2 ) ;     / /   i n u t i l e   a v e c   m o i n s   d e   3   d i m e n s i o n s   !  
  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t A r r a y S i z e   ( 4 ,   c o u n t ) ;  
  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t W i d t h C o l u m n   ( 0 ,   2 4 ) ;  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t W i d t h C o l u m n   ( 1 ,   2 4 ) ;  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t W i d t h C o l u m n   ( 2 ,   1 5 0 ) ;  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t W i d t h C o l u m n   ( 3 ,   1 0 0 ) ;  
  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t H e a d e r T e x t H   ( 0 ,   " C " ) ;  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t H e a d e r T e x t H   ( 1 ,   " L " ) ;  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t H e a d e r T e x t H   ( 2 ,   " A x e " ) ;  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t H e a d e r T e x t H   ( 3 ,   " V a l e u r " ) ;  
  
 	 	 	 i n t   t a b I n d e x   =   1 ;  
  
 	 	 	 f o r   ( i n t   r o w   =   0 ;   r o w   <   c o u n t ;   r o w + + )  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . d i m e n s i o n s T a b l e [ 0 ,   r o w ] . I s E m p t y )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   r a d i o   =   n e w   R a d i o B u t t o n  
 	 	 	 	 	 {  
 	 	 	 	 	 	 N a m e   =   s t r i n g . C o n c a t   ( " C " ,   r o w . T o S t r i n g   ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ) ,  
 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 5 ,   0 ,   0 ,   0 ) ,  
 	 	 	 	 	 } ;  
  
 	 	 	 	 	 r a d i o . C l i c k e d   + =   n e w   E v e n t H a n d l e r < M e s s a g e E v e n t A r g s >   ( t h i s . H a n d l e R a d i o C l i c k e d ) ;  
  
 	 	 	 	 	 t h i s . d i m e n s i o n s T a b l e [ 0 ,   r o w ] . I n s e r t   ( r a d i o ) ;  
 	 	 	 	 	 t h i s . d i m e n s i o n s T a b l e [ 0 ,   r o w ] . T a b I n d e x   =   t a b I n d e x + + ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . d i m e n s i o n s T a b l e [ 1 ,   r o w ] . I s E m p t y )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   r a d i o   =   n e w   R a d i o B u t t o n  
 	 	 	 	 	 {  
 	 	 	 	 	 	 N a m e   =   s t r i n g . C o n c a t   ( " R " ,   r o w . T o S t r i n g   ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ) ,  
 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 5 ,   0 ,   0 ,   0 ) ,  
 	 	 	 	 	 } ;  
  
 	 	 	 	 	 r a d i o . C l i c k e d   + =   n e w   E v e n t H a n d l e r < M e s s a g e E v e n t A r g s >   ( t h i s . H a n d l e R a d i o C l i c k e d ) ;  
  
 	 	 	 	 	 t h i s . d i m e n s i o n s T a b l e [ 1 ,   r o w ] . I n s e r t   ( r a d i o ) ;  
 	 	 	 	 	 t h i s . d i m e n s i o n s T a b l e [ 1 ,   r o w ] . T a b I n d e x   =   t a b I n d e x + + ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . d i m e n s i o n s T a b l e [ 2 ,   r o w ] . I s E m p t y )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   l a b e l   =   n e w   S t a t i c T e x t  
 	 	 	 	 	 {  
 	 	 	 	 	 	 C o n t e n t A l i g n m e n t   =   C o m m o n . D r a w i n g . C o n t e n t A l i g n m e n t . M i d d l e L e f t ,  
 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 5 ,   5 ,   0 ,   0 ) ,  
 	 	 	 	 	 } ;  
  
 	 	 	 	 	 t h i s . d i m e n s i o n s T a b l e [ 2 ,   r o w ] . I n s e r t   ( l a b e l ) ;  
 	 	 	 	 	 t h i s . d i m e n s i o n s T a b l e [ 2 ,   r o w ] . T a b I n d e x   =   t a b I n d e x + + ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( ! t h i s . d i m e n s i o n s T a b l e [ 2 ,   r o w ] . I s E m p t y )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   l a b e l   =   t h i s . d i m e n s i o n s T a b l e [ 2 ,   r o w ] . C h i l d r e n [ 0 ]   a s   S t a t i c T e x t ;  
  
 	 	 	 	 	 l a b e l . F o r m a t t e d T e x t   =   t h i s . t a b l e . D i m e n s i o n s [ r o w ] . N i c e D e s c r i p t i o n ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . d i m e n s i o n s T a b l e [ 3 ,   r o w ] . I s E m p t y )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   c o m b o   =   n e w   T e x t F i e l d C o m b o  
 	 	 	 	 	 {  
 	 	 	 	 	 	 N a m e   =   r o w . T o S t r i n g   ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ,  
 	 	 	 	 	 	 I s R e a d O n l y   =   t r u e ,  
 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( - 1 ) ,  
 	 	 	 	 	 	 T a b I n d e x   =   t a b I n d e x + + ,  
 	 	 	 	 	 } ;  
  
 	 	 	 	 	 c o m b o . C o m b o C l o s e d   + =   n e w   E v e n t H a n d l e r   ( t h i s . H a n d l e C o m b o C l o s e d ) ;  
  
 	 	 	 	 	 t h i s . d i m e n s i o n s T a b l e [ 3 ,   r o w ] . I n s e r t   ( c o m b o ) ;  
 	 	 	 	 	 t h i s . d i m e n s i o n s T a b l e [ 3 ,   r o w ] . T a b I n d e x   =   t a b I n d e x + + ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( ! t h i s . d i m e n s i o n s T a b l e [ 3 ,   r o w ] . I s E m p t y )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   c o m b o   =   t h i s . d i m e n s i o n s T a b l e [ 3 ,   r o w ] . C h i l d r e n [ 0 ]   a s   T e x t F i e l d C o m b o ;  
  
 	 	 	 	 	 t h i s . U p d a t e C o m b o   ( c o m b o ,   r o w ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e C o m b o ( T e x t F i e l d C o m b o   c o m b o ,   i n t   r o w )  
 	 	 {  
 	 	 	 c o m b o . I t e m s . C l e a r   ( ) ;  
  
 	 	 	 f o r e a c h   ( v a r   p o i n t   i n   t h i s . t a b l e . D i m e n s i o n s [ r o w ] . P o i n t s )  
 	 	 	 {  
 	 	 	 	 c o m b o . I t e m s . A d d   ( p o i n t ) ;  
 	 	 	 }  
  
 	 	 	 c o m b o . T e x t   =   t h i s . t a b l e . D i m e n s i o n s [ r o w ] . P o i n t s . F i r s t O r D e f a u l t   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   R e f r e s h D i m e n s i o n s T a b l e ( )  
 	 	 {  
 	 	 	 i n t   c o u n t   =   t h i s . t a b l e . D i m e n s i o n s . C o u n t ;  
  
 	 	 	 f o r   ( i n t   r o w   =   0 ;   r o w   <   c o u n t ;   r o w + + )  
 	 	 	 {  
 	 	 	 	 v a r   r a d i o X   =   t h i s . d i m e n s i o n s T a b l e [ 0 ,   r o w ] . C h i l d r e n [ 0 ]   a s   R a d i o B u t t o n ;  
 	 	 	 	 v a r   r a d i o Y   =   t h i s . d i m e n s i o n s T a b l e [ 1 ,   r o w ] . C h i l d r e n [ 0 ]   a s   R a d i o B u t t o n ;  
 	 	 	 	 v a r   c o m b o     =   t h i s . d i m e n s i o n s T a b l e [ 3 ,   r o w ] . C h i l d r e n [ 0 ]   a s   T e x t F i e l d C o m b o ;  
  
 	 	 	 	 r a d i o X . A c t i v e S t a t e   =   ( t h i s . c o l u m n D i m e n s i o n S e l e c t e d   = =   r o w )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 r a d i o Y . A c t i v e S t a t e   =   ( t h i s . r o w D i m e n s i o n S e l e c t e d         = =   r o w )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
  
 	 	 	 	 c o m b o . V i s i b i l i t y   =   ( t h i s . c o l u m n D i m e n s i o n S e l e c t e d   ! =   r o w   & &   t h i s . r o w D i m e n s i o n S e l e c t e d   ! =   r o w ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   U p d a t e V a l u e s T a b l e ( )  
 	 	 {  
 	 	 	 i n t   c o u n t   =   t h i s . t a b l e . D i m e n s i o n s . C o u n t ;  
  
 	 	 	 t h i s . s w a p B u t t o n . V i s i b i l i t y   =   ( c o u n t   >   1 ) ;     / /   i n u t i l e   a v e c   u n e   s e u l e   d i m e n s i o n   !  
  
 	 	 	 i f   ( c o u n t   = =   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . v a l u e s T a b l e . S t y l e H   =   C e l l A r r a y S t y l e s . S e p a r a t o r ;  
 	 	 	 	 t h i s . v a l u e s T a b l e . S t y l e V   =   C e l l A r r a y S t y l e s . S e p a r a t o r ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( c o u n t   = =   1 )  
 	 	 	 {  
 	 	 	 	 t h i s . v a l u e s T a b l e . S t y l e H   =   C e l l A r r a y S t y l e s . S e p a r a t o r ;  
 	 	 	 	 t h i s . v a l u e s T a b l e . S t y l e V   =   C e l l A r r a y S t y l e s . S c r o l l N o r m   |   C e l l A r r a y S t y l e s . H e a d e r   |   C e l l A r r a y S t y l e s . S e p a r a t o r   |   C e l l A r r a y S t y l e s . S e l e c t C e l l   |   C e l l A r r a y S t y l e s . S e l e c t M u l t i ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . v a l u e s T a b l e . S t y l e H   =   C e l l A r r a y S t y l e s . S c r o l l N o r m   |   C e l l A r r a y S t y l e s . H e a d e r   |   C e l l A r r a y S t y l e s . S e p a r a t o r   |   C e l l A r r a y S t y l e s . S e l e c t C e l l   |   C e l l A r r a y S t y l e s . S e l e c t M u l t i ;  
 	 	 	 	 t h i s . v a l u e s T a b l e . S t y l e V   =   C e l l A r r a y S t y l e s . S c r o l l N o r m   |   C e l l A r r a y S t y l e s . H e a d e r   |   C e l l A r r a y S t y l e s . S e p a r a t o r   |   C e l l A r r a y S t y l e s . S e l e c t C e l l   |   C e l l A r r a y S t y l e s . S e l e c t M u l t i ;  
 	 	 	 }  
  
 	 	 	 v a r   c o l u m n s D i m e n s i o n   =   ( t h i s . c o l u m n D i m e n s i o n S e l e c t e d   = =   - 1 )   ?   n u l l   :   t h i s . t a b l e . D i m e n s i o n s [ t h i s . c o l u m n D i m e n s i o n S e l e c t e d ] ;  
 	 	 	 v a r   r o w s D i m e n s i o n         =   ( t h i s . r o w D i m e n s i o n S e l e c t e d         = =   - 1 )   ?   n u l l   :   t h i s . t a b l e . D i m e n s i o n s [ t h i s . r o w D i m e n s i o n S e l e c t e d ] ;  
  
 	 	 	 t h i s . c o l u m n s L e g e n d . F o r m a t t e d T e x t   =   ( c o l u m n s D i m e n s i o n   = =   n u l l )   ?   F o r m a t t e d T e x t . N u l l   :   T a b l e C o n t r o l l e r . G e t L e g e n d T e x t   ( c o l u m n s D i m e n s i o n . N i c e D e s c r i p t i o n ) ;  
 	 	 	 t h i s . r o w s L e g e n d . F o r m a t t e d T e x t         =   ( r o w s D i m e n s i o n         = =   n u l l )   ?   F o r m a t t e d T e x t . N u l l   :   T a b l e C o n t r o l l e r . G e t L e g e n d T e x t   ( r o w s D i m e n s i o n . N i c e D e s c r i p t i o n ) ;  
  
 	 	 	 i n t   t o t a l C o l u m n s   =   ( c o l u m n s D i m e n s i o n   = =   n u l l )   ?   1   :   c o l u m n s D i m e n s i o n . P o i n t s . C o u n t   ( ) ;  
 	 	 	 i n t   t o t a l R o w s         =   ( r o w s D i m e n s i o n         = =   n u l l )   ?   1   :   r o w s D i m e n s i o n . P o i n t s . C o u n t   ( ) ;  
  
 	 	 	 t h i s . v a l u e s T a b l e . S e t A r r a y S i z e   ( t o t a l C o l u m n s ,   t o t a l R o w s ) ;  
  
 	 	 	 f o r   ( i n t   i   =   0 ;   i   <   t o t a l C o l u m n s ;   i + + )  
 	 	 	 {  
 	 	 	 	 t h i s . v a l u e s T a b l e . S e t W i d t h C o l u m n   ( i ,   1 0 0 ) ;  
 	 	 	 	 t h i s . v a l u e s T a b l e . S e t H e a d e r T e x t H   ( i ,   ( c o l u m n s D i m e n s i o n   = =   n u l l )   ?   n u l l   :   c o l u m n s D i m e n s i o n . P o i n t s [ i ] ) ;  
 	 	 	 }  
  
 	 	 	 f o r   ( i n t   i   =   0 ;   i   <   t o t a l R o w s ;   i + + )  
 	 	 	 {  
 	 	 	 	 t h i s . v a l u e s T a b l e . S e t H e a d e r T e x t V   ( i ,   ( r o w s D i m e n s i o n   = =   n u l l )   ?   n u l l   :   r o w s D i m e n s i o n . P o i n t s [ i ] ) ;  
 	 	 	 }  
  
 	 	 	 i f   ( c o u n t   = =   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . v a l u e s T a b l e . S e t A r r a y S i z e   ( 0 ,   0 ) ;  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 f o r   ( i n t   r o w   =   0 ;   r o w   <   t o t a l R o w s ;   r o w + + )  
 	 	 	 {  
 	 	 	 	 f o r   ( i n t   c o l u m n   =   0 ;   c o l u m n   <   t o t a l C o l u m n s ;   c o l u m n + + )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t h i s . v a l u e s T a b l e [ c o l u m n ,   r o w ] . I s E m p t y )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 v a r   f i e l d   =   n e w   T e x t F i e l d N a v i g a t o r  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 N a m e   =   s t r i n g . C o n c a t   ( c o l u m n . T o S t r i n g   ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ,   " . " ,   r o w . T o S t r i n g   ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ) ,  
 	 	 	 	 	 	 	 C o n t e n t A l i g n m e n t   =   C o m m o n . D r a w i n g . C o n t e n t A l i g n m e n t . M i d d l e R i g h t ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 1 0 ,   1 0 ,   1 ,   1 ) ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 f i e l d . T e x t C h a n g e d   + =   n e w   E v e n t H a n d l e r   ( t h i s . H a n d l e F i e l d T e x t C h a n g e d ) ;  
 	 	 	 	 	 	 f i e l d . N a v i g a t e   + =   n e w   T e x t F i e l d N a v i g a t o r . N a v i g a t o r H a n d l e r   ( t h i s . H a n d l e F i e l d N a v i g a t e ) ;  
 	 	 	 	 	 	 f i e l d . I s F o c u s e d C h a n g e d   + =   n e w   E v e n t H a n d l e r < D e p e n d e n c y P r o p e r t y C h a n g e d E v e n t A r g s >   ( t h i s . H a n d l e F i e l d I s F o c u s e d C h a n g e d ) ;  
  
 	 	 	 	 	 	 t h i s . v a l u e s T a b l e [ c o l u m n ,   r o w ] . I n s e r t   ( f i e l d ) ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 i f   ( ! t h i s . v a l u e s T a b l e [ c o l u m n ,   r o w ] . I s E m p t y )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 v a r   f i e l d   =   t h i s . v a l u e s T a b l e [ c o l u m n ,   r o w ] . C h i l d r e n [ 0 ]   a s   T e x t F i e l d N a v i g a t o r ;  
  
 	 	 	 	 	 	 t h i s . i g n o r e C h a n g e   =   t r u e ;  
 	 	 	 	 	 	 f i e l d . T e x t   =   t h i s . G e t V a l u e   ( t h i s . G e t K e y   ( c o l u m n ,   r o w ) ) ;  
 	 	 	 	 	 	 t h i s . i g n o r e C h a n g e   =   f a l s e ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S e t F o c u s I n V a l u e s T a b l e ( i n t   c o l u m n ,   i n t   r o w )  
 	 	 {  
 	 	 	 / / 	 D é p l a c e   l e   f o c u s   d a n s   u n   c e l l u l e   à   c h o i x .  
 	 	 	 i f   ( c o l u m n   <   0 )  
 	 	 	 {  
 	 	 	 	 c o l u m n   =   t h i s . v a l u e s T a b l e . C o l u m n s - 1 ;  
 	 	 	 	 r o w - - ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( c o l u m n   > =   t h i s . v a l u e s T a b l e . C o l u m n s )  
 	 	 	 {  
 	 	 	 	 c o l u m n   =   0 ;  
 	 	 	 	 r o w + + ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( r o w   <   0 )  
 	 	 	 {  
 	 	 	 	 r o w   =   t h i s . v a l u e s T a b l e . R o w s - 1 ;  
 	 	 	 	 c o l u m n - - ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( r o w   > =   t h i s . v a l u e s T a b l e . R o w s )  
 	 	 	 {  
 	 	 	 	 r o w   =   0 ;  
 	 	 	 	 c o l u m n + + ;  
 	 	 	 }  
  
 	 	 	 f o r   ( i n t   r   =   0 ;   r   <   t h i s . v a l u e s T a b l e . R o w s ;   r + + )  
 	 	 	 {  
 	 	 	 	 f o r   ( i n t   c   =   0 ;   c   <   t h i s . v a l u e s T a b l e . C o l u m n s ;   c + + )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ! t h i s . v a l u e s T a b l e [ c ,   r ] . I s E m p t y )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 v a r   f i e l d   =   t h i s . v a l u e s T a b l e [ c ,   r ] . C h i l d r e n [ 0 ]   a s   T e x t F i e l d N a v i g a t o r ;  
  
 	 	 	 	 	 	 i n t   f i e l d C o l u m n ,   f i e l d R o w ;  
 	 	 	 	 	 	 T a b l e C o n t r o l l e r . G e t T e x t F i e l d N a v i g a t o r C o l u m n R o w   ( f i e l d ,   o u t   f i e l d C o l u m n ,   o u t   f i e l d R o w ) ;  
  
 	 	 	 	 	 	 i f   ( f i e l d C o l u m n   = =   c o l u m n   & &   f i e l d R o w   = =   r o w )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 f i e l d . S e l e c t A l l   ( ) ;  
 	 	 	 	 	 	 	 f i e l d . F o c u s   ( ) ;  
 	 	 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e S w a p B u t t o n C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i n t   t   =   t h i s . c o l u m n D i m e n s i o n S e l e c t e d ;  
 	 	 	 t h i s . c o l u m n D i m e n s i o n S e l e c t e d   =   t h i s . r o w D i m e n s i o n S e l e c t e d ;  
 	 	 	 t h i s . r o w D i m e n s i o n S e l e c t e d   =   t ;  
  
 	 	 	 t h i s . R e f r e s h D i m e n s i o n s T a b l e   ( ) ;  
 	 	 	 t h i s . U p d a t e V a l u e s T a b l e   ( ) ;  
 	 	 	 t h i s . D e s e l e c t A l l   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e R a d i o C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 v a r   r a d i o   =   s e n d e r   a s   R a d i o B u t t o n ;  
 	 	 	 s t r i n g   c o l u m n   =   r a d i o . N a m e . S u b s t r i n g   ( 0 ,   1 ) ;  
 	 	 	 i n t   r o w   =   i n t . P a r s e   ( r a d i o . N a m e . S u b s t r i n g   ( 1 ) ) ;  
  
 	 	 	 i f   ( c o l u m n   = =   " C " )  
 	 	 	 {  
 	 	 	 	 t h i s . c o l u m n D i m e n s i o n S e l e c t e d   =   r o w ;  
 	 	 	 }  
  
 	 	 	 i f   ( c o l u m n   = =   " R " )  
 	 	 	 {  
 	 	 	 	 t h i s . r o w D i m e n s i o n S e l e c t e d   =   r o w ;  
 	 	 	 }  
  
 	 	 	 t h i s . R e f r e s h D i m e n s i o n s T a b l e   ( ) ;  
 	 	 	 t h i s . U p d a t e V a l u e s T a b l e   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C o m b o C l o s e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 v a r   c o m b o   =   s e n d e r   a s   T e x t F i e l d C o m b o ;  
 	 	 	 i n t   r o w   =   i n t . P a r s e   ( c o m b o . N a m e ) ;  
  
 	 	 	 t h i s . U p d a t e V a l u e s T a b l e   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e F i e l d T e x t C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 i f   ( t h i s . i g n o r e C h a n g e )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 v a r   f i e l d   =   s e n d e r   a s   T e x t F i e l d N a v i g a t o r ;  
  
 	 	 	 i n t   c o l u m n ,   r o w ;  
 	 	 	 T a b l e C o n t r o l l e r . G e t T e x t F i e l d N a v i g a t o r C o l u m n R o w   ( f i e l d ,   o u t   c o l u m n ,   o u t   r o w ) ;  
  
 	 	 	 t h i s . S e t V a l u e   ( t h i s . G e t K e y   ( c o l u m n ,   r o w ) ,   f i e l d . T e x t ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e F i e l d N a v i g a t e ( T e x t F i e l d N a v i g a t o r   s e n d e r ,   i n t   h D i r ,   i n t   v D i r )  
 	 	 {  
 	 	 	 i n t   c o l u m n ,   r o w ;  
 	 	 	 T a b l e C o n t r o l l e r . G e t T e x t F i e l d N a v i g a t o r C o l u m n R o w   ( s e n d e r ,   o u t   c o l u m n ,   o u t   r o w ) ;  
  
 	 	 	 t h i s . S e t F o c u s I n V a l u e s T a b l e   ( c o l u m n + h D i r ,   r o w + v D i r ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e F i e l d I s F o c u s e d C h a n g e d ( o b j e c t   s e n d e r ,   D e p e n d e n c y P r o p e r t y C h a n g e d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 b o o l   f o c u s e d   =   ( b o o l )   e . N e w V a l u e ;  
  
 	 	 	 i f   ( f o c u s e d )  
 	 	 	 {  
 	 	 	 	 v a r   f i e l d   =   s e n d e r   a s   T e x t F i e l d N a v i g a t o r ;  
  
 	 	 	 	 i n t   c o l u m n ,   r o w ;  
 	 	 	 	 T a b l e C o n t r o l l e r . G e t T e x t F i e l d N a v i g a t o r C o l u m n R o w   ( f i e l d ,   o u t   c o l u m n ,   o u t   r o w ) ;  
  
 	 	 	 	 t h i s . c o l u m n F o c u s e d   =   c o l u m n ;  
 	 	 	 	 t h i s . r o w F o c u s e d   =   r o w ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   v o i d   G e t T e x t F i e l d N a v i g a t o r C o l u m n R o w ( T e x t F i e l d N a v i g a t o r   f i e l d ,   o u t   i n t   c o l u m n ,   o u t   i n t   r o w )  
 	 	 {  
 	 	 	 v a r   p a r t s   =   f i e l d . N a m e . S p l i t   ( ' . ' ) ;  
 	 	 	 c o l u m n   =   i n t . P a r s e   ( p a r t s [ 0 ] ) ;  
 	 	 	 r o w         =   i n t . P a r s e   ( p a r t s [ 1 ] ) ;  
 	 	 }  
  
  
 	 	 # r e g i o n   I m p o r t / e x p o r t  
 	 	 p r i v a t e   v o i d   C l e a r ( )  
 	 	 {  
 	 	 	 / / 	 E f f a c e   t o u s   l e s   p r i x   d e   l a   t a b e l l e .  
 	 	 	 b o o l   h a s S e l e c t i o n   =   t h i s . H a s S e l e c t i o n ;  
  
 	 	 	 f o r   ( i n t   r   =   0 ;   r   <   t h i s . v a l u e s T a b l e . R o w s ;   r + + )  
 	 	 	 {  
 	 	 	 	 f o r   ( i n t   c   =   0 ;   c   <   t h i s . v a l u e s T a b l e . C o l u m n s ;   c + + )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ! h a s S e l e c t i o n   | |   t h i s . v a l u e s T a b l e . I s C e l l S e l e c t e d   ( r ,   c ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . S e t V a l u e   ( t h i s . G e t K e y   ( c ,   r ) ,   ( d e c i m a l ? )   n u l l ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e V a l u e s T a b l e   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C l i p b o a r d C u t ( )  
 	 	 {  
 	 	 	 t h i s . C l i p b o a r d C o p y   ( ) ;  
 	 	 	 t h i s . C l e a r   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C l i p b o a r d C o p y ( )  
 	 	 {  
 	 	 	 v a r   l i n e s   =   t h i s . G e t C o n t e n t T o E x p o r t   ( t r u e ,   t r u e ) ;  
 	 	 	 s t r i n g   c o n t e n t   =   s t r i n g . J o i n   ( " \ r \ n " ,   l i n e s ) ;  
  
 	 	 	 v a r   d a t a   =   n e w   C l i p b o a r d W r i t e D a t a   ( ) ;  
 	 	 	 d a t a . W r i t e T e x t   ( c o n t e n t ) ;  
 	 	 	 C l i p b o a r d . S e t D a t a   ( d a t a ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C l i p b o a r d P a s t e ( )  
 	 	 {  
 	 	 	 C l i p b o a r d R e a d D a t a   d a t a   =   C l i p b o a r d . G e t D a t a   ( ) ;  
 	 	 	 s t r i n g   c o n t e n t   =   d a t a . R e a d T e x t   ( ) ;  
  
 	 	 	 i f   ( s t r i n g . I s N u l l O r W h i t e S p a c e   ( c o n t e n t ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 c o n t e n t   =   c o n t e n t . R e p l a c e   ( " \ r \ n " ,   " \ n " ) ;  
 	 	 	 s t r i n g [ ]   l i n e s   =   c o n t e n t . S p l i t   ( ' \ n ' ) ;  
  
 	 	 	 t h i s . S e t I m p o r t e d C o n t e n t   ( l i n e s ,   t r u e ,   t r u e ) ;  
 	 	 	 t h i s . U p d a t e V a l u e s T a b l e   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   D e s e l e c t A l l ( )  
 	 	 {  
 	 	 	 f o r   ( i n t   r   =   0 ;   r   <   t h i s . v a l u e s T a b l e . R o w s ;   r + + )  
 	 	 	 {  
 	 	 	 	 f o r   ( i n t   c   =   0 ;   c   <   t h i s . v a l u e s T a b l e . C o l u m n s ;   c + + )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . v a l u e s T a b l e . S e l e c t C e l l   ( c ,   r ,   f a l s e ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S e l e c t A l l ( )  
 	 	 {  
 	 	 	 f o r   ( i n t   r   =   0 ;   r   <   t h i s . v a l u e s T a b l e . R o w s ;   r + + )  
 	 	 	 {  
 	 	 	 	 f o r   ( i n t   c   =   0 ;   c   <   t h i s . v a l u e s T a b l e . C o l u m n s ;   c + + )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . v a l u e s T a b l e . S e l e c t C e l l   ( c ,   r ,   t r u e ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S e l e c t C o l u m n ( )  
 	 	 {  
 	 	 	 f o r   ( i n t   r   =   0 ;   r   <   t h i s . v a l u e s T a b l e . R o w s ;   r + + )  
 	 	 	 {  
 	 	 	 	 t h i s . v a l u e s T a b l e . S e l e c t C e l l   ( t h i s . c o l u m n F o c u s e d ,   r ,   t r u e ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S e l e c t R o w ( )  
 	 	 {  
 	 	 	 f o r   ( i n t   c   =   0 ;   c   <   t h i s . v a l u e s T a b l e . C o l u m n s ;   c + + )  
 	 	 	 {  
 	 	 	 	 t h i s . v a l u e s T a b l e . S e l e c t C e l l   ( c ,   t h i s . r o w F o c u s e d ,   t r u e ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S e l e c t I n v e r t ( )  
 	 	 {  
 	 	 	 f o r   ( i n t   r   =   0 ;   r   <   t h i s . v a l u e s T a b l e . R o w s ;   r + + )  
 	 	 	 {  
 	 	 	 	 f o r   ( i n t   c   =   0 ;   c   <   t h i s . v a l u e s T a b l e . C o l u m n s ;   c + + )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . v a l u e s T a b l e . S e l e c t C e l l   ( c ,   r ,   ! t h i s . v a l u e s T a b l e . I s C e l l S e l e c t e d   ( r ,   c ) ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   H a s S e l e c t i o n  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 f o r   ( i n t   r   =   0 ;   r   <   t h i s . v a l u e s T a b l e . R o w s ;   r + + )  
 	 	 	 	 {  
 	 	 	 	 	 f o r   ( i n t   c   =   0 ;   c   <   t h i s . v a l u e s T a b l e . C o l u m n s ;   c + + )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 i f   ( t h i s . v a l u e s T a b l e . I s C e l l S e l e c t e d   ( r ,   c ) )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   A s s i g n ( d e c i m a l   v a l u e )  
 	 	 {  
 	 	 	 b o o l   h a s S e l e c t i o n   =   t h i s . H a s S e l e c t i o n ;  
  
 	 	 	 f o r   ( i n t   r   =   0 ;   r   <   t h i s . v a l u e s T a b l e . R o w s ;   r + + )  
 	 	 	 {  
 	 	 	 	 f o r   ( i n t   c   =   0 ;   c   <   t h i s . v a l u e s T a b l e . C o l u m n s ;   c + + )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ! h a s S e l e c t i o n   | |   t h i s . v a l u e s T a b l e . I s C e l l S e l e c t e d   ( r ,   c ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . S e t V a l u e   ( t h i s . G e t K e y   ( c ,   r ) ,   v a l u e ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e V a l u e s T a b l e   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C o m p u t e ( d e c i m a l   a d d ,   d e c i m a l   m u l )  
 	 	 {  
 	 	 	 b o o l   h a s S e l e c t i o n   =   t h i s . H a s S e l e c t i o n ;  
  
 	 	 	 f o r   ( i n t   r   =   0 ;   r   <   t h i s . v a l u e s T a b l e . R o w s ;   r + + )  
 	 	 	 {  
 	 	 	 	 f o r   ( i n t   c   =   0 ;   c   <   t h i s . v a l u e s T a b l e . C o l u m n s ;   c + + )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ! h a s S e l e c t i o n   | |   t h i s . v a l u e s T a b l e . I s C e l l S e l e c t e d   ( r ,   c ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 v a r   k e y   =   t h i s . G e t K e y   ( c ,   r ) ;  
  
 	 	 	 	 	 	 d e c i m a l ?   v a l u e   =   t h i s . t a b l e . V a l u e s . G e t V a l u e   ( k e y ) ;  
  
 	 	 	 	 	 	 i f   ( v a l u e . H a s V a l u e )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 v a l u e   + =   a d d ;  
 	 	 	 	 	 	 	 v a l u e   * =   m u l ;  
 	 	 	 	 	 	 	 t h i s . S e t V a l u e   ( k e y ,   v a l u e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e V a l u e s T a b l e   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   E x p o r t ( )  
 	 	 {  
 	 	 	 s t r i n g   p a t h ;  
 	 	 	 b o o l   u s e C o l u m n s ,   u s e R o w s ;  
 	 	 	 i f   ( t h i s . E x p o r t D i a l o g   ( o u t   p a t h ,   o u t   u s e C o l u m n s ,   o u t   u s e R o w s ) )  
 	 	 	 {  
 	 	 	 	 s t r i n g   e r r   =   t h i s . E x p o r t F i l e   ( p a t h ,   u s e C o l u m n s ,   u s e R o w s ) ;  
  
 	 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( e r r ) )  
 	 	 	 	 {  
 	 	 	 	 	 M e s s a g e D i a l o g . S h o w E r r o r   ( e r r ,   t h i s . d i m e n s i o n s T a b l e . W i n d o w ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   I m p o r t ( )  
 	 	 {  
 	 	 	 s t r i n g   p a t h ;  
 	 	 	 b o o l   u s e C o l u m n s ,   u s e R o w s ;  
 	 	 	 i f   ( t h i s . I m p o r t D i a l o g   ( o u t   p a t h ,   o u t   u s e C o l u m n s ,   o u t   u s e R o w s ) )  
 	 	 	 {  
 	 	 	 	 s t r i n g   e r r   =   t h i s . I m p o r t F i l e   ( p a t h ,   u s e C o l u m n s ,   u s e R o w s ) ;  
 	 	 	 	 t h i s . U p d a t e V a l u e s T a b l e   ( ) ;  
  
 	 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( e r r ) )  
 	 	 	 	 {  
 	 	 	 	 	 M e s s a g e D i a l o g . S h o w E r r o r   ( e r r ,   t h i s . d i m e n s i o n s T a b l e . W i n d o w ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   E x p o r t D i a l o g ( o u t   s t r i n g   p a t h ,   o u t   b o o l   u s e C o l u m n s ,   o u t   b o o l   u s e R o w s )  
 	 	 {  
 	 	 	 s t r i n g   t i t l e   =   s t r i n g . F o r m a t   ( " E x p o r t a t i o n   d ' u n e   t a b e l l e   d e   p r i x   ( a x e s   { 0 }   e t   { 1 } ) " ,   t h i s . t a b l e . D i m e n s i o n s [ t h i s . c o l u m n D i m e n s i o n S e l e c t e d ] . N a m e ,   t h i s . t a b l e . D i m e n s i o n s [ t h i s . r o w D i m e n s i o n S e l e c t e d ] . N a m e ) ;  
 	 	 	 v a r   d i a l o g   =   n e w   D i a l o g s . T a b l e D e s i g n e r F i l e E x p o r t D i a l o g   ( t h i s . d i m e n s i o n s T a b l e ,   t i t l e ) ;  
  
 	 	 	 d i a l o g . S h o w D i a l o g   ( ) ;  
  
 	 	 	 i f   ( d i a l o g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 p a t h   =   n u l l ;  
 	 	 	 	 u s e C o l u m n s   =   f a l s e ;  
 	 	 	 	 u s e R o w s   =   f a l s e ;  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 d i a l o g . P a t h M e m o r i z e   ( ) ;  
  
 	 	 	 p a t h   =   d i a l o g . F i l e N a m e ;  
 	 	 	 u s e C o l u m n s   =   d i a l o g . U s e C o l u m n s ;  
 	 	 	 u s e R o w s   =   d i a l o g . U s e R o w s ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   I m p o r t D i a l o g ( o u t   s t r i n g   p a t h ,   o u t   b o o l   u s e C o l u m n s ,   o u t   b o o l   u s e R o w s )  
 	 	 {  
 	 	 	 s t r i n g   t i t l e   =   s t r i n g . F o r m a t   ( " I m p o r t a t i o n   d ' u n e   t a b e l l e   d e   p r i x   ( a x e s   { 0 }   e t   { 1 } ) " ,   t h i s . t a b l e . D i m e n s i o n s [ t h i s . c o l u m n D i m e n s i o n S e l e c t e d ] . N a m e ,   t h i s . t a b l e . D i m e n s i o n s [ t h i s . r o w D i m e n s i o n S e l e c t e d ] . N a m e ) ;  
 	 	 	 v a r   d i a l o g   =   n e w   D i a l o g s . T a b l e D e s i g n e r F i l e I m p o r t D i a l o g   ( t h i s . d i m e n s i o n s T a b l e ,   t i t l e ) ;  
  
 	 	 	 d i a l o g . S h o w D i a l o g   ( ) ;  
  
 	 	 	 i f   ( d i a l o g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 p a t h   =   n u l l ;  
 	 	 	 	 u s e C o l u m n s   =   f a l s e ;  
 	 	 	 	 u s e R o w s   =   f a l s e ;  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 d i a l o g . P a t h M e m o r i z e   ( ) ;  
  
 	 	 	 p a t h   =   d i a l o g . F i l e N a m e ;  
 	 	 	 u s e C o l u m n s   =   d i a l o g . U s e C o l u m n s ;  
 	 	 	 u s e R o w s   =   d i a l o g . U s e R o w s ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   E x p o r t F i l e ( s t r i n g   p a t h ,   b o o l   u s e C o l u m n s ,   b o o l   u s e R o w s )  
 	 	 {  
 	 	 	 / / 	 E x p o r t e   t o u t e   l a   t a b e l l e   d a n s   u n   f i c h i e r   . t x t   t a b u l é .  
 	 	 	 / / 	 R e t o u r n e   n u l l   s i   l ' o p é r a t i o n   e s t   o k   o u   u n   m e s s a g e   d ' e r r e u r .  
 	 	 	 v a r   l i n e s   =   t h i s . G e t C o n t e n t T o E x p o r t   ( u s e C o l u m n s ,   u s e R o w s ) ;  
  
 	 	 	 / / 	 E c r i t   l e   f i c h i e r .  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 S y s t e m . I O . F i l e . W r i t e A l l L i n e s   ( p a t h ,   l i n e s ) ;  
 	 	 	 }  
 	 	 	 c a t c h   ( S y s t e m . E x c e p t i o n   e x )  
 	 	 	 {  
 	 	 	 	 r e t u r n   e x . M e s s a g e ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   n u l l ;     / /   o k  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g [ ]   G e t C o n t e n t T o E x p o r t ( b o o l   u s e C o l u m n s ,   b o o l   u s e R o w s )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   t o u t   l e   c o n t e n u   à   e x p o r t e r .  
 	 	 	 v a r   l i n e s   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 / / 	 P r e m i è r e   l i g n e s   a v e c   l e s   n o m s   d e s   c o l o n n e s .  
 	 	 	 i f   ( u s e C o l u m n s )  
 	 	 	 {  
 	 	 	 	 v a r   w o r d s   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 	 i f   ( u s e R o w s )  
 	 	 	 	 {  
 	 	 	 	 	 w o r d s . A d d   ( " " ) ;  
 	 	 	 	 }  
  
 	 	 	 	 f o r e a c h   ( v a r   s   i n   t h i s . t a b l e . D i m e n s i o n s [ t h i s . c o l u m n D i m e n s i o n S e l e c t e d ] . P o i n t s )  
 	 	 	 	 {  
 	 	 	 	 	 w o r d s . A d d   ( s ) ;  
 	 	 	 	 }  
  
 	 	 	 	 l i n e s . A d d   ( s t r i n g . J o i n   ( " \ t " ,   w o r d s ) ) ;  
 	 	 	 }  
  
 	 	 	 / / 	 M e t   t o u t e s   l e s   v a l e u r s .  
 	 	 	 f o r   ( i n t   r   =   0 ;   r   <   t h i s . t a b l e . D i m e n s i o n s [ t h i s . r o w D i m e n s i o n S e l e c t e d ] . P o i n t s . C o u n t ;   r + + )  
 	 	 	 {  
 	 	 	 	 v a r   w o r d s   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 	 i f   ( u s e R o w s )  
 	 	 	 	 {  
 	 	 	 	 	 w o r d s . A d d   ( t h i s . t a b l e . D i m e n s i o n s [ t h i s . r o w D i m e n s i o n S e l e c t e d ] . P o i n t s [ r ] ) ;  
 	 	 	 	 }  
  
 	 	 	 	 f o r   ( i n t   c   =   0 ;   c   <   t h i s . t a b l e . D i m e n s i o n s [ t h i s . c o l u m n D i m e n s i o n S e l e c t e d ] . P o i n t s . C o u n t ;   c + + )  
 	 	 	 	 {  
 	 	 	 	 	 d e c i m a l ?   v a l u e   =   t h i s . t a b l e . V a l u e s . G e t V a l u e   ( t h i s . G e t K e y   ( c ,   r ) ) ;  
  
 	 	 	 	 	 i f   ( v a l u e . H a s V a l u e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 w o r d s . A d d   ( v a l u e . V a l u e . T o S t r i n g   ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 w o r d s . A d d   ( " " ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 l i n e s . A d d   ( s t r i n g . J o i n   ( " \ t " ,   w o r d s ) ) ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   l i n e s . T o A r r a y   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   I m p o r t F i l e ( s t r i n g   p a t h ,   b o o l   u s e C o l u m n s ,   b o o l   u s e R o w s )  
 	 	 {  
 	 	 	 / / 	 I m p o r t e   u n   f i c h i e r   . t x t   t a b u l é   d a n s   l a   t a b e l l e   d e   p r i x .  
 	 	 	 / / 	 R e t o u r n e   n u l l   s i   l ' o p é r a t i o n   e s t   o k   o u   u n   m e s s a g e   d ' e r r e u r .  
 	 	 	 s t r i n g [ ]   f i l e L i n e s ;  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 f i l e L i n e s   =   S y s t e m . I O . F i l e . R e a d A l l L i n e s   ( p a t h ) ;     / /   l i t   l e   f i c h i e r  
 	 	 	 }  
 	 	 	 c a t c h   ( S y s t e m . E x c e p t i o n   e x )  
 	 	 	 {  
 	 	 	 	 r e t u r n   e x . M e s s a g e ;  
 	 	 	 }  
  
 	 	 	 / / 	 N e   g a r d e   q u e   l e s   l i g n e s   n o n   v i d e s .  
 	 	 	 s t r i n g [ ]   l i n e s   =   f i l e L i n e s . W h e r e   ( x   = >   ! s t r i n g . I s N u l l O r W h i t e S p a c e   ( x ) ) . T o A r r a y   ( ) ;  
  
 	 	 	 i f   ( l i n e s . L e n g t h   < =   ( u s e R o w s   ?   1   :   0 ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   " L e   f i c h i e r   e s t   v i d e . " ;  
 	 	 	 }  
  
 	 	 	 t h i s . S e t I m p o r t e d C o n t e n t   ( l i n e s ,   u s e C o l u m n s ,   u s e R o w s ) ;  
 	 	 	 r e t u r n   n u l l ;     / /   o k  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S e t I m p o r t e d C o n t e n t ( s t r i n g [ ]   l i n e s ,   b o o l   u s e C o l u m n s ,   b o o l   u s e R o w s )  
 	 	 {  
 	 	 	 / / 	 I n s è r e   l e s   l i g n e s   i m p o r t é e s   d a n s   l a   t a b e l l e   d e   p r i x .  
 	 	 	 / / 	 C h e r c h e   l e s   n o m s   d e s   c o l o n n e s .  
 	 	 	 v a r   c o l u m n s   =   t h i s . t a b l e . D i m e n s i o n s [ t h i s . c o l u m n D i m e n s i o n S e l e c t e d ] . P o i n t s ;  
  
 	 	 	 i f   ( u s e C o l u m n s )  
 	 	 	 {  
 	 	 	 	 c o l u m n s   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 	 s t r i n g [ ]   w o r d s   =   l i n e s [ 0 ] . S p l i t   ( ' \ t ' ) ;  
  
 	 	 	 	 f o r   ( i n t   i   =   u s e R o w s   ?   1   :   0 ;   i   <   w o r d s . L e n g t h ;   i + + )  
 	 	 	 	 {  
 	 	 	 	 	 c o l u m n s . A d d   ( w o r d s [ i ] . T r i m   ( ) ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 / / 	 C h e r c h e   l e s   n o m s   d e s   l i g n e s .  
 	 	 	 v a r   r o w s   =   t h i s . t a b l e . D i m e n s i o n s [ t h i s . r o w D i m e n s i o n S e l e c t e d ] . P o i n t s ;  
  
 	 	 	 i f   ( u s e R o w s )  
 	 	 	 {  
 	 	 	 	 r o w s   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 	 f o r   ( i n t   i   =   u s e C o l u m n s   ?   1   :   0 ;   i   <   l i n e s . L e n g t h ;   i + + )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g [ ]   w o r d s   =   l i n e s [ i ] . S p l i t   ( ' \ t ' ) ;  
  
 	 	 	 	 	 r o w s . A d d   ( w o r d s [ 0 ] . T r i m   ( ) ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 / / 	 C o n s t r u i t   l a   t a b l e   d e s   d i m e n s i o n s   i m p o r t é e s .  
 	 	 	 v a r   d i m e n s i o n s   =   n e w   L i s t < D e s i g n e r D i m e n s i o n >   ( ) ;  
  
 	 	 	 f o r   ( i n t   n   =   0 ;   n   <   t h i s . t a b l e . D i m e n s i o n s . C o u n t ;   n + + )  
 	 	 	 {  
 	 	 	 	 d i m e n s i o n s . A d d   ( n e w   D e s i g n e r D i m e n s i o n   ( t h i s . t a b l e . D i m e n s i o n s [ n ] ) ) ;  
 	 	 	 }  
  
 	 	 	 d i m e n s i o n s [ t h i s . c o l u m n D i m e n s i o n S e l e c t e d ] . P o i n t s . C l e a r   ( ) ;  
 	 	 	 d i m e n s i o n s [ t h i s . c o l u m n D i m e n s i o n S e l e c t e d ] . P o i n t s . A d d R a n g e   ( c o l u m n s ) ;  
  
 	 	 	 d i m e n s i o n s [ t h i s . r o w D i m e n s i o n S e l e c t e d ] . P o i n t s . C l e a r   ( ) ;  
 	 	 	 d i m e n s i o n s [ t h i s . r o w D i m e n s i o n S e l e c t e d ] . P o i n t s . A d d R a n g e   ( r o w s ) ;  
  
 	 	 	 / / 	 E f f a c e   t o u t e s   l e s   v a l e u r s   d e s   2   d i m e n s i o n s   i m p o r t é e s .  
 	 	 	 t h i s . C l e a r   ( ) ;  
  
 	 	 	 / / 	 I m p o r t e   l e s   p r i x .  
 	 	 	 f o r   ( i n t   r   =   u s e C o l u m n s   ?   1   :   0 ;   r   <   l i n e s . L e n g t h ;   r + + )  
 	 	 	 {  
 	 	 	 	 i n t   r o w   =   u s e C o l u m n s   ?   r - 1   :   r ;  
 	 	 	 	 s t r i n g [ ]   w o r d s   =   l i n e s [ r ] . S p l i t   ( ' \ t ' ) ;  
  
 	 	 	 	 f o r   ( i n t   c   =   u s e R o w s   ?   1   :   0 ;   c   <   w o r d s . L e n g t h ;   c + + )  
 	 	 	 	 {  
 	 	 	 	 	 i n t   c o l u m n   =   u s e R o w s   ?   c - 1   :   c ;  
  
 	 	 	 	 	 d e c i m a l   v a l u e ;  
 	 	 	 	 	 i f   ( d e c i m a l . T r y P a r s e   ( w o r d s [ c ] . T r i m   ( ) ,   o u t   v a l u e ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 v a r   s t r i n g K e y   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 	 	 	 f o r   ( i n t   n   =   0 ;   n   <   t h i s . t a b l e . D i m e n s i o n s . C o u n t ;   n + + )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 i n t   c h o i c e   =   t h i s . G e t D i m e n s i o n C h o i c e   ( n ) ;  
 	 	 	 	 	 	 	 s t r i n g   k ;  
  
 	 	 	 	 	 	 	 i f   ( c h o i c e   = =   T a b l e C o n t r o l l e r . u s e d F o r C o l u m n )  
 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 i f   ( c o l u m n   > =   c o l u m n s . C o u n t )  
 	 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 	 c o n t i n u e ;  
 	 	 	 	 	 	 	 	 }  
  
 	 	 	 	 	 	 	 	 k   =   c o l u m n s [ c o l u m n ] ;  
 	 	 	 	 	 	 	 }  
 	 	 	 	 	 	 	 e l s e   i f   ( c h o i c e   = =   T a b l e C o n t r o l l e r . u s e d F o r R o w )  
 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 i f   ( r o w   > =   r o w s . C o u n t )  
 	 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 	 c o n t i n u e ;  
 	 	 	 	 	 	 	 	 }  
  
 	 	 	 	 	 	 	 	 k   =   r o w s [ r o w ] ;  
 	 	 	 	 	 	 	 }  
 	 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 k   =   t h i s . t a b l e . D i m e n s i o n s [ n ] . P o i n t s [ c h o i c e ] ;  
 	 	 	 	 	 	 	 }  
  
 	 	 	 	 	 	 	 s t r i n g K e y . A d d   ( k ) ;  
 	 	 	 	 	 	 }  
  
 	 	 	 	 	 	 i n t [ ]   i n t K e y ;  
 	 	 	 	 	 	 i f   ( t h i s . t a b l e . S t r i n g K e y T o I n t K e y   ( d i m e n s i o n s ,   s t r i n g . J o i n   ( " . " ,   s t r i n g K e y ) ,   o u t   i n t K e y ,   r e f   v a l u e ) )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . S e t V a l u e   ( i n t K e y ,   v a l u e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
 	 	 # e n d r e g i o n  
  
  
 	 	 p r i v a t e   i n t [ ]   G e t K e y ( i n t   c o l u m n ,   i n t   r o w )  
 	 	 {  
 	 	 	 v a r   l i s t   =   n e w   L i s t < i n t >   ( ) ;  
  
 	 	 	 f o r   ( i n t   n   =   0 ;   n   <   t h i s . t a b l e . D i m e n s i o n s . C o u n t ;   n + + )  
 	 	 	 {  
 	 	 	 	 i n t   c h o i c e   =   t h i s . G e t D i m e n s i o n C h o i c e   ( n ) ;  
  
 	 	 	 	 i f   ( c h o i c e   = =   T a b l e C o n t r o l l e r . u s e d F o r C o l u m n )  
 	 	 	 	 {  
 	 	 	 	 	 l i s t . A d d   ( c o l u m n ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e   i f   ( c h o i c e   = =   T a b l e C o n t r o l l e r . u s e d F o r R o w )  
 	 	 	 	 {  
 	 	 	 	 	 l i s t . A d d   ( r o w ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 l i s t . A d d   ( c h o i c e ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   l i s t . T o A r r a y   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   i n t   G e t D i m e n s i o n C h o i c e ( i n t   i n d e x )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   c h o i x   e f f e c t u é   d a n s   l ' i n t e r f a c e ,   p o u r   u n e   d i m e n s i o n   d o n n é e .  
 	 	 	 v a r   r a d i o X   =   t h i s . d i m e n s i o n s T a b l e [ 0 ,   i n d e x ] . C h i l d r e n [ 0 ]   a s   R a d i o B u t t o n ;  
 	 	 	 v a r   r a d i o Y   =   t h i s . d i m e n s i o n s T a b l e [ 1 ,   i n d e x ] . C h i l d r e n [ 0 ]   a s   R a d i o B u t t o n ;  
 	 	 	 v a r   c o m b o     =   t h i s . d i m e n s i o n s T a b l e [ 3 ,   i n d e x ] . C h i l d r e n [ 0 ]   a s   T e x t F i e l d C o m b o ;  
  
 	 	 	 i f   ( r a d i o X . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )  
 	 	 	 {  
 	 	 	 	 r e t u r n   T a b l e C o n t r o l l e r . u s e d F o r C o l u m n ;  
 	 	 	 }  
  
 	 	 	 i f   ( r a d i o Y . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )  
 	 	 	 {  
 	 	 	 	 r e t u r n   T a b l e C o n t r o l l e r . u s e d F o r R o w ;  
 	 	 	 }  
  
 	 	 	 i n t   i   =   t h i s . t a b l e . D i m e n s i o n s [ i n d e x ] . P o i n t s . I n d e x O f   ( c o m b o . T e x t ) ;  
  
 	 	 	 i f   ( i   = =   - 1 )  
 	 	 	 {  
 	 	 	 	 i   =   0 ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   i ;  
 	 	 }  
  
  
 	 	 p r i v a t e   s t r i n g   G e t V a l u e ( i n t [ ]   i n d e x e s )  
 	 	 {  
 	 	 	 d e c i m a l ?   d   =   t h i s . t a b l e . V a l u e s . G e t V a l u e   ( i n d e x e s ) ;  
  
 	 	 	 r e t u r n   T a b l e C o n t r o l l e r . P r i c e T o S t r i n g   ( d ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S e t V a l u e ( i n t [ ]   i n d e x e s ,   s t r i n g   v a l u e )  
 	 	 {  
 	 	 	 d e c i m a l   d ;  
 	 	 	 i f   ( d e c i m a l . T r y P a r s e   ( v a l u e ,   o u t   d ) )  
 	 	 	 {  
 	 	 	 	 t h i s . S e t V a l u e   ( i n d e x e s ,   d ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . S e t V a l u e   ( i n d e x e s ,   ( d e c i m a l ? )   n u l l ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S e t V a l u e ( i n t [ ]   i n d e x e s ,   d e c i m a l ?   v a l u e )  
 	 	 {  
 	 	 	 t h i s . t a b l e . V a l u e s . S e t V a l u e   ( i n d e x e s ,   v a l u e ) ;  
 	 	 	 t h i s . S e t D i r t y   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   s t a t i c   F o r m a t t e d T e x t   G e t L e g e n d T e x t ( F o r m a t t e d T e x t   t e x t )  
 	 	 {  
 	 	 	 r e t u r n   t e x t . A p p l y B o l d   ( ) . A p p l y F o n t S i z e   ( 1 6 ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   P r i c e T o S t r i n g ( d e c i m a l ?   v a l u e )  
 	 	 {  
 	 	 	 i f   ( ! v a l u e . H a s V a l u e )  
 	 	 	 {  
 	 	 	 	 r e t u r n   n u l l ;  
 	 	 	 }  
  
 	 	 	 D e c i m a l R a n g e   d e c i m a l R a n g e 0 0 1   =   n e w   D e c i m a l R a n g e   ( - 1 0 0 0 0 0 0 0 0 0 M ,   1 0 0 0 0 0 0 0 0 0 M ,   0 . 0 1 M ) ;  
 	 	 	 r e t u r n   d e c i m a l R a n g e 0 0 1 . C o n v e r t T o S t r i n g   ( v a l u e . V a l u e ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   S e t D i r t y ( )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t . N o t i f y E x t e r n a l C h a n g e s   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   s t a t i c   r e a d o n l y   d o u b l e 	 	 	 	 	 	 l e g e n d H e i g h t   =   2 8 ;  
 	 	 p r i v a t e   s t a t i c   r e a d o n l y   i n t 	 	 	 	 	 	 	 u s e d F o r C o l u m n   =   - 2 ;  
 	 	 p r i v a t e   s t a t i c   r e a d o n l y   i n t 	 	 	 	 	 	 	 u s e d F o r R o w   =   - 3 ;  
  
 	 	 p r i v a t e   r e a d o n l y   C o r e . B u s i n e s s . B u s i n e s s C o n t e x t 	 	 b u s i n e s s C o n t e x t ;  
 	 	 p r i v a t e   r e a d o n l y   D e s i g n e r T a b l e 	 	 	 	 	 	 t a b l e ;  
  
 	 	 p r i v a t e   G l y p h B u t t o n 	 	 	 	 	 	 	 	 	 d i m e n s i o n s B u t t o n ;  
 	 	 p r i v a t e   F r a m e B o x 	 	 	 	 	 	 	 	 	 d i m e n s i o n s P a n e ;  
 	 	 p r i v a t e   C e l l T a b l e 	 	 	 	 	 	 	 	 	 d i m e n s i o n s T a b l e ;  
 	 	 p r i v a t e   G l y p h B u t t o n 	 	 	 	 	 	 	 	 	 s w a p B u t t o n ;  
 	 	 p r i v a t e   V S t a t i c T e x t 	 	 	 	 	 	 	 	 	 r o w s L e g e n d ;  
 	 	 p r i v a t e   S t a t i c T e x t 	 	 	 	 	 	 	 	 	 c o l u m n s L e g e n d ;  
 	 	 p r i v a t e   C e l l T a b l e 	 	 	 	 	 	 	 	 	 v a l u e s T a b l e ;  
 	 	 p r i v a t e   G l y p h B u t t o n 	 	 	 	 	 	 	 	 	 t o o l b a r B u t t o n ;  
 	 	 p r i v a t e   H T o o l B a r 	 	 	 	 	 	 	 	 	 t o o l b a r ;  
  
 	 	 p r i v a t e   i n t 	 	 	 	 	 	 	 	 	 	 	 c o l u m n D i m e n s i o n S e l e c t e d ;  
 	 	 p r i v a t e   i n t 	 	 	 	 	 	 	 	 	 	 	 r o w D i m e n s i o n S e l e c t e d ;  
 	 	 p r i v a t e   i n t 	 	 	 	 	 	 	 	 	 	 	 c o l u m n F o c u s e d ;  
 	 	 p r i v a t e   i n t 	 	 	 	 	 	 	 	 	 	 	 r o w F o c u s e d ;  
 	 	 p r i v a t e   b o o l 	 	 	 	 	 	 	 	 	 	 i g n o r e C h a n g e ;  
 	 }  
 }  
 