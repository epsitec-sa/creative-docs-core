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
 u s i n g   E p s i t e c . C r e s u s . C o r e . E n t i t i e s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . W i d g e t s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . D o c u m e n t s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . D o c u m e n t s . V e r b o s e ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . F a c t o r i e s ;  
  
 u s i n g   S y s t e m . T e x t . R e g u l a r E x p r e s s i o n s ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . C o n t r o l l e r s . S p e c i a l C o n t r o l l e r s  
 {  
 	 p u b l i c   c l a s s   S p e c i a l P a g e T y p e s C o n t r o l l e r   :   I E n t i t y S p e c i a l C o n t r o l l e r  
 	 {  
 	 	 p u b l i c   S p e c i a l P a g e T y p e s C o n t r o l l e r ( T i l e C o n t a i n e r   t i l e C o n t a i n e r ,   D o c u m e n t P r i n t i n g U n i t s E n t i t y   d o c u m e n t P r i n t i n g U n i t s E n t i t y )  
 	 	 {  
 	 	 	 t h i s . t i l e C o n t a i n e r   =   t i l e C o n t a i n e r ;  
 	 	 	 t h i s . d o c u m e n t P r i n t i n g U n i t s E n t i t y   =   d o c u m e n t P r i n t i n g U n i t s E n t i t y ;  
  
 	 	 	 t h i s . p a g e T y p e s   =   t h i s . d o c u m e n t P r i n t i n g U n i t s E n t i t y . G e t P a g e T y p e s   ( ) ;  
 	 	 	 t h i s . a l l P a g e T y p e s   =   V e r b o s e P a g e T y p e . G e t A l l   ( ) . T o L i s t   ( ) ;  
 	 	 	 t h i s . c h e c k B u t t o n s   =   n e w   L i s t < C h e c k B u t t o n >   ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   C r e a t e U I ( W i d g e t   p a r e n t ,   U I B u i l d e r   b u i l d e r ,   b o o l   i s R e a d O n l y )  
 	 	 {  
 	 	 	 t h i s . i s R e a d O n l y   =   i s R e a d O n l y ;  
  
 	 	 	 v a r   b o x   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . S t a c k e d ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   1 0 ,   0 ,   0 ) ,  
 	 	 	 } ;  
  
 	 	 	 n e w   S t a t i c T e x t  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 T e x t   =   " P a g e s   i m p r i m a b l e s   p a r   c e t t e   u n i t é   : " ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   5 ) ,  
 	 	 	 } ;  
  
 	 	 	 f o r e a c h   ( v a r   p a g e T y p e   i n   t h i s . a l l P a g e T y p e s )  
 	 	 	 {  
 	 	 	 	 v a r   b u t t o n   =   n e w   C h e c k B u t t o n  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 T e x t   =   p a g e T y p e . S h o r t D e s c r i p t i o n ,  
 	 	 	 	 	 N a m e   =   p a g e T y p e . T y p e . T o S t r i n g   ( ) ,  
 	 	 	 	 	 A c t i v e S t a t e   =   ( t h i s . p a g e T y p e s . C o n t a i n s   ( p a g e T y p e . T y p e ) )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ,  
 	 	 	 	 	 A u t o T o g g l e   =   f a l s e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 } ;  
  
 	 	 	 	 b u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 	 {  
 	 	 	 	 	 P a g e T y p e   t y p e ;  
 	 	 	 	 	 i f   ( S y s t e m . E n u m . T r y P a r s e   ( b u t t o n . N a m e ,   o u t   t y p e ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . A c t i o n T o g g l e   ( t y p e ) ;  
 	 	 	 	 	 }  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . c h e c k B u t t o n s . A d d   ( b u t t o n ) ;  
 	 	 	 }  
  
 	 	 	 n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 P r e f e r r e d H e i g h t   =   5 ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 } ;  
  
 	 	 	 / / 	 B a n d e s   p o u r   l e s   e x e m p l e s .  
 	 	 	 t h i s . s a m p l e 1   =   t h i s . C r e a t e U I S a m p l e B a n d   ( b o x ,   " D o c u m e n t   d ' u n e   s e u l e   p a g e   a v e c   B V   i n t é g r é   : " ) ;  
 	 	 	 t h i s . s a m p l e 2   =   t h i s . C r e a t e U I S a m p l e B a n d   ( b o x ,   " D o c u m e n t   d e   3   p a g e s   a v e c   B V   i n t é g r é   : " ) ;  
 	 	 	 t h i s . s a m p l e 3   =   t h i s . C r e a t e U I S a m p l e B a n d   ( b o x ,   " D o c u m e n t   d e   3   p a g e s   a v e c   B V   s é p a r é   : " ) ;  
  
 	 	 	 t h i s . U p d a t e W i d g e t s   ( ) ;  
 	 	 	 t h i s . U p d a t e S a m p l e s   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   F r a m e B o x   C r e a t e U I S a m p l e B a n d ( W i d g e t   p a r e n t ,   s t r i n g   t i t l e )  
 	 	 {  
 	 	 	 n e w   S e p a r a t o r  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 P r e f e r r e d H e i g h t   =   1 ,  
 	 	 	 	 I s H o r i z o n t a l L i n e   =   t r u e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   2 ,   2 ) ,  
 	 	 	 } ;  
  
 	 	 	 n e w   S t a t i c T e x t  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 T e x t   =   t i t l e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   4 ) ,  
 	 	 	 } ;  
  
 	 	 	 r e t u r n   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 P r e f e r r e d H e i g h t   =   7 2 ,  
 	 	 	 	 C o n t a i n e r L a y o u t M o d e   =   C o n t a i n e r L a y o u t M o d e . H o r i z o n t a l F l o w ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 } ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   A c t i o n T o g g l e ( P a g e T y p e   p a g e T y p e )  
 	 	 {  
 	 	 	 i f   ( t h i s . p a g e T y p e s . C o n t a i n s   ( p a g e T y p e ) )  
 	 	 	 {  
 	 	 	 	 t h i s . p a g e T y p e s . R e m o v e   ( p a g e T y p e ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . p a g e T y p e s . A d d   ( p a g e T y p e ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e W i d g e t s   ( ) ;  
 	 	 	 t h i s . U p d a t e S a m p l e s   ( ) ;  
 	 	 	 t h i s . S a v e   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S a v e ( )  
 	 	 {  
 	 	 	 t h i s . d o c u m e n t P r i n t i n g U n i t s E n t i t y . S e t P a g e T y p e s   ( t h i s . p a g e T y p e s ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   U p d a t e S a m p l e s ( )  
 	 	 {  
 	 	 	 i n t   t 1 1   =   0 ;     / /   p a g e   s e u l e   a v e c   B V  
 	 	 	 i n t   t 2 1   =   0 ;     / /   p r e m i è r e   p a g e   a v e c   B V  
 	 	 	 i n t   t 2 n   =   0 ;     / /   p a g e   s u i v a n t e   a v e c   B V  
 	 	 	 i n t   t 3 1   =   0 ;     / /   p r e m è r e   p a g e   s a n s   B V  
 	 	 	 i n t   t 3 n   =   0 ;     / /   p a g e   s u i v a n t e   s a n s   B V  
 	 	 	 i n t   t 3 b   =   0 ;     / /   B V   s e u l  
  
 	 	 	 i f   ( t h i s . p a g e T y p e s . C o n t a i n s   ( P a g e T y p e . A l l ) )  
 	 	 	 {  
 	 	 	 	 t 1 1 + + ;  
 	 	 	 	 t 2 1 + + ;  
 	 	 	 	 t 2 n + + ;  
 	 	 	 	 t 3 1 + + ;  
 	 	 	 	 t 3 n + + ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . p a g e T y p e s . C o n t a i n s   ( P a g e T y p e . C o p y ) )  
 	 	 	 {  
 	 	 	 	 t 1 1 + + ;  
 	 	 	 	 t 2 1 + + ;  
 	 	 	 	 t 2 n + + ;  
 	 	 	 	 t 3 1 + + ;  
 	 	 	 	 t 3 n + + ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . p a g e T y p e s . C o n t a i n s   ( P a g e T y p e . S i n g l e ) )  
 	 	 	 {  
 	 	 	 	 t 1 1 + + ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . p a g e T y p e s . C o n t a i n s   ( P a g e T y p e . F i r s t ) )  
 	 	 	 {  
 	 	 	 	 t 2 1 + + ;  
 	 	 	 	 t 3 1 + + ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . p a g e T y p e s . C o n t a i n s   ( P a g e T y p e . F o l l o w i n g ) )  
 	 	 	 {  
 	 	 	 	 t 2 n + + ;  
 	 	 	 	 t 3 n + + ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . p a g e T y p e s . C o n t a i n s   ( P a g e T y p e . I s r ) )  
 	 	 	 {  
 	 	 	 	 t 3 b + + ;  
 	 	 	 }  
  
 	 	 	 t h i s . s a m p l e 1 . C h i l d r e n . C l e a r   ( ) ;  
 	 	 	 t h i s . C r e a t e S a m p l e   ( t h i s . s a m p l e 1 ,   S a m p l e P a g e . P a g e T y p e E n u m . W i t h I s r ,         " p 1 " ,   t 1 1 ) ;  
  
 	 	 	 t h i s . s a m p l e 2 . C h i l d r e n . C l e a r   ( ) ;  
 	 	 	 t h i s . C r e a t e S a m p l e   ( t h i s . s a m p l e 2 ,   S a m p l e P a g e . P a g e T y p e E n u m . W i t h I s r ,         " p 1 " ,   t 2 1 ) ;  
 	 	 	 t h i s . C r e a t e S a m p l e   ( t h i s . s a m p l e 2 ,   S a m p l e P a g e . P a g e T y p e E n u m . W i t h I s r ,         " p 2 " ,   t 2 n ) ;  
 	 	 	 t h i s . C r e a t e S a m p l e   ( t h i s . s a m p l e 2 ,   S a m p l e P a g e . P a g e T y p e E n u m . W i t h I s r ,         " p 3 " ,   t 2 n ) ;  
  
 	 	 	 t h i s . s a m p l e 3 . C h i l d r e n . C l e a r   ( ) ;  
 	 	 	 t h i s . C r e a t e S a m p l e   ( t h i s . s a m p l e 3 ,   S a m p l e P a g e . P a g e T y p e E n u m . W i t h o u t I s r ,   " p 1 " ,   t 3 1 ) ;  
 	 	 	 t h i s . C r e a t e S a m p l e   ( t h i s . s a m p l e 3 ,   S a m p l e P a g e . P a g e T y p e E n u m . W i t h o u t I s r ,   " p 2 " ,   t 3 n ) ;  
 	 	 	 t h i s . C r e a t e S a m p l e   ( t h i s . s a m p l e 3 ,   S a m p l e P a g e . P a g e T y p e E n u m . W i t h o u t I s r ,   " p 3 " ,   t 3 n ) ;  
 	 	 	 t h i s . C r e a t e S a m p l e   ( t h i s . s a m p l e 3 ,   S a m p l e P a g e . P a g e T y p e E n u m . S i n g l e I s r ,     " B V " ,   t 3 b ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C r e a t e S a m p l e ( W i d g e t   p a r e n t ,   S a m p l e P a g e . P a g e T y p e E n u m   t y p e ,   s t r i n g   t e x t ,   i n t   c o p i e s )  
 	 	 {  
 	 	 	 s t r i n g   n x   =   " " ;  
  
 	 	 	 i f   ( c o p i e s   >   0 )  
 	 	 	 {  
 	 	 	 	 n x   =   s t r i n g . F o r m a t   ( " { 0 } × " ,   c o p i e s . T o S t r i n g   ( ) ) ;  
 	 	 	 }  
  
 	 	 	 n e w   S a m p l e P a g e  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   4 0 ,  
 	 	 	 	 P a g e T y p e   =   t y p e ,  
 	 	 	 	 P a g e T e x t   =   t e x t ,  
 	 	 	 	 P a g e B o t t o m L a b e l   =   n x ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   2 ,   0 ,   0 ) ,  
 	 	 	 } ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e W i d g e t s ( )  
 	 	 {  
 	 	 	 f o r e a c h   ( v a r   b u t t o n   i n   t h i s . c h e c k B u t t o n s )  
 	 	 	 {  
 	 	 	 	 P a g e T y p e   t y p e ;  
 	 	 	 	 i f   ( S y s t e m . E n u m . T r y P a r s e   ( b u t t o n . N a m e ,   o u t   t y p e ) )  
 	 	 	 	 {  
 	 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   ( t h i s . p a g e T y p e s . C o n t a i n s   ( t y p e ) )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   c l a s s   F a c t o r y   :   D e f a u l t E n t i t y S p e c i a l C o n t r o l l e r F a c t o r y < D o c u m e n t P r i n t i n g U n i t s E n t i t y >  
 	 	 {  
 	 	 	 p r o t e c t e d   o v e r r i d e   I E n t i t y S p e c i a l C o n t r o l l e r   C r e a t e ( T i l e C o n t a i n e r   c o n t a i n e r ,   D o c u m e n t P r i n t i n g U n i t s E n t i t y   e n t i t y ,   i n t   m o d e )  
 	 	 	 {  
 	 	 	 	 r e t u r n   n e w   S p e c i a l P a g e T y p e s C o n t r o l l e r   ( c o n t a i n e r ,   e n t i t y ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   r e a d o n l y   T i l e C o n t a i n e r 	 	 	 	 	 	 t i l e C o n t a i n e r ;  
 	 	 p r i v a t e   r e a d o n l y   D o c u m e n t P r i n t i n g U n i t s E n t i t y 	 	 d o c u m e n t P r i n t i n g U n i t s E n t i t y ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < P a g e T y p e > 	 	 	 	 	 	 p a g e T y p e s ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < V e r b o s e P a g e T y p e > 	 	 	 	 a l l P a g e T y p e s ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < C h e c k B u t t o n > 	 	 	 	 	 c h e c k B u t t o n s ;  
  
 	 	 p r i v a t e   b o o l 	 	 	 	 	 	 	 	 	 	 i s R e a d O n l y ;  
 	 	 p r i v a t e   F r a m e B o x 	 	 	 	 	 	 	 	 	 s a m p l e 1 ;  
 	 	 p r i v a t e   F r a m e B o x 	 	 	 	 	 	 	 	 	 s a m p l e 2 ;  
 	 	 p r i v a t e   F r a m e B o x 	 	 	 	 	 	 	 	 	 s a m p l e 3 ;  
 	 }  
 }  
 