ÿþ/ / 	 C o p y r i g h t   ©   2 0 1 0 ,   E P S I T E C   S A ,   C H - 1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . T a b l e D e s i g n e r  
 {  
 	 p u b l i c   c l a s s   D e s i g n e r T a b l e  
 	 {  
 	 	 p u b l i c   D e s i g n e r T a b l e ( )  
 	 	 {  
 	 	 	 t h i s . d i m e n s i o n s   =   n e w   L i s t < D e s i g n e r D i m e n s i o n >   ( ) ;  
 	 	 	 t h i s . v a l u e s   =   n e w   D e s i g n e r V a l u e s   ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   L i s t < D e s i g n e r D i m e n s i o n >   D i m e n s i o n s  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . d i m e n s i o n s ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   D e s i g n e r V a l u e s   V a l u e s  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . v a l u e s ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   D e s i g n e r D i m e n s i o n   G e t D i m e n s i o n ( s t r i n g   c o d e )  
 	 	 {  
 	 	 	 r e t u r n   t h i s . d i m e n s i o n s . W h e r e   ( x   = >   x . C o d e   = =   c o d e ) . F i r s t O r D e f a u l t   ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   C l e a n U p ( )  
 	 	 {  
 	 	 	 / / 	 S u p p r i m e   l e s   p o i n t s   d o u b l o n s   d a n s   t o u t e s   l e s   d i m e n s i o n s .  
 	 	 	 v a r   d a t a   =   t h i s . E x p o r t V a l u e s   ( ) ;  
  
 	 	 	 f o r e a c h   ( v a r   d i m e n s i o n   i n   t h i s . d i m e n s i o n s )  
 	 	 	 {  
 	 	 	 	 d i m e n s i o n . C l e a n U p   ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . I m p o r t V a l u e s   ( d a t a ) ;  
 	 	 }  
  
  
 	 	 # r e g i o n   R o b u s t   i m p o r t / e x p o r t  
 	 	 p u b l i c   R o b u s t D a t a   E x p o r t V a l u e s ( )  
 	 	 {  
 	 	 	 / / 	 E x p o r t e   t o u t e s   l e s   v a l e u r s   d a n s   u n   f o r m a t   r o b u s t e ,   r é s i s t a n t   a u x   m o d i f i c a t i o n s   d e s   p o i n t s   d e s   d i m e n s i o n s .  
 	 	 	 / / 	 C e c i   q u i   p e r m e t   d ' a j u s t e r   i n t e l l i g e m e n t   l e s   v a l e u r s   l o r s   d e   l ' i m p o r t a t i o n ,   s u i t e   à   n ' i m p o r t e s   q u e l l e s  
 	 	 	 / / 	 m o d i f i c a t i o n s   d e s   p o i n t s   d e s   d i m e n s i o n s   ( a j o u t ,   s u p p r e s s i o n ,   d é p l a c e m e n t   o u   m o d i f i c a t i o n ) .   S i   u n   p o i n t  
 	 	 	 / / 	 d é c i m a l   e s t   m o d i f i é ,   l e s   v a l e u r s   c o r r e s p o n d a n t e s   s o n t   a j u s t é e s   p a r   i n t e r p o l a t i o n   l i n é a i r e .  
 	 	 	 / / 	 E n   r e v a n c h e ,   l e   f o r m a t   n e   r é s i s t e   p a s   a u x   m o d i f i c a t i o n s   d e s   d i m e n s i o n s   e l l e s - m ê m e s   ( a j o u t   o u   s u p p r e s s i o n  
 	 	 	 / / 	 d ' u n e   d i m e n s i o n   p a r   e x e m p l e ) .  
 	 	 	 / /  
 	 	 	 / / 	 D a n s   c e t   e x e m p l e   à   d e u x   d i m e n s i o n s ,   l e   p o i n t   1 5   a   é t é   a j o u t é   :  
 	 	 	 / /  
 	 	 	 / / 	                   1 0           2 0                                       1 0           1 5           2 0  
 	 	 	 / / 	             + - - - - - - + - - - - - - +                           + - - - - - - + - - - - - - + - - - - - - +  
 	 	 	 / / 	     2 0 0   |   4 . 0 0   |   8 . 0 0   |       - - >       2 0 0   |   4 . 0 0   |             |   8 . 0 0   |  
 	 	 	 / / 	             + - - - - - - + - - - - - - +                           + - - - - - - + - - - - - - + - - - - - - +  
 	 	 	 / / 	     3 0 0   |   5 . 0 0   |   9 . 0 0   |                   3 0 0   |   5 . 0 0   |             |   9 . 0 0   |  
 	 	 	 / / 	             + - - - - - - + - - - - - - +                           + - - - - - - + - - - - - - + - - - - - - +  
 	 	 	 / /  
 	 	 	 / / 	 D a n s   c e t   e x e m p l e   à   d e u x   d i m e n s i o n s ,   l e   p o i n t   3 0 0   a   é t é   m o d i f i é   e n   2 5 0   :  
 	 	 	 / /  
 	 	 	 / / 	                   1 0           2 0                                       1 0           2 0  
 	 	 	 / / 	             + - - - - - - + - - - - - - +                           + - - - - - - + - - - - - - +  
 	 	 	 / / 	     2 0 0   |   4 . 0 0   |   8 . 0 0   |       - - >       2 0 0   |   4 . 0 0   |   8 . 0 0   |  
 	 	 	 / / 	             + - - - - - - + - - - - - - +                           + - - - - - - + - - - - - - +  
 	 	 	 / / 	     3 0 0   |   5 . 0 0   |   9 . 0 0   |                   2 5 0   |   4 . 1 6   |   7 . 5 0   |  
 	 	 	 / / 	             + - - - - - - + - - - - - - +                           + - - - - - - + - - - - - - +  
  
 	 	 	 v a r   d a t a   =   n e w   R o b u s t D a t a   ( ) ;  
  
 	 	 	 f o r e a c h   ( v a r   d i m e n s i o n   i n   t h i s . d i m e n s i o n s )  
 	 	 	 {  
 	 	 	 	 d a t a . D i m e n s i o n s . A d d   ( n e w   D e s i g n e r D i m e n s i o n   ( d i m e n s i o n ) ) ;  
 	 	 	 }  
  
 	 	 	 f o r e a c h   ( v a r   p a i r   i n   t h i s . v a l u e s . D a t a )  
 	 	 	 {  
 	 	 	 	 s t r i n g   i n t K e y   =   p a i r . K e y ;  
 	 	 	 	 d e c i m a l   v a l u e   =   p a i r . V a l u e ;  
  
 	 	 	 	 d a t a . V a l u e s . A d d   ( t h i s . I n t K e y T o S t r i n g K e y   ( i n t K e y ) ,   v a l u e ) ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   d a t a ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   I m p o r t V a l u e s ( R o b u s t D a t a   d a t a )  
 	 	 {  
 	 	 	 / / 	 I m p o r t e   t o u t e s   l e s   v a l e u r s   à   p a r t i r   d e s   d o n n é e s   r o b u s t e s .  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( d a t a . D i m e n s i o n s . C o u n t   = =   t h i s . d i m e n s i o n s . C o u n t ) ;  
  
 	 	 	 f o r   ( i n t   i = 0 ;   i < t h i s . d i m e n s i o n s . C o u n t ;   i + + )  
 	 	 	 {  
 	 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( d a t a . D i m e n s i o n s [ i ] . C o d e   = =   t h i s . d i m e n s i o n s [ i ] . C o d e ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . v a l u e s . C l e a r   ( ) ;  
  
 	 	 	 f o r e a c h   ( v a r   p a i r   i n   d a t a . V a l u e s )  
 	 	 	 {  
 	 	 	 	 s t r i n g   s t r i n g K e y   =   p a i r . K e y ;  
 	 	 	 	 d e c i m a l   v a l u e   =   p a i r . V a l u e ;  
  
 	 	 	 	 i n t [ ]   i n t K e y ;  
 	 	 	 	 i f   ( t h i s . S t r i n g K e y T o I n t K e y   ( d a t a ,   s t r i n g K e y ,   o u t   i n t K e y ,   r e f   v a l u e ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . v a l u e s . S e t V a l u e   ( i n t K e y ,   v a l u e ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   s t r i n g   I n t K e y T o S t r i n g K e y ( s t r i n g   i n t K e y )  
 	 	 {  
 	 	 	 / / 	 T r a n s f o r m e   u n   c l é   " 0 . 1 4 . 2 "   e n   u n e   c l é   " 4 0 0 . 1 2 0 0 . S T D - 1 " .  
 	 	 	 v a r   l i s t   =   t h i s . I n t K e y T o S t r i n g K e y A r r a y   ( i n t K e y ) ;  
 	 	 	 r e t u r n   s t r i n g . J o i n   ( " . " ,   l i s t ) ;  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   S t r i n g K e y T o I n t K e y ( R o b u s t D a t a   d a t a ,   s t r i n g   s t r i n g K e y ,   o u t   i n t [ ]   i n t K e y ,   r e f   d e c i m a l   v a l u e )  
 	 	 {  
 	 	 	 r e t u r n   t h i s . S t r i n g K e y T o I n t K e y   ( d a t a . D i m e n s i o n s ,   s t r i n g K e y ,   o u t   i n t K e y ,   r e f   v a l u e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   L i s t < s t r i n g >   G e t M o d i f i e d P o i n t s ( L i s t < s t r i n g >   o l d P o i n t s ,   L i s t < s t r i n g >   n e w P o i n t s )  
 	 	 {  
 	 	 	 v a r   l i s t   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 f o r e a c h   ( v a r   p   i n   n e w P o i n t s )  
 	 	 	 {  
 	 	 	 	 i f   ( ! o l d P o i n t s . C o n t a i n s   ( p ) )  
 	 	 	 	 {  
 	 	 	 	 	 l i s t . A d d   ( p ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   l i s t ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   i n t   N e a r e s t ( L i s t < s t r i n g >   p o i n t s ,   d e c i m a l   p o i n t )  
 	 	 {  
 	 	 	 d e c i m a l   d e l t a   =   d e c i m a l . M a x V a l u e ;  
 	 	 	 i n t   i n d e x   =   - 1 ;  
  
 	 	 	 f o r   ( i n t   i   =   0 ;   i   <   p o i n t s . C o u n t ;   i + + )  
 	 	 	 {  
 	 	 	 	 d e c i m a l   p   =   d e c i m a l . P a r s e   ( p o i n t s [ i ] ) ;  
 	 	 	 	 d e c i m a l   d   =   S y s t e m . M a t h . A b s   ( p - p o i n t ) ;  
  
 	 	 	 	 i f   ( d e l t a   >   d )  
 	 	 	 	 {  
 	 	 	 	 	 d e l t a   =   d ;  
 	 	 	 	 	 i n d e x   =   i ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   i n d e x ;  
 	 	 }  
  
  
 	 	 p u b l i c   c l a s s   R o b u s t D a t a  
 	 	 {  
 	 	 	 p u b l i c   R o b u s t D a t a ( )  
 	 	 	 {  
 	 	 	 	 t h i s . d i m e n s i o n s   =   n e w   L i s t < D e s i g n e r D i m e n s i o n >   ( ) ;  
 	 	 	 	 t h i s . v a l u e s   =   n e w   D i c t i o n a r y < s t r i n g ,   d e c i m a l >   ( ) ;  
 	 	 	 }  
  
 	 	 	 p u b l i c   L i s t < D e s i g n e r D i m e n s i o n >   D i m e n s i o n s  
 	 	 	 {  
 	 	 	 	 g e t  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   t h i s . d i m e n s i o n s ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p u b l i c   D i c t i o n a r y < s t r i n g ,   d e c i m a l >   V a l u e s  
 	 	 	 {  
 	 	 	 	 g e t  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   t h i s . v a l u e s ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r i v a t e   r e a d o n l y   L i s t < D e s i g n e r D i m e n s i o n > 	 	 	 d i m e n s i o n s ;  
 	 	 	 p r i v a t e   r e a d o n l y   D i c t i o n a r y < s t r i n g ,   d e c i m a l > 	 	 v a l u e s ;  
 	 	 }  
 	 	 # e n d r e g i o n  
  
  
 	 	 p u b l i c   s t r i n g [ ]   I n t K e y T o S t r i n g K e y A r r a y ( s t r i n g   i n t K e y )  
 	 	 {  
 	 	 	 / / 	 T r a n s f o r m e   u n   c l é   " 0 . 1 4 . 2 "   e n   u n e   c l é   " 4 0 0 . 1 2 0 0 . S T D - 1 " .  
 	 	 	 s t r i n g [ ]   p a r t s   =   i n t K e y . S p l i t   ( ' . ' ) ;  
 	 	 	 s t r i n g [ ]   l i s t   =   n e w   s t r i n g [ p a r t s . L e n g t h ] ;  
  
 	 	 	 f o r   ( i n t   i = 0 ;   i < p a r t s . L e n g t h ;   i + + )  
 	 	 	 {  
 	 	 	 	 i n t   j   =   i n t . P a r s e   ( p a r t s [ i ] ) ;  
 	 	 	 	 l i s t [ i ]   =   t h i s . d i m e n s i o n s [ i ] . P o i n t s [ j ] ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   l i s t ;  
 	 	 }  
  
 	 	 p u b l i c   b o o l   S t r i n g K e y T o I n t K e y ( L i s t < D e s i g n e r D i m e n s i o n >   d i m e n s i o n s ,   s t r i n g   s t r i n g K e y ,   o u t   i n t [ ]   i n t K e y ,   r e f   d e c i m a l   v a l u e )  
 	 	 {  
 	 	 	 / / 	 T r a n s f o r m e   u n   c l é   " 4 0 0 . 1 2 0 0 . S T D - 1 "   e n   u n e   c l é   " 0 . 1 4 . 2 " .  
 	 	 	 / / 	 S i   l e   p o i n t   n ' e x i s t e   p l u s ,   o n   c h e r c h e   l e   p l u s   p r o c h e   e t   o n   a j u s t e   l a   v a l e u r .  
 	 	 	 s t r i n g [ ]   p a r t s   =   s t r i n g K e y . S p l i t   ( ' . ' ) ;  
 	 	 	 i n t K e y   =   n e w   i n t [ p a r t s . L e n g t h ] ;  
  
 	 	 	 f o r   ( i n t   i = 0 ;   i < p a r t s . L e n g t h ;   i + + )  
 	 	 	 {  
 	 	 	 	 s t r i n g   s   =   p a r t s [ i ] ;  
 	 	 	 	 i n t   j   =   t h i s . d i m e n s i o n s [ i ] . P o i n t s . I n d e x O f   ( s ) ;  
  
 	 	 	 	 i f   ( j   = =   - 1 )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ! t h i s . d i m e n s i o n s [ i ] . H a s D e c i m a l )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 d e c i m a l   i n i t i a l ;  
 	 	 	 	 	 i f   ( ! d e c i m a l . T r y P a r s e   ( s ,   o u t   i n i t i a l ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 v a r   p o i n t s   =   D e s i g n e r T a b l e . G e t M o d i f i e d P o i n t s   ( d i m e n s i o n s [ i ] . P o i n t s ,   t h i s . d i m e n s i o n s [ i ] . P o i n t s ) ;  
 	 	 	 	 	 j   =   D e s i g n e r T a b l e . N e a r e s t   ( p o i n t s ,   i n i t i a l ) ;  
  
 	 	 	 	 	 i f   ( i n i t i a l   = =   0   | |   j   = =   - 1 )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 d e c i m a l   e x i s t i n g   =   d e c i m a l . P a r s e   ( p o i n t s [ j ] ) ;  
  
 	 	 	 	 	 v a l u e   * =   e x i s t i n g / i n i t i a l ;     / /   i n t e r p o l a t i o n   l i n é a i r e  
  
 	 	 	 	 	 j   =   t h i s . d i m e n s i o n s [ i ] . P o i n t s . I n d e x O f   ( p o i n t s [ j ] ) ;  
 	 	 	 	 }  
  
 	 	 	 	 i n t K e y [ i ]   =   j ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
  
 	 	 p r i v a t e   r e a d o n l y   L i s t < D e s i g n e r D i m e n s i o n > 	 	 d i m e n s i o n s ;  
 	 	 p r i v a t e   r e a d o n l y   D e s i g n e r V a l u e s 	 	 	 	 	 v a l u e s ;  
 	 }  
 }  
 