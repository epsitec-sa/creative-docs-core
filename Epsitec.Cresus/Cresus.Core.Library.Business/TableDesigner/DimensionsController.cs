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
 	 p u b l i c   c l a s s   D i m e n s i o n s C o n t r o l l e r  
 	 {  
 	 	 p u b l i c   D i m e n s i o n s C o n t r o l l e r ( C o r e . B u s i n e s s . B u s i n e s s C o n t e x t   b u s i n e s s C o n t e x t ,   A r t i c l e D e f i n i t i o n E n t i t y   a r t i c l e D e f i n i t i o n E n t i t y ,   D e s i g n e r T a b l e   t a b l e )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t                   =   b u s i n e s s C o n t e x t ;  
 	 	 	 t h i s . a r t i c l e D e f i n i t i o n E n t i t y   =   a r t i c l e D e f i n i t i o n E n t i t y ;  
 	 	 	 t h i s . t a b l e                                       =   t a b l e ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   C r e a t e U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   f r a m e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 v a r   d i m e n s i o n s P a n e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   f r a m e ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   2 0 0 + 1 + T i l e A r r o w . B r e a d t h ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 1 0 ,   1 0 ,   1 0 ,   1 0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   p o i n t s P a n e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   f r a m e ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   2 0 0 ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   1 0 ,   1 0 ,   1 0 ) ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . C r e a t e D i m e n s i o n U I   ( d i m e n s i o n s P a n e ) ;  
 	 	 	 t h i s . C r e a t e P o i n t U I   ( p o i n t s P a n e ) ;  
  
 	 	 	 t h i s . U p d a t e D i m e n s i o n s L i s t   ( ) ;  
 	 	 	 t h i s . U p d a t e R o u n d i n g P a n e   ( ) ;  
 	 	 	 t h i s . U p d a t e P o i n t s L i s t   ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   U p d a t e ( )  
 	 	 {  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   C r e a t e D i m e n s i o n U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   c o l u m n T i t l e   =   n e w   S t a t i c T e x t   ( p a r e n t ) ;  
 	 	 	 c o l u m n T i t l e . S e t C o l u m n T i t l e   ( " A x e s " ) ;  
  
 	 	 	 / / 	 C r é e   l a   l i s t e .  
 	 	 	 v a r   t i l e   =   n e w   A r r o w e d T i l e F r a m e   ( D i r e c t i o n . R i g h t )  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 P a d d i n g   =   T i l e A r r o w . G e t C o n t a i n e r P a d d i n g   ( D i r e c t i o n . R i g h t )   +   n e w   M a r g i n s   ( L i b r a r y . U I . C o n s t a n t s . T i l e I n t e r n a l P a d d i n g ) ,  
 	 	 	 } ;  
  
 	 	 	 t i l e . S e t S e l e c t e d   ( t r u e ) ;     / /   c o n t e n e u r   o r a n g e  
  
 	 	 	 t h i s . d i m e n s i o n s T a b l e   =   n e w   C e l l T a b l e  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t i l e ,  
 	 	 	 	 S t y l e H   =   C e l l A r r a y S t y l e s . S c r o l l M a g i c   |   C e l l A r r a y S t y l e s . S e p a r a t o r ,  
 	 	 	 	 S t y l e V   =   C e l l A r r a y S t y l e s . S c r o l l M a g i c   |   C e l l A r r a y S t y l e s . S e p a r a t o r   |   C e l l A r r a y S t y l e s . S e l e c t L i n e ,  
 	 	 	 	 D e f H e i g h t   =   2 4 ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 / / 	 C r é e   l e   p i e d   d e   p a g e .  
 	 	 	 t h i s . d i m e n s i o n s R o u n d i n g P a n e   =   n e w   G r o u p B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 T e x t   =   " T y p e   d ' a r r o n d i " ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . B o t t o m ,  
 	 	 	 	 M a r g i n s   =   T i l e A r r o w . G e t C o n t a i n e r P a d d i n g   ( D i r e c t i o n . R i g h t )   +   n e w   M a r g i n s   ( 0 ,   0 ,   1 0 ,   0 ) ,  
 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 1 0 ) ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g N o n e   =   n e w   R a d i o B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t h i s . d i m e n s i o n s R o u n d i n g P a n e ,  
 	 	 	 	 T e x t   =   " P a s   d ' a r r o n d i " ,  
 	 	 	 	 N a m e   =   " N o n e " ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g N e a r e s t   =   n e w   R a d i o B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t h i s . d i m e n s i o n s R o u n d i n g P a n e ,  
 	 	 	 	 T e x t   =   " V a l e u r   l a   p l u s   p r o c h e " ,  
 	 	 	 	 N a m e   =   " N e a r e s t " ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g D o w n   =   n e w   R a d i o B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t h i s . d i m e n s i o n s R o u n d i n g P a n e ,  
 	 	 	 	 T e x t   =   " V a l e u r   i n f é r i e u r e " ,  
 	 	 	 	 N a m e   =   " D o w n " ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g U p   =   n e w   R a d i o B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t h i s . d i m e n s i o n s R o u n d i n g P a n e ,  
 	 	 	 	 T e x t   =   " V a l e u r   s u p é r i e u r e " ,  
 	 	 	 	 N a m e   =   " U p " ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 } ;  
  
 	 	 	 / / 	 C o n n e x i o n   d e s   é v é n e m e n t s .  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e l e c t i o n C h a n g e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . D i m e n s i o n S e l e c t e d I t e m C h a n g e d   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g N o n e       . C l i c k e d   + =   n e w   E v e n t H a n d l e r < M e s s a g e E v e n t A r g s >   ( t h i s . H a n d l e D i m e n s i o n s R a d i o R o u n d i n g C l i c k e d ) ;  
 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g N e a r e s t . C l i c k e d   + =   n e w   E v e n t H a n d l e r < M e s s a g e E v e n t A r g s >   ( t h i s . H a n d l e D i m e n s i o n s R a d i o R o u n d i n g C l i c k e d ) ;  
 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g U p           . C l i c k e d   + =   n e w   E v e n t H a n d l e r < M e s s a g e E v e n t A r g s >   ( t h i s . H a n d l e D i m e n s i o n s R a d i o R o u n d i n g C l i c k e d ) ;  
 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g D o w n       . C l i c k e d   + =   n e w   E v e n t H a n d l e r < M e s s a g e E v e n t A r g s >   ( t h i s . H a n d l e D i m e n s i o n s R a d i o R o u n d i n g C l i c k e d ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C r e a t e P o i n t U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   c o l u m n T i t l e   =   n e w   S t a t i c T e x t   ( p a r e n t ) ;  
 	 	 	 c o l u m n T i t l e . S e t C o l u m n T i t l e   ( " P o i n t s   s u r   l ' a x e " ) ;  
  
 	 	 	 t h i s . C r e a t e P o i n t s T o o l b a r U I   ( p a r e n t ) ;  
  
 	 	 	 t h i s . p o i n t s S c r o l l L i s t   =   n e w   S c r o l l L i s t  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 / / 	 C r é e   l e   p i e d   d e   p a g e .  
 	 	 	 v a r   f o o t e r   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . B o t t o m ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   1 0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   l a b e l   =   n e w   S t a t i c T e x t  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   f o o t e r ,  
 	 	 	 	 T e x t   =   " V a l e u r   d u   p o i n t " ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   8 5 ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . p o i n t V a l u e F i e l d   =   n e w   T e x t F i e l d E x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   f o o t e r ,  
 	 	 	 	 D e f o c u s A c t i o n   =   D e f o c u s A c t i o n . A u t o A c c e p t O r R e j e c t E d i t i o n ,  
 	 	 	 	 S w a l l o w E s c a p e O n R e j e c t E d i t i o n   =   t r u e ,  
 	 	 	 	 S w a l l o w R e t u r n O n A c c e p t E d i t i o n   =   t r u e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 / / 	 C o n n e x i o n   d e s   é v é n e m e n t s .  
 	 	 	 t h i s . p o i n t s S c r o l l L i s t . S e l e c t e d I t e m C h a n g e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . P o i n t S e l e c t e d I t e m C h a n g e d   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . p o i n t V a l u e F i e l d . E d i t i o n A c c e p t e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . C h a n g e P o i n t V a l u e   ( ) ;  
 	 	 	 } ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C r e a t e P o i n t s T o o l b a r U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 / / 	 C r é e   l a   t o o l b a r .  
 	 	 	 d o u b l e   b u t t o n S i z e   =   1 9 ;  
  
 	 	 	 t h i s . p o i n t s T o o l b a r   =   U I B u i l d e r . C r e a t e M i n i T o o l b a r   ( p a r e n t ,   b u t t o n S i z e ) ;  
 	 	 	 t h i s . p o i n t s T o o l b a r . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . p o i n t s T o o l b a r . M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   - 1 ) ;  
  
 	 	 	 t h i s . a d d P o i n t s B u t t o n   =   n e w   G l y p h B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t h i s . p o i n t s T o o l b a r ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( b u t t o n S i z e * 2 + 1 ,   b u t t o n S i z e ) ,  
 	 	 	 	 G l y p h S h a p e   =   G l y p h S h a p e . P l u s ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . r e m o v e P o i n t s B u t t o n   =   n e w   G l y p h B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t h i s . p o i n t s T o o l b a r ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( b u t t o n S i z e ,   b u t t o n S i z e ) ,  
 	 	 	 	 G l y p h S h a p e   =   G l y p h S h a p e . M i n u s ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 1 ,   0 ,   0 ,   0 ) ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . u p P o i n t s B u t t o n   =   n e w   G l y p h B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t h i s . p o i n t s T o o l b a r ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( b u t t o n S i z e ,   b u t t o n S i z e ) ,  
 	 	 	 	 G l y p h S h a p e   =   G l y p h S h a p e . A r r o w U p ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 1 0 ,   0 ,   0 ,   0 ) ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 } ;  
  
 	 	 	 t h i s . d o w n P o i n t s B u t t o n   =   n e w   G l y p h B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t h i s . p o i n t s T o o l b a r ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( b u t t o n S i z e ,   b u t t o n S i z e ) ,  
 	 	 	 	 G l y p h S h a p e   =   G l y p h S h a p e . A r r o w D o w n ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 1 ,   0 ,   0 ,   0 ) ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 } ;  
  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . a d d P o i n t s B u t t o n ,         " C r é e   u n   n o u v e a u   p o i n t " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . r e m o v e P o i n t s B u t t o n ,   " S u p p r i m e   l e   p o i n t   s é l e c t i o n n é " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . u p P o i n t s B u t t o n ,           " M o n t e   l e   p o i n t   d a n s   l a   l i s t e " ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . d o w n P o i n t s B u t t o n ,       " D e s c e n d   l e   p o i n t   d a n s   l a   l i s t e " ) ;  
  
 	 	 	 / / 	 C o n n e x i o n   d e s   é v é n e m e n t s .  
 	 	 	 t h i s . a d d P o i n t s B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . P o i n t I t e m I n s e r t e d   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . r e m o v e P o i n t s B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . P o i n t I t e m R e m o v e d   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . u p P o i n t s B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . P o i n t I t e m M o v e d   ( - 1 ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . d o w n P o i n t s B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . P o i n t I t e m M o v e d   ( 1 ) ;  
 	 	 	 } ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   U p d a t e D i m e n s i o n s L i s t ( )  
 	 	 {  
 	 	 	 i n t   c o u n t   =   t h i s . A r t i c l e P a r a m e t e r D e f i n i t i o n s . C o u n t ;  
  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t A r r a y S i z e   ( 2 ,   c o u n t ) ;  
  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t W i d t h C o l u m n   ( 0 ,   2 4 ) ;  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e t W i d t h C o l u m n   ( 1 ,   2 0 0 - 2 4 - 1 0 ) ;  
  
 	 	 	 f o r   ( i n t   r o w   =   0 ;   r o w   <   c o u n t ;   r o w + + )  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . d i m e n s i o n s T a b l e [ 0 ,   r o w ] . I s E m p t y )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   c o d e   =   t h i s . A r t i c l e P a r a m e t e r D e f i n i t i o n s [ r o w ] . C o d e ;  
  
 	 	 	 	 	 v a r   b u t t o n   =   n e w   C h e c k B u t t o n  
 	 	 	 	 	 {  
 	 	 	 	 	 	 A c t i v e S t a t e   =   t h i s . t a b l e . G e t D i m e n s i o n   ( c o d e )   = =   n u l l   ?   A c t i v e S t a t e . N o   :   A c t i v e S t a t e . Y e s ,  
 	 	 	 	 	 	 A u t o T o g g l e   =   f a l s e ,  
 	 	 	 	 	 	 N a m e   =   c o d e ,  
 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 5 ,   0 ,   0 ,   0 ) ,  
 	 	 	 	 	 } ;  
  
 	 	 	 	 	 b u t t o n . C l i c k e d   + =   n e w   E v e n t H a n d l e r < M e s s a g e E v e n t A r g s >   ( t h i s . H a n d l e C h e c k B u t t o n C l i c k e d ) ;  
  
 	 	 	 	 	 t h i s . d i m e n s i o n s T a b l e [ 0 ,   r o w ] . I n s e r t   ( b u t t o n ) ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . d i m e n s i o n s T a b l e [ 1 ,   r o w ] . I s E m p t y )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   l a b e l   =   n e w   S t a t i c T e x t  
 	 	 	 	 	 {  
 	 	 	 	 	 	 C o n t e n t A l i g n m e n t   =   C o m m o n . D r a w i n g . C o n t e n t A l i g n m e n t . M i d d l e L e f t ,  
 	 	 	 	 	 	 F o r m a t t e d T e x t   =   D i m e n s i o n s C o n t r o l l e r . G e t P a r a m e t e r D e s c r i p t i o n   ( t h i s . A r t i c l e P a r a m e t e r D e f i n i t i o n s [ r o w ] ) ,  
 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 5 ,   5 ,   0 ,   0 ) ,  
 	 	 	 	 	 } ;  
  
 	 	 	 	 	 t h i s . d i m e n s i o n s T a b l e [ 1 ,   r o w ] . I n s e r t   ( l a b e l ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e R o u n d i n g P a n e ( )  
 	 	 {  
 	 	 	 v a r   d i m e n s i o n   =   t h i s . G e t D i m e n s i o n ;  
  
 	 	 	 i f   ( d i m e n s i o n   ! =   n u l l   & &   d i m e n s i o n . H a s D e c i m a l )  
 	 	 	 {  
 	 	 	 	 t h i s . d i m e n s i o n s R o u n d i n g P a n e . V i s i b i l i t y   =   t r u e ;  
  
 	 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g N o n e       . A c t i v e S t a t e   =   ( d i m e n s i o n . R o u n d i n g M o d e   = =   R o u n d i n g M o d e . N o n e       )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g N e a r e s t . A c t i v e S t a t e   =   ( d i m e n s i o n . R o u n d i n g M o d e   = =   R o u n d i n g M o d e . N e a r e s t )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g U p           . A c t i v e S t a t e   =   ( d i m e n s i o n . R o u n d i n g M o d e   = =   R o u n d i n g M o d e . U p           )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . d i m e n s i o n s R a d i o R o u n d i n g D o w n       . A c t i v e S t a t e   =   ( d i m e n s i o n . R o u n d i n g M o d e   = =   R o u n d i n g M o d e . D o w n       )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d i m e n s i o n s R o u n d i n g P a n e . V i s i b i l i t y   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e P o i n t s L i s t ( i n t ?   s e l   =   n u l l )  
 	 	 {  
 	 	 	 i f   ( s e l   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 s e l   =   t h i s . p o i n t s S c r o l l L i s t . S e l e c t e d I t e m I n d e x ;  
 	 	 	 }  
  
 	 	 	 t h i s . p o i n t s S c r o l l L i s t . I t e m s . C l e a r   ( ) ;  
  
 	 	 	 v a r   d i m e n s i o n   =   t h i s . G e t D i m e n s i o n ;  
 	 	 	 v a r   l i s t   =   t h i s . G e t P o i n t s ;  
  
 	 	 	 i f   ( l i s t   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 t h i s . p o i n t s T o o l b a r . V i s i b i l i t y   =   f a l s e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . p o i n t s T o o l b a r . V i s i b i l i t y   =   t r u e ;  
  
 	 	 	 	 i f   ( d i m e n s i o n . H a s D e c i m a l )  
 	 	 	 	 {  
 	 	 	 	 	 / / 	 U n e   l i s t e   n u m é r i q u e   e s t   i n t r i n s è q u e m e n t   o r d o n n é e .   C e l a   n ' a   d o n c   p a s   d e   s e n s  
 	 	 	 	 	 / / 	 d e   p o u v o i r   m o d i f i e r   l ' o r d r e .  
 	 	 	 	 	 t h i s . u p P o i n t s B u t t o n . V i s i b i l i t y   =   f a l s e ;  
 	 	 	 	 	 t h i s . d o w n P o i n t s B u t t o n . V i s i b i l i t y   =   f a l s e ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . u p P o i n t s B u t t o n . V i s i b i l i t y   =   t r u e ;  
 	 	 	 	 	 t h i s . d o w n P o i n t s B u t t o n . V i s i b i l i t y   =   t r u e ;  
 	 	 	 	 }  
  
 	 	 	 	 f o r e a c h   ( v a r   v a l u e   i n   l i s t )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . p o i n t s S c r o l l L i s t . I t e m s . A d d   ( v a l u e ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . p o i n t s S c r o l l L i s t . S e l e c t e d I t e m I n d e x   =   s e l . V a l u e ;  
 	 	 	 t h i s . U p d a t e A f t e r P o i n t S e l e c t e d   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e A f t e r D i m e n s i o n S e l e c t e d ( )  
 	 	 {  
 	 	 	 t h i s . U p d a t e R o u n d i n g P a n e   ( ) ;  
 	 	 	 t h i s . U p d a t e P o i n t s L i s t   ( - 1 ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e A f t e r P o i n t S e l e c t e d ( )  
 	 	 {  
 	 	 	 i n t   s e l   =   t h i s . p o i n t s S c r o l l L i s t . S e l e c t e d I t e m I n d e x ;  
 	 	 	 v a r   d i m e n s i o n   =   t h i s . G e t D i m e n s i o n ;  
  
 	 	 	 i f   ( s e l   = =   - 1   | |   d i m e n s i o n   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 t h i s . p o i n t V a l u e F i e l d . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p o i n t V a l u e F i e l d . T e x t   =   n u l l ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . p o i n t V a l u e F i e l d . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . p o i n t V a l u e F i e l d . T e x t   =   t h i s . G e t D i m e n s i o n . P o i n t s . E l e m e n t A t   ( s e l ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . r e m o v e P o i n t s B u t t o n . E n a b l e   =   s e l   ! =   - 1 ;  
 	 	 	 t h i s . u p P o i n t s B u t t o n . E n a b l e           =   s e l   >   0 ;  
 	 	 	 t h i s . d o w n P o i n t s B u t t o n . E n a b l e       =   s e l   ! =   - 1   & &   s e l   <   d i m e n s i o n . P o i n t s . C o u n t - 1 ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e D i m e n s i o n s R a d i o R o u n d i n g C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 v a r   r a d i o   =   s e n d e r   a s   R a d i o B u t t o n ;  
 	 	 	 v a r   d i m e n s i o n   =   t h i s . G e t D i m e n s i o n ;  
  
 	 	 	 s w i t c h   ( r a d i o . N a m e )  
 	 	 	 {  
 	 	 	 	 c a s e   " N o n e " :  
 	 	 	 	 	 d i m e n s i o n . R o u n d i n g M o d e   =   R o u n d i n g M o d e . N o n e ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   " N e a r e s t " :  
 	 	 	 	 	 d i m e n s i o n . R o u n d i n g M o d e   =   R o u n d i n g M o d e . N e a r e s t ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   " U p " :  
 	 	 	 	 	 d i m e n s i o n . R o u n d i n g M o d e   =   R o u n d i n g M o d e . U p ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   " D o w n " :  
 	 	 	 	 	 d i m e n s i o n . R o u n d i n g M o d e   =   R o u n d i n g M o d e . D o w n ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 }  
  
 	 	 	 t h i s . S e t D i r t y   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e C h e c k B u t t o n C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 v a r   b u t t o n   =   s e n d e r   a s   C h e c k B u t t o n ;  
  
 	 	 	 i f   ( b u t t o n . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )  
 	 	 	 {  
 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
  
 	 	 	 	 i f   ( ! t h i s . C r e a t e D i m e n s i o n   ( b u t t o n . N a m e ) )  
 	 	 	 	 {  
 	 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
  
 	 	 	 	 i f   ( ! t h i s . R e m o v e D i m e n s i o n   ( b u t t o n . N a m e ) )  
 	 	 	 	 {  
 	 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   C r e a t e D i m e n s i o n ( s t r i n g   c o d e )  
 	 	 {  
 	 	 	 i f   ( t h i s . t a b l e . V a l u e s . D a t a . C o u n t   >   0 )  
 	 	 	 {  
 	 	 	 	 s t r i n g   m e s s a g e   =   " V o u s   a l l e z   c r é e r   u n   n o u v e l   a x e   d a n s   l e s   t a b e l l e s   d e   p r i x . < b r / > "   +  
 	 	 	 	 	 	 	 	   " T o u s   l e s   p r i x   s e r o n t   e f f a c é s . < b r / > "   +  
 	 	 	 	 	 	 	 	   " E s t - c e   b i e n   c e   q u e   v o u s   d é s i r e z   ? " ;  
  
 	 	 	 	 i f   ( M e s s a g e D i a l o g . S h o w Q u e s t i o n   ( m e s s a g e ,   t h i s . d i m e n s i o n s T a b l e . W i n d o w )   ! =   D i a l o g R e s u l t . Y e s )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 v a r   p   =   t h i s . A r t i c l e P a r a m e t e r D e f i n i t i o n s . W h e r e   ( x   = >   x . C o d e   = =   c o d e ) . F i r s t O r D e f a u l t   ( ) ;  
  
 	 	 	 i f   ( p   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 v a r   d i m e n s i o n   =   n e w   D e s i g n e r D i m e n s i o n   ( p ) ;  
 	 	 	 t h i s . t a b l e . D i m e n s i o n s . A d d   ( d i m e n s i o n ) ;  
 	 	 	 t h i s . t a b l e . V a l u e s . C l e a r   ( ) ;  
 	 	 	 t h i s . S e t D i r t y   ( ) ;  
  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . D e s e l e c t A l l   ( ) ;  
 	 	 	 t h i s . d i m e n s i o n s T a b l e . S e l e c t R o w   ( t h i s . G e t A r t i c l e P a r a m e t e r I n d e x   ( c o d e ) ,   t r u e ) ;  
  
 	 	 	 t h i s . U p d a t e P o i n t s L i s t   ( - 1 ) ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   R e m o v e D i m e n s i o n ( s t r i n g   c o d e )  
 	 	 {  
 	 	 	 i f   ( t h i s . t a b l e . V a l u e s . D a t a . C o u n t   >   0 )  
 	 	 	 {  
 	 	 	 	 s t r i n g   m e s s a g e   =   " V o u s   a l l e z   s u p p r i m e r   u n   a x e   d a n s   l e s   t a b e l l e s   d e   p r i x . < b r / > "   +  
 	 	 	 	 	 	 	 	   " T o u s   l e s   p r i x   s e r o n t   e f f a c é s . < b r / > "   +  
 	 	 	 	 	 	 	 	   " E s t - c e   b i e n   c e   q u e   v o u s   d é s i r e z   ? " ;  
  
 	 	 	 	 i f   ( M e s s a g e D i a l o g . S h o w Q u e s t i o n   ( m e s s a g e ,   t h i s . d i m e n s i o n s T a b l e . W i n d o w )   ! =   D i a l o g R e s u l t . Y e s )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 v a r   d i m e n s i o n   =   t h i s . t a b l e . G e t D i m e n s i o n   ( c o d e ) ;  
  
 	 	 	 i f   ( d i m e n s i o n   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 t h i s . t a b l e . D i m e n s i o n s . R e m o v e   ( d i m e n s i o n ) ;  
 	 	 	 t h i s . t a b l e . V a l u e s . C l e a r   ( ) ;  
 	 	 	 t h i s . S e t D i r t y   ( ) ;  
  
 	 	 	 t h i s . U p d a t e P o i n t s L i s t   ( - 1 ) ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   D i m e n s i o n S e l e c t e d I t e m C h a n g e d ( )  
 	 	 {  
 	 	 	 t h i s . U p d a t e A f t e r D i m e n s i o n S e l e c t e d   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   P o i n t S e l e c t e d I t e m C h a n g e d ( )  
 	 	 {  
 	 	 	 t h i s . U p d a t e A f t e r P o i n t S e l e c t e d   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   P o i n t I t e m I n s e r t e d ( )  
 	 	 {  
 	 	 	 i n t   s e l   =   t h i s . p o i n t s S c r o l l L i s t . S e l e c t e d I t e m I n d e x ;  
 	 	 	 v a r   d i m e n s i o n   =   t h i s . G e t D i m e n s i o n ;  
  
 	 	 	 v a r   d a t a   =   t h i s . t a b l e . E x p o r t V a l u e s   ( ) ;  
  
 	 	 	 i f   ( d i m e n s i o n . H a s D e c i m a l )  
 	 	 	 {  
 	 	 	 	 d i m e n s i o n . P o i n t s . A d d   ( " 0 " ) ;  
 	 	 	 	 s e l   =   d i m e n s i o n . S o r t   ( d i m e n s i o n . P o i n t s . C o u n t - 1 ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 s e l + + ;  
 	 	 	 	 d i m e n s i o n . P o i n t s . I n s e r t   ( s e l ,   " N o u v e a u " ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . t a b l e . I m p o r t V a l u e s   ( d a t a ) ;  
 	 	 	 t h i s . S e t D i r t y   ( ) ;  
  
 	 	 	 t h i s . U p d a t e P o i n t s L i s t   ( s e l ) ;  
  
 	 	 	 t h i s . p o i n t V a l u e F i e l d . S e l e c t A l l   ( ) ;  
 	 	 	 t h i s . p o i n t V a l u e F i e l d . F o c u s   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   P o i n t I t e m R e m o v e d ( )  
 	 	 {  
 	 	 	 i n t   s e l   =   t h i s . p o i n t s S c r o l l L i s t . S e l e c t e d I t e m I n d e x ;  
 	 	 	 v a r   d i m e n s i o n   =   t h i s . G e t D i m e n s i o n ;  
  
 	 	 	 i f   ( s e l   ! =   - 1 )  
 	 	 	 {  
 	 	 	 	 v a r   d a t a   =   t h i s . t a b l e . E x p o r t V a l u e s   ( ) ;  
 	 	 	 	 d i m e n s i o n . P o i n t s . R e m o v e A t   ( s e l ) ;  
 	 	 	 	 t h i s . t a b l e . I m p o r t V a l u e s   ( d a t a ) ;  
 	 	 	 	 t h i s . S e t D i r t y   ( ) ;  
  
 	 	 	 	 i f   ( s e l   > =   d i m e n s i o n . P o i n t s . C o u n t )  
 	 	 	 	 {  
 	 	 	 	 	 s e l   =   d i m e n s i o n . P o i n t s . C o u n t - 1 ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . U p d a t e P o i n t s L i s t   ( s e l ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   P o i n t I t e m M o v e d ( i n t   d i r e c t i o n )  
 	 	 {  
 	 	 	 i n t   s e l   =   t h i s . p o i n t s S c r o l l L i s t . S e l e c t e d I t e m I n d e x ;  
 	 	 	 v a r   d i m e n s i o n   =   t h i s . G e t D i m e n s i o n ;  
  
 	 	 	 i f   ( s e l   ! =   - 1 )  
 	 	 	 {  
 	 	 	 	 v a r   d a t a   =   t h i s . t a b l e . E x p o r t V a l u e s   ( ) ;  
  
 	 	 	 	 v a r   t   =   d i m e n s i o n . P o i n t s [ s e l ] ;  
 	 	 	 	 d i m e n s i o n . P o i n t s . R e m o v e A t   ( s e l ) ;  
 	 	 	 	 d i m e n s i o n . P o i n t s . I n s e r t   ( s e l + d i r e c t i o n ,   t ) ;  
  
 	 	 	 	 t h i s . t a b l e . I m p o r t V a l u e s   ( d a t a ) ;  
 	 	 	 	 t h i s . S e t D i r t y   ( ) ;  
  
 	 	 	 	 t h i s . U p d a t e P o i n t s L i s t   ( s e l + d i r e c t i o n ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C h a n g e P o i n t V a l u e ( )  
 	 	 {  
 	 	 	 i n t   s e l   =   t h i s . p o i n t s S c r o l l L i s t . S e l e c t e d I t e m I n d e x ;  
 	 	 	 v a r   d i m e n s i o n   =   t h i s . G e t D i m e n s i o n ;  
 	 	 	 s t r i n g   v a l u e   =   t h i s . p o i n t V a l u e F i e l d . T e x t ;  
  
 	 	 	 i f   ( s e l   ! =   - 1 )  
 	 	 	 {  
 	 	 	 	 v a r   d a t a   =   t h i s . t a b l e . E x p o r t V a l u e s   ( ) ;  
  
 	 	 	 	 i f   ( d i m e n s i o n . H a s D e c i m a l )  
 	 	 	 	 {  
 	 	 	 	 	 d e c i m a l   d v ;  
 	 	 	 	 	 i f   ( d e c i m a l . T r y P a r s e   ( v a l u e ,   o u t   d v ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d i m e n s i o n . P o i n t s [ s e l ]   =   v a l u e ;  
 	 	 	 	 	 	 s e l   =   d i m e n s i o n . S o r t   ( s e l ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 d i m e n s i o n . P o i n t s [ s e l ]   =   v a l u e ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . t a b l e . I m p o r t V a l u e s   ( d a t a ) ;  
 	 	 	 	 t h i s . S e t D i r t y   ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e P o i n t s L i s t   ( s e l ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   i n t   G e t A r t i c l e P a r a m e t e r I n d e x ( s t r i n g   c o d e )  
 	 	 {  
 	 	 	 f o r   ( i n t   i = 0 ;   i < t h i s . a r t i c l e D e f i n i t i o n E n t i t y . A r t i c l e P a r a m e t e r D e f i n i t i o n s . C o u n t   ( ) ;   i + + )  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . a r t i c l e D e f i n i t i o n E n t i t y . A r t i c l e P a r a m e t e r D e f i n i t i o n s [ i ] . C o d e   = =   c o d e )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   i ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   - 1 ;  
 	 	 }  
  
 	 	 p r i v a t e   L i s t < A b s t r a c t A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y >   A r t i c l e P a r a m e t e r D e f i n i t i o n s  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l a   l i s t e   d e s   p a r a m è t r e s   q u i   p e u v e n t   s e r v i r   d ' a x e s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . a r t i c l e D e f i n i t i o n E n t i t y . A r t i c l e P a r a m e t e r D e f i n i t i o n s  
 	 	 	 	 	 . W h e r e   ( x   = >   x   i s   N u m e r i c V a l u e A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y   | |   x   i s   E n u m V a l u e A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y )  
 	 	 	 	 	 . T o L i s t   ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   L i s t < s t r i n g >   G e t P o i n t s  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 v a r   d i m e n s i o n   =   t h i s . G e t D i m e n s i o n ;  
  
 	 	 	 	 i f   ( d i m e n s i o n   = =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   n u l l ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   d i m e n s i o n . P o i n t s . T o L i s t   ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   D e s i g n e r D i m e n s i o n   G e t D i m e n s i o n  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 i n t   s e l   =   t h i s . d i m e n s i o n s T a b l e . S e l e c t e d R o w ;  
 	 	 	 	 i f   ( s e l   = =   - 1 )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   n u l l ;  
 	 	 	 	 }  
  
 	 	 	 	 v a r   b u t t o n   =   t h i s . d i m e n s i o n s T a b l e [ 0 ,   s e l ] . C h i l d r e n [ 0 ]   a s   C h e c k B u t t o n ;  
 	 	 	 	 i f   ( b u t t o n   = =   n u l l   | |   b u t t o n . A c t i v e S t a t e   ! =   A c t i v e S t a t e . Y e s )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   n u l l ;  
 	 	 	 	 }  
  
 	 	 	 	 r e t u r n   t h i s . t a b l e . G e t D i m e n s i o n   ( b u t t o n . N a m e ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   s t a t i c   F o r m a t t e d T e x t   G e t P a r a m e t e r D e s c r i p t i o n ( A b s t r a c t A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y   p a r a m e t e r )  
 	 	 {  
 	 	 	 v a r   d e s c   =   T e x t F o r m a t t e r . F o r m a t T e x t   ( " ( ~ " ,   p a r a m e t e r . D e s c r i p t i o n ,   " ~ ) " ) ;  
 	 	 	 r e t u r n   T e x t F o r m a t t e r . F o r m a t T e x t   ( p a r a m e t e r . N a m e ,   d e s c ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   S e t D i r t y ( )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t . N o t i f y E x t e r n a l C h a n g e s   ( ) ;  
 	 	 }  
  
 	  
 	 	 p r i v a t e   r e a d o n l y   C o r e . B u s i n e s s . B u s i n e s s C o n t e x t 	 	 b u s i n e s s C o n t e x t ;  
 	 	 p r i v a t e   r e a d o n l y   A r t i c l e D e f i n i t i o n E n t i t y 	 	 	 a r t i c l e D e f i n i t i o n E n t i t y ;  
 	 	 p r i v a t e   r e a d o n l y   D e s i g n e r T a b l e 	 	 	 	 	 	 t a b l e ;  
  
 	 	 p r i v a t e   C e l l T a b l e 	 	 	 	 	 	 	 	 	 d i m e n s i o n s T a b l e ;  
 	 	 p r i v a t e   G r o u p B o x 	 	 	 	 	 	 	 	 	 d i m e n s i o n s R o u n d i n g P a n e ;  
 	 	 p r i v a t e   R a d i o B u t t o n 	 	 	 	 	 	 	 	 	 d i m e n s i o n s R a d i o R o u n d i n g N o n e ;  
 	 	 p r i v a t e   R a d i o B u t t o n 	 	 	 	 	 	 	 	 	 d i m e n s i o n s R a d i o R o u n d i n g N e a r e s t ;  
 	 	 p r i v a t e   R a d i o B u t t o n 	 	 	 	 	 	 	 	 	 d i m e n s i o n s R a d i o R o u n d i n g U p ;  
 	 	 p r i v a t e   R a d i o B u t t o n 	 	 	 	 	 	 	 	 	 d i m e n s i o n s R a d i o R o u n d i n g D o w n ;  
  
 	 	 p r i v a t e   F r a m e B o x 	 	 	 	 	 	 	 	 	 p o i n t s T o o l b a r ;  
 	 	 p r i v a t e   G l y p h B u t t o n 	 	 	 	 	 	 	 	 	 a d d P o i n t s B u t t o n ;  
 	 	 p r i v a t e   G l y p h B u t t o n 	 	 	 	 	 	 	 	 	 r e m o v e P o i n t s B u t t o n ;  
 	 	 p r i v a t e   G l y p h B u t t o n 	 	 	 	 	 	 	 	 	 u p P o i n t s B u t t o n ;  
 	 	 p r i v a t e   G l y p h B u t t o n 	 	 	 	 	 	 	 	 	 d o w n P o i n t s B u t t o n ;  
 	 	 p r i v a t e   S c r o l l L i s t 	 	 	 	 	 	 	 	 	 p o i n t s S c r o l l L i s t ;  
 	 	  
 	 	 p r i v a t e   T e x t F i e l d E x 	 	 	 	 	 	 	 	 	 p o i n t V a l u e F i e l d ;  
 	 }  
 }  
 