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
  
 u s i n g   S y s t e m . T e x t . R e g u l a r E x p r e s s i o n s ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . T a b l e D e s i g n e r  
 {  
 	 p u b l i c   c l a s s   M a i n C o n t r o l l e r  
 	 {  
 	 	 p u b l i c   M a i n C o n t r o l l e r ( W i d g e t   p a r e n t ,   C o r e . B u s i n e s s . B u s i n e s s C o n t e x t   b u s i n e s s C o n t e x t ,   P r i c e C a l c u l a t o r E n t i t y   p r i c e C a l c u l a t o r E n t i t y ,   A r t i c l e D e f i n i t i o n E n t i t y   a r t i c l e D e f i n i t i o n E n t i t y )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t                   =   b u s i n e s s C o n t e x t ;  
 	 	 	 t h i s . p r i c e C a l c u l a t o r E n t i t y       =   p r i c e C a l c u l a t o r E n t i t y ;  
 	 	 	 t h i s . a r t i c l e D e f i n i t i o n E n t i t y   =   a r t i c l e D e f i n i t i o n E n t i t y ;  
  
 	 	 	 t h i s . d i m e n s i o n T a b l e   =   t h i s . p r i c e C a l c u l a t o r E n t i t y . G e t P r i c e T a b l e   ( ) ;  
  
 	 	 	 t h i s . t a b l e   =   n e w   D e s i g n e r T a b l e   ( ) ;  
 	 	 	 t h i s . E x t r a c t D i m e n s i o n s   ( ) ;  
 	 	 	 t h i s . E x t r a c t V a l u e s   ( ) ;  
  
 	 	 	 i f   ( ! t h i s . C h e c k D i m e n s i o n s   ( ) )  
 	 	 	 {  
 	 	 	 	 s t r i n g   m e s s a g e   =   " U n   o u   p l u s i e u r s   a x e s   d e s   t a b e l l e s   d e   p r i x   n e   c o r r e s p o n d e n t   p l u s   a u x < b r / > "   +  
 	 	 	 	 	 	 	 	   " p a r a m è t r e s   d e   l ' a r t i c l e .   E n   c o n s é q u e n c e ,   t o u s   l e s   p r i x   s e r o n t   e f f a c é s . " ;  
 	 	 	 	  
 	 	 	 	 M e s s a g e D i a l o g . S h o w M e s s a g e   ( m e s s a g e ,   p a r e n t . W i n d o w ) ;  
  
 	 	 	 	 t h i s . t a b l e . V a l u e s . C l e a r   ( ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   C r e a t e U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 v a r   t a b B o o k   =   n e w   T a b B o o k  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 A r r o w s   =   T a b B o o k A r r o w s . R i g h t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 } ;  
  
 	 	 	 / / 	 C r é e   l e s   o n g l e t s .  
 	 	 	 v a r   b a s e P a g e   =   n e w   T a b P a g e  
 	 	 	 {  
 	 	 	 	 N a m e   =   " b a s e " ,  
 	 	 	 	 T a b T i t l e   =   " G é n é r a l " ,  
 	 	 	 } ;  
  
 	 	 	 v a r   d i m e n s i o n s P a g e   =   n e w   T a b P a g e  
 	 	 	 {  
 	 	 	 	 N a m e   =   " d i m e n s i o n s " ,  
 	 	 	 	 T a b T i t l e   =   " D é f i n i t i o n   d e s   a x e s " ,  
 	 	 	 } ;  
  
 	 	 	 v a r   v a l u e s P a g e   =   n e w   T a b P a g e  
 	 	 	 {  
 	 	 	 	 N a m e   =   " v a l u e s " ,  
 	 	 	 	 T a b T i t l e   =   " T a b e l l e s   d e   p r i x " ,  
 	 	 	 } ;  
  
 	 	 	 / / ? t a b B o o k . I t e m s . A d d   ( b a s e P a g e ) ;  
 	 	 	 t a b B o o k . I t e m s . A d d   ( d i m e n s i o n s P a g e ) ;  
 	 	 	 t a b B o o k . I t e m s . A d d   ( v a l u e s P a g e ) ;  
  
 	 	 	 i f   ( t h i s . t a b l e . D i m e n s i o n s . C o u n t   = =   0 )  
 	 	 	 {  
 	 	 	 	 t a b B o o k . A c t i v e P a g e   =   d i m e n s i o n s P a g e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t a b B o o k . A c t i v e P a g e   =   v a l u e s P a g e ;  
 	 	 	 }  
  
 	 	 	 / / 	 P e u p l e   l e s   o n g l e t s .  
 	 	 	 v a r   b c   =   n e w   B a s e C o n t r o l l e r   ( t h i s . p r i c e C a l c u l a t o r E n t i t y ) ;  
 	 	 	 b c . C r e a t e U I   ( b a s e P a g e ) ;  
  
 	 	 	 v a r   d c   =   n e w   D i m e n s i o n s C o n t r o l l e r   ( t h i s . b u s i n e s s C o n t e x t ,   t h i s . a r t i c l e D e f i n i t i o n E n t i t y ,   t h i s . t a b l e ) ;  
 	 	 	 d c . C r e a t e U I   ( d i m e n s i o n s P a g e ) ;  
  
 	 	 	 v a r   t c   =   n e w   T a b l e C o n t r o l l e r   ( t h i s . b u s i n e s s C o n t e x t ,   t h i s . t a b l e ) ;  
 	 	 	 t c . C r e a t e U I   ( v a l u e s P a g e ) ;  
  
 	 	 	 / / 	 C o n n e c t i o n   d e s   é v é n e m e n t s .  
 	 	 	 t a b B o o k . A c t i v e P a g e C h a n g e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 i f   ( t a b B o o k . A c t i v e P a g e . N a m e   = =   " b a s e " )  
 	 	 	 	 {  
 	 	 	 	 	 b c . U p d a t e   ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t a b B o o k . A c t i v e P a g e . N a m e   = =   " d i m e n s i o n s " )  
 	 	 	 	 {  
 	 	 	 	 	 d c . U p d a t e   ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t a b B o o k . A c t i v e P a g e . N a m e   = =   " v a l u e s " )  
 	 	 	 	 {  
 	 	 	 	 	 t c . U p d a t e   ( ) ;  
 	 	 	 	 }  
 	 	 	 } ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   S a v e D e s i g n ( )  
 	 	 {  
 	 	 	 v a r   d i m e n s i o n T a b l e   =   t h i s . C o l l e c t D i m e n s i o n s A n d V a l u e s   ( ) ;  
  
 	 	 	 t h i s . p r i c e C a l c u l a t o r E n t i t y . S e t P r i c e T a b l e   ( d i m e n s i o n T a b l e ) ;  
 	 	 	 t h i s . p r i c e C a l c u l a t o r E n t i t y . I n f o r m a t i o n s   =   t h i s . G e t I n f o r m a t i o n s   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   s t r i n g   G e t I n f o r m a t i o n s ( )  
 	 	 {  
 	 	 	 v a r   b u i l d e r   =   n e w   S y s t e m . T e x t . S t r i n g B u i l d e r   ( ) ;  
  
 	 	 	 i n t   c o u n t   =   t h i s . t a b l e . D i m e n s i o n s . C o u n t   ( ) ;  
 	 	 	 b u i l d e r . A p p e n d   ( c o u n t . T o S t r i n g   ( ) ) ;  
 	 	 	 b u i l d e r . A p p e n d   ( c o u n t   < =   1   ?   "   a x e "   :   "   a x e s " ) ;  
 	 	 	 b u i l d e r . A p p e n d   ( " :   " ) ;  
  
 	 	 	 f o r   ( i n t   i   =   0 ;   i   <   c o u n t ;   i + + )  
 	 	 	 {  
 	 	 	 	 v a r   d i m e n s i o n   =   t h i s . t a b l e . D i m e n s i o n s [ i ] ;  
 	 	 	 	 b u i l d e r . A p p e n d   ( d i m e n s i o n . N a m e ) ;  
  
 	 	 	 	 i f   ( i   <   c o u n t - 1 )  
 	 	 	 	 {  
 	 	 	 	 	 b u i l d e r . A p p e n d   ( " ,   " ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   b u i l d e r . T o S t r i n g   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   C h e c k D i m e n s i o n s ( )  
 	 	 {  
 	 	 	 / / 	 V é r i f i e   s i   t o u t e s   l e s   d i m e n s i o n s   c o r r e s p o n d e n t   à   u n   p a r a m è t r e   d e   l ' a r t i c l e .  
 	 	 	 / / 	 S i   n o n ,   l a   o u   l e s   d i m e n s i o n s   c o n c e r n é e s   s o n t   s u p p r i m é e s ,   e t   l ' a p p e l   r e t o u r n e   f a l s e .  
 	 	 	 b o o l   o k   =   t r u e ;  
 	 	 	 i n t   i   =   0 ;  
  
 	 	 	 w h i l e   ( i   <   t h i s . t a b l e . D i m e n s i o n s . C o u n t )  
 	 	 	 {  
 	 	 	 	 v a r   d i m e n s i o n   =   t h i s . t a b l e . D i m e n s i o n s [ i ] ;  
 	 	 	 	 s t r i n g   c o d e   =   d i m e n s i o n . C o d e ;  
 	 	 	 	 v a r   p   =   t h i s . a r t i c l e D e f i n i t i o n E n t i t y . A r t i c l e P a r a m e t e r D e f i n i t i o n s . W h e r e   ( x   = >   x . C o d e   = =   c o d e ) . F i r s t O r D e f a u l t   ( ) ;  
  
 	 	 	 	 i f   ( p   = =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . t a b l e . D i m e n s i o n s . R e m o v e A t   ( i ) ;  
 	 	 	 	 	 o k   =   f a l s e ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 i + + ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   o k ;  
 	 	 }  
  
  
 	 	 # r e g i o n   E x t r a c t   a n d   c o l l e c t   d i m e n s i o n s   a n d   v a l u e s  
 	 	 p r i v a t e   v o i d   E x t r a c t D i m e n s i o n s ( )  
 	 	 {  
 	 	 	 i f   ( t h i s . d i m e n s i o n T a b l e   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 f o r e a c h   ( v a r   d i m e n s i o n   i n   t h i s . d i m e n s i o n T a b l e . D i m e n s i o n s )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   d d   =   n e w   D e s i g n e r D i m e n s i o n   ( d i m e n s i o n ) ;  
  
 	 	 	 	 	 d d . N a m e                 =   t h i s . G e t D i m e n s i o n N a m e   ( d i m e n s i o n . C o d e ) ;  
 	 	 	 	 	 d d . D e s c r i p t i o n   =   t h i s . G e t D i m e n s i o n D e s c r i p t i o n   ( d i m e n s i o n . C o d e ) ;  
  
 	 	 	 	 	 t h i s . t a b l e . D i m e n s i o n s . A d d   ( d d ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   E x t r a c t V a l u e s ( )  
 	 	 {  
 	 	 	 i f   ( t h i s . d i m e n s i o n T a b l e   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 f o r e a c h   ( v a r   p a i r   i n   t h i s . d i m e n s i o n T a b l e . D e f i n e d E n t r i e s )  
 	 	 	 	 {  
 	 	 	 	 	 i n t [ ]   k e y   =   t h i s . d i m e n s i o n T a b l e . G e t I n d e x e s F r o m K e y   ( p a i r . K e y ) ;  
 	 	 	 	 	 d e c i m a l   v a l u e   =   p a i r . V a l u e ;  
  
 	 	 	 	 	 t h i s . t a b l e . V a l u e s . S e t V a l u e   ( k e y ,   v a l u e ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   D i m e n s i o n T a b l e   C o l l e c t D i m e n s i o n s A n d V a l u e s ( )  
 	 	 {  
 	 	 	 t h i s . t a b l e . C l e a n U p   ( ) ;  
  
 	 	 	 v a r   a b s t r a c t D i m e n s i o n s   =   n e w   L i s t < A b s t r a c t D i m e n s i o n >   ( ) ;  
  
 	 	 	 f o r e a c h   ( v a r   d i m e n s i o n   i n   t h i s . t a b l e . D i m e n s i o n s )  
 	 	 	 {  
 	 	 	 	 i f   ( d i m e n s i o n . H a s D e c i m a l )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   n   =   n e w   N u m e r i c D i m e n s i o n   ( d i m e n s i o n . C o d e ,   d i m e n s i o n . R o u n d i n g M o d e ,   d i m e n s i o n . D e c i m a l P o i n t s ) ;  
 	 	 	 	 	 a b s t r a c t D i m e n s i o n s . A d d   ( n ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 v a r   c   =   n e w   C o d e D i m e n s i o n   ( d i m e n s i o n . C o d e ,   d i m e n s i o n . P o i n t s ) ;  
 	 	 	 	 	 a b s t r a c t D i m e n s i o n s . A d d   ( c ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 v a r   d i m e n s i o n T a b l e   =   n e w   D i m e n s i o n T a b l e   ( a b s t r a c t D i m e n s i o n s . T o A r r a y   ( ) ) ;  
  
 	 	 	 f o r e a c h   ( v a r   p a i r   i n   t h i s . t a b l e . V a l u e s . D a t a )  
 	 	 	 {  
 	 	 	 	 s t r i n g   i n t K e y   =   p a i r . K e y ;  
 	 	 	 	 d e c i m a l   v a l u e   =   p a i r . V a l u e ;  
  
 	 	 	 	 s t r i n g [ ]   s t r i n g K e y   =   t h i s . t a b l e . I n t K e y T o S t r i n g K e y A r r a y   ( i n t K e y ) ;  
  
 	 	 	 	 d i m e n s i o n T a b l e [ s t r i n g K e y ]   =   v a l u e ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   d i m e n s i o n T a b l e ;  
 	 	 }  
  
 	 	 p r i v a t e   F o r m a t t e d T e x t   G e t D i m e n s i o n N a m e ( s t r i n g   c o d e )  
 	 	 {  
 	 	 	 v a r   p   =   t h i s . a r t i c l e D e f i n i t i o n E n t i t y . A r t i c l e P a r a m e t e r D e f i n i t i o n s . W h e r e   ( x   = >   x . C o d e   = =   c o d e ) . F i r s t O r D e f a u l t   ( ) ;  
  
 	 	 	 i f   ( p   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 r e t u r n   " ? " ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 r e t u r n   p . N a m e ;  
 	 	 	 }  
 	 	 }  
 	 	  
 	 	 p r i v a t e   F o r m a t t e d T e x t   G e t D i m e n s i o n D e s c r i p t i o n ( s t r i n g   c o d e )  
 	 	 {  
 	 	 	 v a r   p   =   t h i s . a r t i c l e D e f i n i t i o n E n t i t y . A r t i c l e P a r a m e t e r D e f i n i t i o n s . W h e r e   ( x   = >   x . C o d e   = =   c o d e ) . F i r s t O r D e f a u l t   ( ) ;  
  
 	 	 	 i f   ( p   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 r e t u r n   " " ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 r e t u r n   p . D e s c r i p t i o n ;  
 	 	 	 }  
 	 	 }  
 	 	 # e n d r e g i o n  
  
  
 	 	 p r i v a t e   r e a d o n l y   C o r e . B u s i n e s s . B u s i n e s s C o n t e x t 	 	 b u s i n e s s C o n t e x t ;  
 	 	 p r i v a t e   r e a d o n l y   P r i c e C a l c u l a t o r E n t i t y 	 	 	 	 p r i c e C a l c u l a t o r E n t i t y ;  
 	 	 p r i v a t e   r e a d o n l y   A r t i c l e D e f i n i t i o n E n t i t y 	 	 	 a r t i c l e D e f i n i t i o n E n t i t y ;  
 	 	 p r i v a t e   r e a d o n l y   D i m e n s i o n T a b l e 	 	 	 	 	 	 d i m e n s i o n T a b l e ;  
 	 	 p r i v a t e   r e a d o n l y   D e s i g n e r T a b l e 	 	 	 	 	 	 t a b l e ;  
 	 }  
 }  
 