ÿþ/ / 	 C o p y r i g h t   ©   2 0 1 0 ,   E P S I T E C   S A ,   C H - 1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
 u s i n g   E p s i t e c . C o m m o n . D i a l o g s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t . E n t i t y E n g i n e ;  
  
 u s i n g   E p s i t e c . C r e s u s . C o r e . E n t i t i e s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . D o c u m e n t s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . D o c u m e n t s . V e r b o s e ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . B u s i n e s s ;  
  
 u s i n g   S y s t e m . T e x t . R e g u l a r E x p r e s s i o n s ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . D o c u m e n t O p t i o n s C o n t r o l l e r  
 {  
 	 / / /   < s u m m a r y >  
 	 / / /   C e   c o n t r ô l e u r   p e r m e t   d e   s a i s i r   l e s   o p t i o n s   d ' i m p r e s s i o n .   U n   p a n n e a u   d ' e n v i r o n   3 0 0   p i x e l s   d e   l a r g e   e t   l e   p l u s   h a u t  
 	 / / /   p o s s i b l e   c o n t i e n t   d e s   b o u t o n s   à   c o c h e r   e t   d e s   b o u t o n s   r a d i o .   L e   m o d e   ' t h r e e S t a t e '   p e r m e t   d e   d é f i n i r   u n e   o p t i o n  
 	 / / /   c o m m e   é t a n t   " n o n   i m p o s é e " .   C ' e s t   a v e c   c e   m o d e   q u ' i l   e s t   n é c e s s a i r e   d e   d é f i n i r   d e u x   O p t i o n s D i c t i o n a r y .  
 	 / / /   < / s u m m a r y >  
 	 p u b l i c   c l a s s   O p t i o n s C o n t r o l l e r  
 	 {  
 	 	 p u b l i c   O p t i o n s C o n t r o l l e r ( A b s t r a c t E n t i t y   e n t i t y ,   P r i n t i n g O p t i o n D i c t i o n a r y   o p t i o n s K e y s ,   P r i n t i n g O p t i o n D i c t i o n a r y   o p t i o n s V a l u e s   =   n u l l ,   b o o l   t h r e e S t a t e   =   f a l s e )  
 	 	 {  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( o p t i o n s K e y s   ! =   n u l l ) ;  
  
 	 	 	 i f   ( e n t i t y   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 t h i s . d o c u m e n t T y p e   =   D o c u m e n t T y p e . N o n e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d o c u m e n t T y p e   =   D o c u m e n t T y p e . N o n e ;     / /   T O D O :   à   t r o u v e r   d ' a p r è s   A b s t r a c t E n t i t y   ! ! !  
 	 	 	 }  
  
 	 	 	 t h i s . o p t i o n s K e y s   =   o p t i o n s K e y s ;  
  
 	 	 	 i f   ( o p t i o n s V a l u e s   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 t h i s . o p t i o n s V a l u e s   =   o p t i o n s K e y s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . o p t i o n s V a l u e s   =   o p t i o n s V a l u e s ;  
 	 	 	 }  
  
 	 	 	 t h i s . t h r e e S t a t e   =   t h r e e S t a t e ;  
  
 	 	 	 t h i s . a l l O p t i o n s       =   V e r b o s e D o c u m e n t O p t i o n . G e t A l l   ( ) . W h e r e   ( x   = >   ! x . I s T i t l e ) . T o L i s t   ( ) ;  
 	 	 	 t h i s . t i t l e O p t i o n s   =   V e r b o s e D o c u m e n t O p t i o n . G e t A l l   ( ) . W h e r e   ( x   = >   x . I s T i t l e ) . T o L i s t   ( ) ;  
  
 	 	 	 t h i s . e d i t a b l e W i d g e t s   =   n e w   L i s t < W i d g e t >   ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   C r e a t e U I ( W i d g e t   p a r e n t ,   S y s t e m . A c t i o n   o n C h a n g e d )  
 	 	 {  
 	 	 	 t h i s . o n C h a n g e d   =   o n C h a n g e d ;  
  
 	 	 	 p a r e n t . C h i l d r e n . C l e a r   ( ) ;  
 	 	 	 t h i s . e d i t a b l e W i d g e t s . C l e a r   ( ) ;  
  
 	 	 	 s t r i n g   l a s t G r o u p   =   n u l l ;  
 	 	 	 i n t   t a b I n d e x   =   0 ;  
 	 	 	 b o o l   f i r s t W i d g e t   =   t r u e ;  
 	 	 	 F r a m e B o x   b o x   =   n u l l ;  
 	 	 	 s t r i n g   c u r r e n t T y p e   =   n u l l ;  
 	 	 	 s t r i n g   l a s t T y p e         =   n u l l ;  
 	 	 	 s t r i n g   c u r r e n t S e p a r a t o r G r o u p   =   n u l l ;  
 	 	 	 s t r i n g   l a s t S e p a r a t o r G r o u p         =   n u l l ;  
  
 	 	 	 f o r e a c h   ( v a r   v e r b o s e O p t i o n   i n   t h i s . a l l O p t i o n s )  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . o p t i o n s K e y s . C o n t a i n s O p t i o n   ( v e r b o s e O p t i o n . O p t i o n ) )  
 	 	 	 	 {  
 	 	 	 	 	 l a s t T y p e   =   c u r r e n t T y p e ;  
 	 	 	 	 	 c u r r e n t T y p e   =   v e r b o s e O p t i o n . S i m i l i T y p e ;  
  
 	 	 	 	 	 l a s t S e p a r a t o r G r o u p   =   c u r r e n t S e p a r a t o r G r o u p ;  
 	 	 	 	 	 c u r r e n t S e p a r a t o r G r o u p   =   v e r b o s e O p t i o n . G r o u p ;  
  
 	 	 	 	 	 s t r i n g   g r o u p ;  
  
 	 	 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y   ( v e r b o s e O p t i o n . G r o u p ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 g r o u p   =   n u l l ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 i n t   i   =   v e r b o s e O p t i o n . G r o u p . I n d e x O f   ( ' . ' ) ;  
 	 	 	 	 	 	 i f   ( i   = =   - 1 )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 g r o u p   =   v e r b o s e O p t i o n . G r o u p ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 g r o u p   =   v e r b o s e O p t i o n . G r o u p . S u b s t r i n g   ( 0 ,   i ) ;     / /   t r a n s f o r m e   " P a p e r . 1 "   e n   " P a p e r "  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
  
 	 	 	 	 	 / / 	 R e g a r d e   s ' i l   f a u t   m e t t r e   l e   t i t r e   d u   g r o u p e .  
 	 	 	 	 	 b o o l   t i t l e G e n e r a t e d   =   f a l s e ;  
  
 	 	 	 	 	 i f   ( f i r s t W i d g e t   | |   l a s t G r o u p   ! =   g r o u p )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 l a s t G r o u p   =   g r o u p ;  
 	 	 	 	 	 	 f i r s t W i d g e t   =   f a l s e ;  
 	 	 	 	 	 	 l a s t T y p e   =   " u n d e f i n e d " ;  
  
 	 	 	 	 	 	 b o x   =   n e w   F r a m e B o x  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   - 1 ) ,  
 	 	 	 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 1 0 ) ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( v e r b o s e O p t i o n . G r o u p ) )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 v a r   t   =   t h i s . t i t l e O p t i o n s . W h e r e   ( x   = >   x . G r o u p   = =   l a s t G r o u p ) . F i r s t O r D e f a u l t   ( ) ;  
  
 	 	 	 	 	 	 	 i f   ( t   ! =   n u l l )  
 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 v a r   t i t l e   =   n e w   S t a t i c T e x t  
 	 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 	 	 	 	 T e x t   =   s t r i n g . C o n c a t   ( t . T i t l e ,   "   : " ) ,  
 	 	 	 	 	 	 	 	 	 T e x t B r e a k M o d e   =   T e x t B r e a k M o d e . E l l i p s i s   |   T e x t B r e a k M o d e . S p l i t   |   T e x t B r e a k M o d e . S i n g l e L i n e ,  
 	 	 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   4 ) ,  
 	 	 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 	 	 t i t l e G e n e r a t e d   =   t r u e ;  
 	 	 	 	 	 	 	 }  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
  
 	 	 	 	 	 / / 	 R e g a r d e   s ' i l   f a u t   m e t t r e   u n   e s p a c e   d e   s é p a r a t i o n .  
 	 	 	 	 	 i f   ( l a s t T y p e   ! =   c u r r e n t T y p e   | |   l a s t T y p e   = =   " e n u m "   | |   ( ! t i t l e G e n e r a t e d   & &   l a s t S e p a r a t o r G r o u p   ! =   c u r r e n t S e p a r a t o r G r o u p ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 n e w   F r a m e B o x  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 	 	 P r e f e r r e d H e i g h t   =   6 ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 	 } ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 / / 	 R e g a r d e   s ' i l   f a u t   m e t t r e   u n   b o u t o n   à   c o c h e r .  
 	 	 	 	 	 i f   ( v e r b o s e O p t i o n . I s B o o l e a n )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 v a r   b u t t o n   =   n e w   C h e c k B u t t o n  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 	 	 N a m e   =   D o c u m e n t O p t i o n C o n v e r t e r . T o S t r i n g   ( v e r b o s e O p t i o n . O p t i o n ) ,  
 	 	 	 	 	 	 	 F o r m a t t e d T e x t   =   v e r b o s e O p t i o n . S h o r t D e s c r i p t i o n ,  
 	 	 	 	 	 	 	 A c c e p t T h r e e S t a t e   =   t h i s . t h r e e S t a t e ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 	 	 A u t o T o g g l e   =   f a l s e ,  
 	 	 	 	 	 	 	 T a b I n d e x   =   + + t a b I n d e x ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 v e r b o s e O p t i o n . S e t T o o l t i p   ( b u t t o n ) ;  
  
 	 	 	 	 	 	 b u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 v a r   o p   =   D o c u m e n t O p t i o n C o n v e r t e r . P a r s e   ( b u t t o n . N a m e ) ;  
 	 	 	 	 	 	 	 t h i s . C h e c k B u t t o n A c t i o n   ( o p ) ;  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 t h i s . e d i t a b l e W i d g e t s . A d d   ( b u t t o n ) ;  
 	 	 	 	 	 	 f i r s t W i d g e t   =   f a l s e ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 / / 	 R e g a r d e   s ' i l   f a u t   m e t t r e   l e s   l i s t e   d e   b o u t o n s   r a d i o .  
 	 	 	 	 	 e l s e   i f   ( v e r b o s e O p t i o n . T y p e   = =   D o c u m e n t O p t i o n V a l u e T y p e . E n u m e r a t i o n )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 f o r   ( i n t   e   =   - 1 ;   e   <   v e r b o s e O p t i o n . E n u m e r a t i o n . C o u n t   ( ) ;   e + + )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 s t r i n g   e n u m V a l u e ,   d e s c r i p t i o n ;  
  
 	 	 	 	 	 	 	 i f   ( e   = =   - 1 )  
 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 i f   ( t h i s . t h r e e S t a t e )  
 	 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 	 e n u m V a l u e       =   " _ u n u s e d _ " ;  
 	 	 	 	 	 	 	 	 	 d e s c r i p t i o n   =   " P a s   i m p o s é " ;  
 	 	 	 	 	 	 	 	 }  
 	 	 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 	 c o n t i n u e ;  
 	 	 	 	 	 	 	 	 }  
 	 	 	 	 	 	 	 }  
 	 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 e n u m V a l u e       =   v e r b o s e O p t i o n . E n u m e r a t i o n . E l e m e n t A t   ( e ) ;  
 	 	 	 	 	 	 	 	 d e s c r i p t i o n   =   v e r b o s e O p t i o n . E n u m e r a t i o n D e s c r i p t i o n . E l e m e n t A t   ( e ) ;  
 	 	 	 	 	 	 	 }  
  
 	 	 	 	 	 	 	 v a r   b u t t o n   =   n e w   R a d i o B u t t o n  
 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 	 	 	 N a m e   =   s t r i n g . C o n c a t   ( D o c u m e n t O p t i o n C o n v e r t e r . T o S t r i n g   ( v e r b o s e O p t i o n . O p t i o n ) ,   " . " ,   e n u m V a l u e ) ,  
 	 	 	 	 	 	 	 	 G r o u p   =   v e r b o s e O p t i o n . G r o u p ,  
 	 	 	 	 	 	 	 	 T e x t   =   d e s c r i p t i o n ,  
 	 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 	 	 	 A u t o T o g g l e   =   f a l s e ,  
 	 	 	 	 	 	 	 	 T a b I n d e x   =   + + t a b I n d e x ,  
 	 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 	 v e r b o s e O p t i o n . S e t T o o l t i p   ( b u t t o n ) ;  
  
 	 	 	 	 	 	 	 b u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 v a r   p   =   b u t t o n . N a m e . S p l i t   ( ' . ' ) ;  
 	 	 	 	 	 	 	 	 v a r   o p   =   D o c u m e n t O p t i o n C o n v e r t e r . P a r s e   ( p [ 0 ] ) ;  
 	 	 	 	 	 	 	 	 t h i s . R a d i o B u t t o n A c t i o n   ( o p ,   p [ 1 ] ) ;  
 	 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 	 t h i s . e d i t a b l e W i d g e t s . A d d   ( b u t t o n ) ;  
 	 	 	 	 	 	 	 f i r s t W i d g e t   =   f a l s e ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
  
 	 	 	 	 	 / / 	 R e g a r d e   s ' i l   f a u t   m e t t r e   u n   t e x t e   é d i t a b l e   p o u r   u n e   v a l e u r   n u m é r i q u e .  
 	 	 	 	 	 e l s e   i f   ( v e r b o s e O p t i o n . T y p e   = =   D o c u m e n t O p t i o n V a l u e T y p e . D i s t a n c e   | |  
 	 	 	 	 	 	 	   v e r b o s e O p t i o n . T y p e   = =   D o c u m e n t O p t i o n V a l u e T y p e . S i z e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 v a r   f r a m e   =   n e w   F r a m e B o x  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   - 1 ,   0 ) ,  
 	 	 	 	 	 	 	 T a b I n d e x   =   + + t a b I n d e x ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 v a r   l a b e l   =   n e w   S t a t i c T e x t  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   f r a m e ,  
 	 	 	 	 	 	 	 T e x t   =   v e r b o s e O p t i o n . S h o r t D e s c r i p t i o n ,  
 	 	 	 	 	 	 	 T e x t B r e a k M o d e   =   T e x t B r e a k M o d e . E l l i p s i s   |   T e x t B r e a k M o d e . S p l i t   |   T e x t B r e a k M o d e . S i n g l e L i n e ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 v e r b o s e O p t i o n . S e t T o o l t i p   ( l a b e l ) ;  
  
 	 	 	 	 	 	 v a r   u n i t   =   n e w   S t a t i c T e x t  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   f r a m e ,  
 	 	 	 	 	 	 	 T e x t   =   " m m " ,  
 	 	 	 	 	 	 	 P r e f e r r e d W i d t h   =   2 0 ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . R i g h t ,  
 	 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 5 ,   0 ,   0 ,   0 ) ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 v a r   f i e l d   =   n e w   T e x t F i e l d E x  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   f r a m e ,  
 	 	 	 	 	 	 	 N a m e   =   D o c u m e n t O p t i o n C o n v e r t e r . T o S t r i n g   ( v e r b o s e O p t i o n . O p t i o n ) ,  
 	 	 	 	 	 	 	 P r e f e r r e d W i d t h   =   7 0 ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . R i g h t ,  
 	 	 	 	 	 	 	 D e f o c u s A c t i o n   =   D e f o c u s A c t i o n . A u t o A c c e p t O r R e j e c t E d i t i o n ,  
 	 	 	 	 	 	 	 S w a l l o w E s c a p e O n R e j e c t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 	 	 S w a l l o w R e t u r n O n A c c e p t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 	 	 T a b I n d e x   =   + + t a b I n d e x ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 v e r b o s e O p t i o n . S e t T o o l t i p   ( f i e l d ) ;  
  
 	 	 	 	 	 	 f i e l d . E d i t i o n A c c e p t e d   + =   d e l e g a t e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 v a r   o p   =   D o c u m e n t O p t i o n C o n v e r t e r . P a r s e   ( f i e l d . N a m e ) ;  
 	 	 	 	 	 	 	 t h i s . E d i t i o n N u m e r i c A c c e p t e d   ( o p ,   f i e l d . T e x t ) ;  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 t h i s . e d i t a b l e W i d g e t s . A d d   ( f i e l d ) ;  
 	 	 	 	 	 	 f i r s t W i d g e t   =   f a l s e ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 / / 	 R e g a r d e   s ' i l   f a u t   m e t t r e   u n   t e x t e   é d i t a b l e   d ' u n e   s e u l e   l i g n e .  
 	 	 	 	 	 e l s e   i f   ( v e r b o s e O p t i o n . T y p e   = =   D o c u m e n t O p t i o n V a l u e T y p e . T e x t )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 v a r   f r a m e   =   n e w   F r a m e B o x  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   - 1 ,   0 ) ,  
 	 	 	 	 	 	 	 T a b I n d e x   =   + + t a b I n d e x ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 v a r   l a b e l   =   n e w   S t a t i c T e x t  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   f r a m e ,  
 	 	 	 	 	 	 	 T e x t   =   v e r b o s e O p t i o n . S h o r t D e s c r i p t i o n ,  
 	 	 	 	 	 	 	 T e x t B r e a k M o d e   =   T e x t B r e a k M o d e . E l l i p s i s   |   T e x t B r e a k M o d e . S p l i t   |   T e x t B r e a k M o d e . S i n g l e L i n e ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 v e r b o s e O p t i o n . S e t T o o l t i p   ( l a b e l ) ;  
  
 	 	 	 	 	 	 v a r   f i e l d   =   n e w   T e x t F i e l d E x  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   f r a m e ,  
 	 	 	 	 	 	 	 N a m e   =   D o c u m e n t O p t i o n C o n v e r t e r . T o S t r i n g   ( v e r b o s e O p t i o n . O p t i o n ) ,  
 	 	 	 	 	 	 	 P r e f e r r e d W i d t h   =   9 0 ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . R i g h t ,  
 	 	 	 	 	 	 	 D e f o c u s A c t i o n   =   D e f o c u s A c t i o n . A u t o A c c e p t O r R e j e c t E d i t i o n ,  
 	 	 	 	 	 	 	 S w a l l o w E s c a p e O n R e j e c t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 	 	 S w a l l o w R e t u r n O n A c c e p t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 	 	 T a b I n d e x   =   + + t a b I n d e x ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 v e r b o s e O p t i o n . S e t T o o l t i p   ( f i e l d ) ;  
  
 	 	 	 	 	 	 f i e l d . E d i t i o n A c c e p t e d   + =   d e l e g a t e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 v a r   o p   =   D o c u m e n t O p t i o n C o n v e r t e r . P a r s e   ( f i e l d . N a m e ) ;  
 	 	 	 	 	 	 	 t h i s . E d i t i o n T e x t A c c e p t e d   ( o p ,   f i e l d . T e x t ) ;  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 t h i s . e d i t a b l e W i d g e t s . A d d   ( f i e l d ) ;  
 	 	 	 	 	 	 f i r s t W i d g e t   =   f a l s e ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 / / 	 R e g a r d e   s ' i l   f a u t   m e t t r e   u n   t e x t e   é d i t a b l e   m u l t i l i g n e s .  
 	 	 	 	 	 e l s e   i f   ( v e r b o s e O p t i o n . T y p e   = =   D o c u m e n t O p t i o n V a l u e T y p e . T e x t M u l t i l i n e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 v a r   l a b e l   =   n e w   S t a t i c T e x t  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 	 	 T e x t   =   v e r b o s e O p t i o n . S h o r t D e s c r i p t i o n   +   "   : " ,  
 	 	 	 	 	 	 	 T e x t B r e a k M o d e   =   T e x t B r e a k M o d e . E l l i p s i s   |   T e x t B r e a k M o d e . S p l i t   |   T e x t B r e a k M o d e . S i n g l e L i n e ,  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 v e r b o s e O p t i o n . S e t T o o l t i p   ( l a b e l ) ;  
  
 	 	 	 	 	 	 v a r   f i e l d   =   n e w   T e x t F i e l d M u l t i E x  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 P a r e n t   =   b o x ,  
 	 	 	 	 	 	 	 N a m e   =   D o c u m e n t O p t i o n C o n v e r t e r . T o S t r i n g   ( v e r b o s e O p t i o n . O p t i o n ) ,  
 	 	 	 	 	 	 	 P r e f e r r e d H e i g h t   =   7 + 1 5 * 3 ,     / /   p l a c e   p o u r   3   l i g n e s  
 	 	 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 	 	 D e f o c u s A c t i o n   =   D e f o c u s A c t i o n . A u t o A c c e p t O r R e j e c t E d i t i o n ,  
 	 	 	 	 	 	 	 S w a l l o w E s c a p e O n R e j e c t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 	 	 S w a l l o w R e t u r n O n A c c e p t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 	 	 T a b I n d e x   =   + + t a b I n d e x ,  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 v e r b o s e O p t i o n . S e t T o o l t i p   ( f i e l d ) ;  
  
 	 	 	 	 	 	 f i e l d . E d i t i o n A c c e p t e d   + =   d e l e g a t e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 v a r   o p   =   D o c u m e n t O p t i o n C o n v e r t e r . P a r s e   ( f i e l d . N a m e ) ;  
 	 	 	 	 	 	 	 t h i s . E d i t i o n T e x t A c c e p t e d   ( o p ,   f i e l d . T e x t ) ;  
 	 	 	 	 	 	 } ;  
  
 	 	 	 	 	 	 t h i s . e d i t a b l e W i d g e t s . A d d   ( f i e l d ) ;  
 	 	 	 	 	 	 f i r s t W i d g e t   =   f a l s e ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 v a r   d e f a u l t B u t t o n   =   n e w   B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 T e x t   =   t h i s . t h r e e S t a t e   ?   " N ' i m p o s e   a u c u n e   o p t i o n "   :   " V a l e u r s   p a r   d é f a u t " ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 1 0 ,   1 0 ,   1 0 ,   1 ) ,  
 	 	 	 } ;  
  
 	 	 	 d e f a u l t B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . D e f a u l t V a l u e s   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . U p d a t e E d i t a b l e s W i d g e t s V a l u e s   ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   R a d i o B u t t o n A c t i o n ( D o c u m e n t O p t i o n   o p t i o n ,   s t r i n g   v a l u e )  
 	 	 {  
 	 	 	 i f   ( t h i s . t h r e e S t a t e   & &   v a l u e   = =   " _ u n u s e d _ " )  
 	 	 	 {  
 	 	 	 	 t h i s . o p t i o n s V a l u e s [ o p t i o n ]   =   n u l l ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . o p t i o n s V a l u e s [ o p t i o n ]   =   v a l u e ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e E d i t a b l e s W i d g e t s V a l u e s   ( ) ;  
 	 	 	 t h i s . S e t D i r t y   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C h e c k B u t t o n A c t i o n ( D o c u m e n t O p t i o n   o p t i o n )  
 	 	 {  
 	 	 	 i f   ( t h i s . t h r e e S t a t e )  
 	 	 	 {  
 	 	 	 	 / / 	 C y c l e   " m a y b e "   - >   " y e s "   - >   " n o "  
 	 	 	 	 i f   ( t h i s . o p t i o n s V a l u e s . C o n t a i n s O p t i o n   ( o p t i o n ) )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t h i s . o p t i o n s V a l u e s [ o p t i o n ]   = =   " f a l s e " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . o p t i o n s V a l u e s [ o p t i o n ]   =   n u l l ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . o p t i o n s V a l u e s [ o p t i o n ]   =   " f a l s e " ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . o p t i o n s V a l u e s [ o p t i o n ]   =   " t r u e " ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 / / 	 C y c l e   " y e s "   - >   " n o "  
 	 	 	 	 i f   ( t h i s . o p t i o n s V a l u e s [ o p t i o n ]   = =   " f a l s e " )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . o p t i o n s V a l u e s [ o p t i o n ]   =   " t r u e " ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . o p t i o n s V a l u e s [ o p t i o n ]   =   " f a l s e " ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e E d i t a b l e s W i d g e t s V a l u e s   ( ) ;  
 	 	 	 t h i s . S e t D i r t y   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   E d i t i o n N u m e r i c A c c e p t e d ( D o c u m e n t O p t i o n   o p t i o n ,   s t r i n g   v a l u e )  
 	 	 {  
 	 	 	 i f   ( t h i s . t h r e e S t a t e   & &   s t r i n g . I s N u l l O r E m p t y   ( v a l u e ) )  
 	 	 	 {  
 	 	 	 	 t h i s . o p t i o n s V a l u e s [ o p t i o n ]   =   n u l l ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 d o u b l e   d ;  
 	 	 	 	 i f   ( d o u b l e . T r y P a r s e   ( v a l u e ,   o u t   d ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . o p t i o n s V a l u e s [ o p t i o n ]   =   v a l u e ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e E d i t a b l e s W i d g e t s V a l u e s   ( ) ;  
 	 	 	 t h i s . S e t D i r t y   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   E d i t i o n T e x t A c c e p t e d ( D o c u m e n t O p t i o n   o p t i o n ,   s t r i n g   v a l u e )  
 	 	 {  
 	 	 	 t h i s . o p t i o n s V a l u e s [ o p t i o n ]   =   v a l u e ;  
  
 	 	 	 t h i s . U p d a t e E d i t a b l e s W i d g e t s V a l u e s   ( ) ;  
 	 	 	 t h i s . S e t D i r t y   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   D e f a u l t V a l u e s ( )  
 	 	 {  
 	 	 	 i f   ( t h i s . t h r e e S t a t e )  
 	 	 	 {  
 	 	 	 	 t h i s . o p t i o n s V a l u e s . C l e a r   ( ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 f o r e a c h   ( v a r   k e y   i n   t h i s . o p t i o n s K e y s . O p t i o n s . T o A r r a y   ( ) )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   v a l u e   =   t h i s . a l l O p t i o n s . W h e r e   ( x   = >   x . O p t i o n   = =   k e y ) . S e l e c t   ( x   = >   x . D e f a u l t V a l u e ) . F i r s t O r D e f a u l t   ( ) ;  
 	 	 	 	 	 t h i s . o p t i o n s V a l u e s [ k e y ]   =   v a l u e ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e E d i t a b l e s W i d g e t s V a l u e s   ( ) ;  
 	 	 	 t h i s . S e t D i r t y   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e E d i t a b l e s W i d g e t s V a l u e s ( )  
 	 	 {  
 	 	 	 f o r e a c h   ( v a r   w i d g e t   i n   t h i s . e d i t a b l e W i d g e t s )  
 	 	 	 {  
 	 	 	 	 i f   ( w i d g e t   i s   C h e c k B u t t o n )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   o p t i o n   =   D o c u m e n t O p t i o n C o n v e r t e r . P a r s e   ( w i d g e t . N a m e ) ;  
  
 	 	 	 	 	 i f   ( t h i s . o p t i o n s V a l u e s . C o n t a i n s O p t i o n   ( o p t i o n ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 w i d g e t . A c t i v e S t a t e   =   ( t h i s . o p t i o n s V a l u e s [ o p t i o n ]   = =   " t r u e " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 w i d g e t . A c t i v e S t a t e   =   A c t i v e S t a t e . M a y b e ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( w i d g e t   i s   R a d i o B u t t o n )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   n   =   w i d g e t . N a m e . S p l i t   ( ' . ' ) ;  
 	 	 	 	 	 v a r   o p t i o n   =   D o c u m e n t O p t i o n C o n v e r t e r . P a r s e   ( n [ 0 ] ) ;  
 	 	 	 	 	 v a r   v a l u e   =   n [ 1 ] ;  
  
 	 	 	 	 	 i f   ( v a l u e   = =   " _ u n u s e d _ " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 w i d g e t . A c t i v e S t a t e   =   ( ! t h i s . o p t i o n s V a l u e s . C o n t a i n s O p t i o n   ( o p t i o n ) )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 w i d g e t . A c t i v e S t a t e   =   ( t h i s . o p t i o n s V a l u e s [ o p t i o n ]   = =   v a l u e )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( w i d g e t   i s   A b s t r a c t T e x t F i e l d )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   o p t i o n   =   D o c u m e n t O p t i o n C o n v e r t e r . P a r s e   ( w i d g e t . N a m e ) ;  
 	 	 	 	 	 w i d g e t . T e x t   =   t h i s . o p t i o n s V a l u e s [ o p t i o n ] ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   S e t D i r t y ( )  
 	 	 {  
 	 	 	 i f   ( t h i s . o n C h a n g e d   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 t h i s . o n C h a n g e d   ( ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   r e a d o n l y   D o c u m e n t T y p e 	 	 	 	 	 	 d o c u m e n t T y p e ;  
 	 	 p r i v a t e   r e a d o n l y   P r i n t i n g O p t i o n D i c t i o n a r y 	 	 	 o p t i o n s K e y s ;  
 	 	 p r i v a t e   r e a d o n l y   P r i n t i n g O p t i o n D i c t i o n a r y 	 	 	 o p t i o n s V a l u e s ;  
 	 	 p r i v a t e   r e a d o n l y   b o o l 	 	 	 	 	 	 	 	 t h r e e S t a t e ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < V e r b o s e D o c u m e n t O p t i o n > 	 	 a l l O p t i o n s ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < V e r b o s e D o c u m e n t O p t i o n > 	 	 t i t l e O p t i o n s ;  
 	 	 p r i v a t e   r e a d o n l y   L i s t < W i d g e t > 	 	 	 	 	 	 e d i t a b l e W i d g e t s ;  
  
 	 	 p r i v a t e   S y s t e m . A c t i o n 	 	 	 	 	 	 	 	 o n C h a n g e d ;  
 	 }  
 }  
 