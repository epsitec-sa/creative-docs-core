ÿþu s i n g   E p s i t e c . C o m m o n . D o c u m e n t ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
 u s i n g   E p s i t e c . C o m m o n . I O ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . I O ;  
 u s i n g   S y s t e m . R u n t i m e . S e r i a l i z a t i o n ;  
 u s i n g   S y s t e m . R u n t i m e . S e r i a l i z a t i o n . F o r m a t t e r s . S o a p ;  
  
 n a m e s p a c e   E p s i t e c . C o m m o n . D o c u m e n t E d i t o r  
 {  
 	 u s i n g   D r a w i n g                 =   C o m m o n . D r a w i n g ;  
 	 u s i n g   W i d g e t s                 =   C o m m o n . W i d g e t s ;  
 	 u s i n g   D o c W i d g e t s           =   C o m m o n . D o c u m e n t . W i d g e t s ;  
 	 u s i n g   R i b b o n s                 =   C o m m o n . D o c u m e n t . R i b b o n s ;  
 	 u s i n g   C o n t a i n e r s           =   C o m m o n . D o c u m e n t . C o n t a i n e r s ;  
 	 u s i n g   O b j e c t s                 =   C o m m o n . D o c u m e n t . O b j e c t s ;  
 	 u s i n g   S e t t i n g s               =   C o m m o n . D o c u m e n t . S e t t i n g s ;  
 	 u s i n g   G l o b a l S e t t i n g s   =   C o m m o n . D o c u m e n t . S e t t i n g s . G l o b a l S e t t i n g s ;  
 	 u s i n g   M e n u s                     =   C o m m o n . D o c u m e n t . M e n u s ;  
 	 u s i n g   D o c u m e n t               =   C o m m o n . D o c u m e n t . D o c u m e n t ;  
  
 	 / / /   < s u m m a r y >  
 	 / / /   L a   c l a s s e   D o c u m e n t E d i t o r   r e p r é s e n t e   l ' é d i t e u r   d e   d o c u m e n t   c o m p l e t .  
 	 / / /   < / s u m m a r y >  
 	 p u b l i c   c l a s s   D o c u m e n t E d i t o r   :   W i d g e t  
 	 {  
 	 	 p u b l i c   D o c u m e n t E d i t o r ( D o c u m e n t T y p e   t y p e )  
 	 	 	 :   t h i s   ( t y p e ,   n e w   C o m m a n d D i s p a t c h e r   ( " C o m m o n . D o c u m e n t E d i t o r " ,   C o m m a n d D i s p a t c h e r L e v e l . P r i m a r y ) ,   n e w   C o m m a n d C o n t e x t   ( ) ,   n u l l )  
 	 	 {  
 	 	 }  
  
 	 	 p u b l i c   D o c u m e n t E d i t o r ( D o c u m e n t T y p e   t y p e ,   C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d C o n t e x t   c o n t e x t ,   W i n d o w   w i n d o w )  
 	 	 {  
 	 	 	 t h i s . c o m m a n d D i s p a t c h e r   =   d i s p a t c h e r ;  
 	 	 	 t h i s . c o m m a n d C o n t e x t         =   c o n t e x t ;  
 	 	 	  
 	 	 	 t h i s . c o m m a n d D i s p a t c h e r . R e g i s t e r C o n t r o l l e r   ( t h i s ) ;  
 	 	 	  
 	 	 	 C o m m a n d D i s p a t c h e r . S e t D i s p a t c h e r ( t h i s ,   t h i s . c o m m a n d D i s p a t c h e r ) ;  
 	 	 	  
 	 	 	 t h i s . d o c u m e n t T y p e   =   t y p e ;  
 	 	 	 t h i s . u s e A r r a y   =   f a l s e ;  
  
 	 	 	 i f   (   t h i s . d o c u m e n t T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m   )  
 	 	 	 {  
 	 	 	 	 t h i s . i n s t a l l T y p e   =   I n s t a l l T y p e . F u l l ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 i f   (   A p p l i c a t i o n . M o d e   = =   " F "   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . a s k K e y   =   f a l s e ;  
 	 	 	 	 	 t h i s . i n s t a l l T y p e   =   I n s t a l l T y p e . F r e e w a r e ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 # i f   D E B U G  
 	 	 	 	 	 t h i s . i n s t a l l T y p e   =   I n s t a l l T y p e . F u l l ;  
 # e l s e  
 	 	 	 	 	 s t r i n g   k e y   =   C o m m o n . S u p p o r t . S e r i a l A l g o r i t h m . R e a d C r D o c S e r i a l ( ) ;  
 	 	 	 	 	 t h i s . a s k K e y   =   ( k e y   = =   n u l l ) ;  
  
 	 	 	 	 	 i f   (   s t r i n g . I s N u l l O r E m p t y   ( k e y )   | |   k e y   = =   " d e m o "   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . i n s t a l l T y p e   =   I n s t a l l T y p e . D e m o ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   (   C o m m o n . S u p p o r t . S e r i a l A l g o r i t h m . C h e c k S e r i a l ( k e y ,   4 0 )   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . i n s t a l l T y p e   =   I n s t a l l T y p e . F u l l ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . i n s t a l l T y p e   =   I n s t a l l T y p e . E x p i r e d ;  
 	 	 	 	 	 }  
 # e n d i f  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 i f   ( E p s i t e c . C o m m o n . S u p p o r t . G l o b a l s . I s D e b u g B u i l d )  
 	 	 	 {  
 	 	 	 	 t h i s . d e b u g M o d e   =   D e b u g M o d e . D e b u g C o m m a n d s ;  
 	 	 	 	 / / ? t h i s . d e b u g M o d e   =   t h i s . d o c u m e n t T y p e   = =   C o m m o n . D o c u m e n t . D o c u m e n t T y p e . P i c t o g r a m   ?   D e b u g M o d e . D e b u g C o m m a n d s   :   D e b u g M o d e . R e l e a s e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d e b u g M o d e   =   D e b u g M o d e . R e l e a s e ;  
 	 	 	 }  
  
 	 	 	 i f   (   ! t h i s . R e a d G l o b a l S e t t i n g s ( )   )  
 	 	 	 {  
 	 	 	 	 t h i s . g l o b a l S e t t i n g s   =   n e w   G l o b a l S e t t i n g s ( ) ;  
 	 	 	 	 t h i s . g l o b a l S e t t i n g s . I n i t i a l i z e ( t h i s . d o c u m e n t T y p e ) ;  
 	 	 	 }  
  
 	 	 	 E p s i t e c . C o m m o n . W i d g e t s . A d o r n e r s . F a c t o r y . S e t A c t i v e ( t h i s . g l o b a l S e t t i n g s . A d o r n e r ) ;  
  
 	 	 	 t h i s . d l g S p l a s h   =   n e w   D i a l o g s . S p l a s h ( t h i s ) ;  
 	 	 	  
 	 	 	 i f   (   t h i s . d o c u m e n t T y p e   ! =   D o c u m e n t T y p e . P i c t o g r a m   & &  
 	 	 	 	   t h i s . g l o b a l S e t t i n g s . S p l a s h S c r e e n   )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g S p l a s h . S h o w ( ) ;  
 	 	 	 }  
  
 	 	 	 I m a g e M a n a g e r . I n i t i a l i z e D e f a u l t C a c h e ( ) ;  
  
 	 	 	 / / 	 C r é e   l e s   a s s o c i a t i o n s   i n t e r n e s   p o u r   l a   l e c t u r e   d e s   m i n i a t u r e s   d e s   f i c h i e r s  
 	 	 	 / / 	 * . c r d o c   e t   * . c r m o d   ( p o u r   l e   c a c h e   d e s   d o c u m e n t s ) .  
 	 	 	 t h i s . d e f a u l t D o c u m e n t M a n a g e r   =   n e w   D o c u m e n t M a n a g e r ( ) ;  
 	 	 	 t h i s . d e f a u l t D o c u m e n t M a n a g e r . A s s o c i a t e ( " . c r d o c " ,   D o c u m e n t . G e t D o c u m e n t I n f o ) ;  
 	 	 	 t h i s . d e f a u l t D o c u m e n t M a n a g e r . A s s o c i a t e ( " . c r m o d " ,   D o c u m e n t . G e t D o c u m e n t I n f o ) ;  
  
 	 	 	 D o c u m e n t C a c h e . C r e a t e D e f a u l t I m a g e A s s o c i a t i o n s ( t h i s . d e f a u l t D o c u m e n t M a n a g e r ) ;  
 	 	 	  
 	 	 	 t h i s . d l g A b o u t                   =   n e w   D i a l o g s . A b o u t ( t h i s ) ;  
 	 	 	 t h i s . d l g D o w n l o a d             =   n e w   D i a l o g s . D o w n l o a d ( t h i s ) ;  
 	 	 	 t h i s . d l g E x p o r t T y p e         =   n e w   D i a l o g s . E x p o r t T y p e ( t h i s ) ;  
 	 	 	 t h i s . d l g E x p o r t                 =   n e w   D i a l o g s . E x p o r t ( t h i s ) ;  
 	 	 	 t h i s . d l g E x p o r t P D F           =   n e w   D i a l o g s . E x p o r t P D F ( t h i s ) ;  
 	 	 	 t h i s . d l g E x p o r t I C O           =   n e w   D i a l o g s . E x p o r t I C O ( t h i s ) ;  
 	 	 	 t h i s . d l g G l y p h s                 =   n e w   D i a l o g s . G l y p h s ( t h i s ) ;  
 	 	 	 t h i s . d l g I n f o s                   =   n e w   D i a l o g s . I n f o s ( t h i s ) ;  
 	 	 	 t h i s . d l g F i l e E x p o r t         =   n e w   D i a l o g s . F i l e E x p o r t ( t h i s ) ;  
 	 	 	 t h i s . d l g F i l e N e w               =   n e w   D i a l o g s . F i l e N e w ( t h i s ) ;  
 	 	 	 t h i s . d l g F i l e O p e n             =   n e w   D i a l o g s . F i l e O p e n ( t h i s ) ;  
 	 	 	 t h i s . d l g F i l e O p e n M o d e l   =   n e w   D i a l o g s . F i l e O p e n M o d e l ( t h i s ) ;  
 	 	 	 t h i s . d l g F i l e S a v e             =   n e w   D i a l o g s . F i l e S a v e ( t h i s ) ;  
 	 	 	 t h i s . d l g F i l e S a v e M o d e l   =   n e w   D i a l o g s . F i l e S a v e M o d e l ( t h i s ) ;  
 	 	 	 t h i s . d l g P a g e S t a c k           =   n e w   D i a l o g s . P a g e S t a c k ( t h i s ) ;  
 	 	 	 t h i s . d l g P r i n t                   =   n e w   D i a l o g s . P r i n t ( t h i s ) ;  
 	 	 	 t h i s . d l g R e p l a c e               =   n e w   D i a l o g s . R e p l a c e ( t h i s ) ;  
 	 	 	 t h i s . d l g S e t t i n g s             =   n e w   D i a l o g s . S e t t i n g s ( t h i s ) ;  
 	 	 	 t h i s . d l g U p d a t e                 =   n e w   D i a l o g s . U p d a t e ( t h i s ) ;  
  
 	 	 	 t h i s . d l g G l y p h s . C l o s e d         + =   t h i s . H a n d l e D l g C l o s e d ;  
 	 	 	 t h i s . d l g I n f o s . C l o s e d           + =   t h i s . H a n d l e D l g C l o s e d ;  
 	 	 	 t h i s . d l g P a g e S t a c k . C l o s e d   + =   t h i s . H a n d l e D l g C l o s e d ;  
 	 	 	 t h i s . d l g R e p l a c e . C l o s e d       + =   t h i s . H a n d l e D l g C l o s e d ;  
 	 	 	 t h i s . d l g S e t t i n g s . C l o s e d     + =   t h i s . H a n d l e D l g C l o s e d ;  
  
 	 	 	 t h i s . S t a r t C h e c k ( f a l s e ) ;  
 	 	 	 t h i s . I n i t C o m m a n d s ( ) ;  
 	 	 	 t h i s . C r e a t e L a y o u t ( ) ;  
  
 	 	 	 M i s c . G e t F o n t L i s t ( f a l s e ) ;     / /   m i s e   e n   c a c h e   d e s   p o l i c e s  
 	 	 	  
 	 	 	 t h i s . d l g S p l a s h . S t a r t T i m e r ( ) ;  
  
 	 	 	 t h i s . c l i p b o a r d   =   n e w   D o c u m e n t ( t h i s . d o c u m e n t T y p e ,   D o c u m e n t M o d e . C l i p b o a r d ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ,   t h i s . g l o b a l S e t t i n g s ,   t h i s . C o m m a n d D i s p a t c h e r ,   t h i s . C o m m a n d C o n t e x t ,   w i n d o w ) ;  
 	 	 	 t h i s . c l i p b o a r d . N a m e   =   " C l i p b o a r d " ;  
  
 	 	 	 t h i s . d o c u m e n t s   =   n e w   L i s t < D o c u m e n t I n f o >   ( ) ;  
 	 	 	 t h i s . c u r r e n t D o c u m e n t   =   - 1 ;  
  
 	 	 	 s t r i n g [ ]   a r g s   =   S y s t e m . E n v i r o n m e n t . G e t C o m m a n d L i n e A r g s ( ) ;  
 	 	 	 i f   (   a r g s . L e n g t h   > =   2   )  
 	 	 	 {  
 	 	 	 	 t h i s . C r e a t e D o c u m e n t ( w i n d o w ) ;  
 	 	 	 	 s t r i n g   f i l e n a m e   =   a r g s [ 1 ] ;  
 	 	 	 	 s t r i n g   e r r   =   " " ;  
 	 	 	 	 i f   (   M i s c . I s E x t e n s i o n ( f i l e n a m e ,   " . c r c o l o r s " )   )  
 	 	 	 	 {  
 	 	 	 	 	 e r r   =   t h i s . P a l e t t e R e a d ( f i l e n a m e ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 e r r   =   t h i s . C u r r e n t D o c u m e n t . R e a d ( f i l e n a m e ) ;  
 	 	 	 	 	 t h i s . U p d a t e A f t e r R e a d ( ) ;  
 	 	 	 	 }  
 	 	 	 	 t h i s . U p d a t e R u l e r s ( ) ;  
 	 	 	 	 i f   (   e r r   = =   " "   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . D i a l o g W a r n i n g s ( t h i s . C u r r e n t D o c u m e n t . R e a d W a r n i n g s ) ;  
 	 	 	 	 }  
 	 	 	 	 t h i s . D i a l o g E r r o r ( e r r ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 i f   (   t h i s . g l o b a l S e t t i n g s . F i r s t A c t i o n   = =   S e t t i n g s . F i r s t A c t i o n . O p e n N e w D o c u m e n t   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C r e a t e D o c u m e n t ( w i n d o w ) ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   (   t h i s . g l o b a l S e t t i n g s . F i r s t A c t i o n   = =   S e t t i n g s . F i r s t A c t i o n . O p e n L a s t M o d e l   & &  
 	 	 	 	 	   t h i s . g l o b a l S e t t i n g s . L a s t M o d e l C o u n t   >   0   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C r e a t e D o c u m e n t ( w i n d o w ) ;  
 	 	 	 	 	 s t r i n g   f i l e n a m e   =   t h i s . g l o b a l S e t t i n g s . L a s t M o d e l G e t ( 0 ) ;  
 	 	 	 	 	 s t r i n g   e r r   =   t h i s . C u r r e n t D o c u m e n t . R e a d ( f i l e n a m e ) ;  
 	 	 	 	 	 t h i s . U p d a t e A f t e r R e a d ( ) ;  
 	 	 	 	 	 t h i s . U p d a t e R u l e r s ( ) ;  
 	 	 	 	 	 i f   (   e r r   = =   " "   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . D i a l o g W a r n i n g s ( t h i s . C u r r e n t D o c u m e n t . R e a d W a r n i n g s ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 i f   (   t h i s . g l o b a l S e t t i n g s . F i r s t A c t i o n   = =   S e t t i n g s . F i r s t A c t i o n . O p e n L a s t F i l e   & &  
 	 	 	 	 	   t h i s . g l o b a l S e t t i n g s . L a s t F i l e n a m e C o u n t   >   0   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C r e a t e D o c u m e n t   ( w i n d o w ) ;  
 	 	 	 	 	 s t r i n g   f i l e n a m e   =   t h i s . g l o b a l S e t t i n g s . L a s t F i l e n a m e G e t ( 0 ) ;  
 	 	 	 	 	 s t r i n g   e r r   =   t h i s . C u r r e n t D o c u m e n t . R e a d ( f i l e n a m e ) ;  
 	 	 	 	 	 t h i s . U p d a t e A f t e r R e a d ( ) ;  
 	 	 	 	 	 t h i s . U p d a t e R u l e r s ( ) ;  
 	 	 	 	 	 i f   (   e r r   = =   " "   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . D i a l o g W a r n i n g s ( t h i s . C u r r e n t D o c u m e n t . R e a d W a r n i n g s ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 t h i s . i n i t i a l i z a t i o n I n P r o g r e s s   =   t r u e ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . I n i t i a l i z a t i o n I n P r o g r e s s   =   t r u e ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . N o t i f y A l l C h a n g e d ( ) ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . G e n e r a t e E v e n t s ( ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . U s e D o c u m e n t ( - 1 ) ;  
 	 	 	 	 t h i s . U p d a t e C l o s e C o m m a n d ( ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e S i z e C h a n g e d ( )  
 	 	 {  
 	 	 	 i f   (   t h i s . r e s i z e   = =   n u l l   | |   t h i s . W i n d o w   = =   n u l l   )     r e t u r n ;  
 	 	 	 t h i s . r e s i z e . E n a b l e   =   ! t h i s . W i n d o w . I s F u l l S c r e e n ;  
 	 	 }  
  
 	 	 p r o t e c t e d   o v e r r i d e   v o i d   S e t B o u n d s O v e r r i d e ( R e c t a n g l e   o l d R e c t ,   R e c t a n g l e   n e w R e c t )  
 	 	 {  
 	 	 	 b a s e . S e t B o u n d s O v e r r i d e ( o l d R e c t ,   n e w R e c t ) ;  
 	 	 	 t h i s . H a n d l e S i z e C h a n g e d ( ) ;  
 	 	 }  
  
  
 	 	 p r o t e c t e d   o v e r r i d e   v o i d   D i s p o s e ( b o o l   d i s p o s i n g )  
 	 	 {  
 	 	 	 b a s e . D i s p o s e ( d i s p o s i n g ) ;  
  
 	 	 	 I m a g e M a n a g e r . S h u t D o w n D e f a u l t C a c h e   ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   D o c u m e n t T y p e   D o c u m e n t T y p e  
 	 	 {  
 	 	 	 g e t   {   r e t u r n   t h i s . d o c u m e n t T y p e ;   }  
 	 	 }  
 	 	  
 	 	 p u b l i c   I n s t a l l T y p e   I n s t a l l T y p e  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . i n s t a l l T y p e ;  
 	 	 	 }  
  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 t h i s . i n s t a l l T y p e   =   v a l u e ;  
  
 	 	 	 	 i n t   t o t a l   =   t h i s . d o c u m e n t s . C o u n t ;  
 	 	 	 	 f o r   (   i n t   i = 0   ;   i < t o t a l   ;   i + +   )  
 	 	 	 	 {  
 	 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . d o c u m e n t s [ i ] ;  
 	 	 	 	 	 d i . d o c u m e n t . I n s t a l l T y p e   =   t h i s . i n s t a l l T y p e ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
 	 	  
 	 	 p u b l i c   D e b u g M o d e   D e b u g M o d e  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . d e b u g M o d e ;  
 	 	 	 }  
  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 t h i s . d e b u g M o d e   =   v a l u e ;  
  
 	 	 	 	 i n t   t o t a l   =   t h i s . d o c u m e n t s . C o u n t ;  
 	 	 	 	 f o r   (   i n t   i = 0   ;   i < t o t a l   ;   i + +   )  
 	 	 	 	 {  
 	 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . d o c u m e n t s [ i ] ;  
 	 	 	 	 	 d i . d o c u m e n t . D e b u g M o d e   =   t h i s . d e b u g M o d e ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
 	 	  
 	 	 p u b l i c   G l o b a l S e t t i n g s   G l o b a l S e t t i n g s  
 	 	 {  
 	 	 	 g e t   {   r e t u r n   t h i s . g l o b a l S e t t i n g s ;   }  
 	 	 }  
 	 	  
 	 	 p u b l i c   C o m m a n d D i s p a t c h e r   C o m m a n d D i s p a t c h e r  
 	 	 {  
 	 	 	 g e t   {   r e t u r n   t h i s . c o m m a n d D i s p a t c h e r ;   }  
 	 	 }  
  
 	 	 p u b l i c   C o m m a n d C o n t e x t   C o m m a n d C o n t e x t  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . c o m m a n d C o n t e x t ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   v o i d   M a k e R e a d y T o R u n ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l ' a p p l i c a t i o n   a   f i n i   d e   d é m a r r e r .  
 	 	 	 i f   (   t h i s . a s k K e y   )  
 	 	 	 {  
 	 	 	 	 t h i s . a s k K e y   =   f a l s e ;  
 	 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
 	 	 	 }  
  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . N o t i f y O r i g i n C h a n g e d ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . E n d C h e c k ( f a l s e ) ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   A s y n c N o t i f y ( )  
 	 	 {  
 	 	 	 i f   (   t h i s . c u r r e n t D o c u m e n t   <   0   )     r e t u r n ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . G e n e r a t e E v e n t s ( ) ;  
  
 	 	 	 i f   ( t h i s . i n i t i a l i z a t i o n I n P r o g r e s s )  
 	 	 	 {  
 	 	 	 	 / / 	 I n i t i a l i s a t i o n   e n   c o u r s .   C e t t e   p é r i o d e   d u r e   d e p u i s   l a   c r é a t i o n   d ' u n  
 	 	 	 	 / / 	 n o u v e a u   d o c u m e n t ,   j u s q u ' a u   p r e m i e r   A s y n c N o t i f y   e f f e c t u é   l o r s q u e   t o u t  
 	 	 	 	 / / 	 e x i s t e   d e   f a ç o n   c e r t a i n e .   L o r s q u e   c e   m o d e   e s t   t r u e ,   l e   d o c u m e n t   n ' e s t  
 	 	 	 	 / / 	 p a s   a f f i c h é ,   p o u r   é v i t e r   d e   v o i r   a p p a r a î t r e   b r i è v e m e n t   u n   d o c u m e n t  
 	 	 	 	 / / 	 a v e c   u n   z o o m   f a u x .   C e c i   e s t   n é c e s s a i r e   à   c a u s e   d e   Z o o m P a g e A n d C e n t e r  
 	 	 	 	 / / 	 p o u r   l e q u e l   l a   f e n ê t r e   d o i t   e x i s t e r   d a n s   s a   t a i l l e   d é f i n i t i v e   !  
 	 	 	 	 t h i s . i n i t i a l i z a t i o n I n P r o g r e s s   =   f a l s e ;  
  
 	 	 	 	 i n t   t o t a l   =   t h i s . d o c u m e n t s . C o u n t ;  
 	 	 	 	 f o r   ( i n t   i = 0 ;   i < t o t a l ;   i + + )  
 	 	 	 	 {  
 	 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . d o c u m e n t s [ i ] ;  
 	 	 	 	 	 i f   ( d i . d o c u m e n t . I n i t i a l i z a t i o n I n P r o g r e s s )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d i . d o c u m e n t . I n i t i a l i z a t i o n I n P r o g r e s s   =   f a l s e ;  
 	 	 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   d i . d o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 	 	 c o n t e x t . Z o o m P a g e A n d C e n t e r ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . H a s C u r r e n t D o c u m e n t )  
 	 	 	 	 {  
 	 	 	 	 	 / / ? D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 	 / / ? c o n t e x t . Z o o m P a g e A n d C e n t e r ( ) ;  
 	 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . F o c u s ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e L a y o u t ( )  
 	 	 {  
 	 	 	 T o o l T i p . D e f a u l t . B e h a v i o u r   =   T o o l T i p B e h a v i o u r . N o r m a l ;  
  
 	 	 	 / / 	 C r é e   l e   R i b b o n B o o k   u n i q u e   q u i   r e m p l a c e   l e   t r a d i t i o n n e l   m e n u .  
 	 	 	 t h i s . r i b b o n B o o k   =   n e w   R i b b o n B o o k ( t h i s ) ;  
 	 	 	 t h i s . r i b b o n B o o k . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . r i b b o n B o o k . A c t i v e P a g e C h a n g e d   + =   t h i s . H a n d l e R i b b o n B o o k A c t i v e P a g e C h a n g e d ;  
  
 	 	 	 / / 	 C r é e   l e s   d i f f é r e n t e s   p a g e s   d u   r u b a n .  
 	 	 	 t h i s . r i b b o n M a i n   =   n e w   R i b b o n P a g e ( ) ;  
 	 	 	 t h i s . r i b b o n M a i n . N a m e   =   " M a i n " ;  
 	 	 	 t h i s . r i b b o n M a i n . R i b b o n T i t l e   =   R e s . S t r i n g s . R i b b o n . M a i n ;  
 	 	 	 t h i s . r i b b o n B o o k . P a g e s . A d d ( t h i s . r i b b o n M a i n ) ;  
  
 	 	 	 t h i s . r i b b o n G e o m   =   n e w   R i b b o n P a g e ( ) ;  
 	 	 	 t h i s . r i b b o n G e o m . N a m e   =   " G e o m " ;  
 	 	 	 t h i s . r i b b o n G e o m . R i b b o n T i t l e   =   R e s . S t r i n g s . R i b b o n . G e o m ;  
 	 	 	 t h i s . r i b b o n B o o k . P a g e s . A d d ( t h i s . r i b b o n G e o m ) ;  
  
 	 	 	 t h i s . r i b b o n O p e r   =   n e w   R i b b o n P a g e ( ) ;  
 	 	 	 t h i s . r i b b o n O p e r . N a m e   =   " O p e r " ;  
 	 	 	 t h i s . r i b b o n O p e r . R i b b o n T i t l e   =   R e s . S t r i n g s . R i b b o n . O p e r ;  
 	 	 	 t h i s . r i b b o n B o o k . P a g e s . A d d ( t h i s . r i b b o n O p e r ) ;  
  
 	 	 	 t h i s . r i b b o n T e x t   =   n e w   R i b b o n P a g e ( ) ;  
 	 	 	 t h i s . r i b b o n T e x t . N a m e   =   " T e x t " ;  
 	 	 	 t h i s . r i b b o n T e x t . R i b b o n T i t l e   =   R e s . S t r i n g s . R i b b o n . T e x t ;  
 	 	 	 t h i s . r i b b o n B o o k . P a g e s . A d d ( t h i s . r i b b o n T e x t ) ;  
  
 	 	 	 / / 	 P e u b l e   l e s   p a g e s   d e s   r u b a n s   p a r   l e s   s e c t i o n s .  
 	 	 	 t h i s . r i b b o n M a i n . I t e m s . A d d ( n e w   R i b b o n s . F i l e ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n M a i n . I t e m s . A d d ( n e w   R i b b o n s . C l i p b o a r d ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n M a i n . I t e m s . A d d ( n e w   R i b b o n s . U n d o ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n M a i n . I t e m s . A d d ( n e w   R i b b o n s . S e l e c t ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n M a i n . I t e m s . A d d ( n e w   R i b b o n s . V i e w ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n M a i n . I t e m s . A d d ( n e w   R i b b o n s . A c t i o n ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
 	 	 	 i f   ( t h i s . d e b u g M o d e   = =   D e b u g M o d e . D e b u g C o m m a n d s )  
 	 	 	 {  
                                 t h i s . r i b b o n M a i n . I t e m s . A d d ( n e w   R i b b o n s . D e b u g ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
 	 	 	 }  
  
                         t h i s . r i b b o n G e o m . I t e m s . A d d ( n e w   R i b b o n s . M o v e ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n G e o m . I t e m s . A d d ( n e w   R i b b o n s . R o t a t e ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n G e o m . I t e m s . A d d ( n e w   R i b b o n s . S c a l e ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n G e o m . I t e m s . A d d ( n e w   R i b b o n s . A l i g n ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n G e o m . I t e m s . A d d ( n e w   R i b b o n s . B o o l ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n G e o m . I t e m s . A d d ( n e w   R i b b o n s . G e o m ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
  
                         t h i s . r i b b o n O p e r . I t e m s . A d d ( n e w   R i b b o n s . O r d e r ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n O p e r . I t e m s . A d d ( n e w   R i b b o n s . G r o u p ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n O p e r . I t e m s . A d d ( n e w   R i b b o n s . C o l o r ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
  
                         t h i s . r i b b o n T e x t . I t e m s . A d d ( n e w   R i b b o n s . T e x t S t y l e s ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n T e x t . I t e m s . A d d ( n e w   R i b b o n s . P a r a g r a p h ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n T e x t . I t e m s . A d d ( n e w   R i b b o n s . F o n t ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n T e x t . I t e m s . A d d ( n e w   R i b b o n s . C l i p b o a r d ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n T e x t . I t e m s . A d d ( n e w   R i b b o n s . U n d o ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n T e x t . I t e m s . A d d ( n e w   R i b b o n s . R e p l a c e ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
                         t h i s . r i b b o n T e x t . I t e m s . A d d ( n e w   R i b b o n s . I n s e r t ( t h i s . d o c u m e n t T y p e ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ) ) ;  
  
 	 	 	 t h i s . U p d a t e Q u i c k C o m m a n d s ( ) ;  
  
 	 	 	 / / 	 C r é e   l a   b a r r e   d e   s t a t u s .  
 	 	 	 t h i s . i n f o   =   n e w   S t a t u s B a r ( t h i s ) ;  
 	 	 	 t h i s . i n f o . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 t h i s . I n f o A d d ( C o m m o n . W i d g e t s . R e s . C o m m a n d s . D e s e l e c t A l l . C o m m a n d I d ) ;  
 	 	 	 t h i s . I n f o A d d ( C o m m o n . W i d g e t s . R e s . C o m m a n d s . S e l e c t A l l . C o m m a n d I d ) ;  
 	 	 	 t h i s . I n f o A d d ( " S e l e c t I n v e r t " ) ;  
 	 	 	 t h i s . I n f o A d d ( " H i d e S e l " ) ;  
 	 	 	 t h i s . I n f o A d d ( " H i d e R e s t " ) ;  
 	 	 	 t h i s . I n f o A d d ( " H i d e C a n c e l " ) ;  
  
 	 	 	 t h i s . I n f o A d d ( " S t a t u s O b j e c t " ,   1 2 0 ) ;  
  
 	 	 	 / / ? t h i s . I n f o A d d ( " Z o o m M i n " ) ;  
 	 	 	 i f   (   t h i s . d o c u m e n t T y p e   ! =   D o c u m e n t T y p e . P i c t o g r a m   )  
 	 	 	 {  
 	 	 	 	 t h i s . I n f o A d d ( " Z o o m P a g e " ) ;  
 	 	 	 	 t h i s . I n f o A d d ( " Z o o m P a g e W i d t h " ) ;  
 	 	 	 }  
 	 	 	 t h i s . I n f o A d d ( " Z o o m D e f a u l t " ) ;  
 	 	 	 t h i s . I n f o A d d ( " Z o o m S e l " ) ;  
 	 	 	 i f   (   t h i s . d o c u m e n t T y p e   ! =   D o c u m e n t T y p e . P i c t o g r a m   )  
 	 	 	 {  
 	 	 	 	 t h i s . I n f o A d d ( " Z o o m S e l W i d t h " ) ;  
 	 	 	 }  
 	 	 	 t h i s . I n f o A d d ( " Z o o m P r e v " ) ;  
  
 	 	 	 S t a t u s F i e l d   s f   =   t h i s . I n f o A d d ( " S t a t u s Z o o m " ,   5 5 ) ;  
 	 	 	 s f . C l i c k e d   + =   t h i s . H a n d l e S t a t u s Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( s f ,   R e s . S t r i n g s . S t a t u s . Z o o m . M e n u ) ;  
  
 	 	 	 A b s t r a c t S l i d e r   s l i d e r   =   n e w   H S l i d e r ( ) ;  
 	 	 	 s l i d e r . N a m e   =   " S t a t u s Z o o m S l i d e r " ;  
 	 	 	 s l i d e r . P r e f e r r e d W i d t h   =   1 0 0 ;  
 	 	 	 s l i d e r . M a r g i n s   =   n e w   M a r g i n s ( 1 ,   1 ,   1 ,   1 ) ;  
 	 	 	 i f   ( t h i s . d o c u m e n t T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m )  
 	 	 	 {  
 	 	 	 	 s l i d e r . M i n V a l u e   =   0 . 5 M ;  
 	 	 	 	 s l i d e r . M a x V a l u e   =   8 . 0 M ;  
 	 	 	 	 s l i d e r . S m a l l C h a n g e   =   0 . 1 M ;  
 	 	 	 	 s l i d e r . L a r g e C h a n g e   =   0 . 5 M ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 s l i d e r . M i n V a l u e   =   0 . 1 M ;  
 	 	 	 	 s l i d e r . M a x V a l u e   =   1 6 . 0 M ;  
 	 	 	 	 s l i d e r . S m a l l C h a n g e   =   0 . 1 M ;  
 	 	 	 	 s l i d e r . L a r g e C h a n g e   =   0 . 5 M ;  
 	 	 	 }  
 	 	 	 s l i d e r . R e s o l u t i o n   =   0 . 0 M ;  
 	 	 	 s l i d e r . L o g a r i t h m i c D i v i s o r   =   3 . 0 M ;  
 	 	 	 s l i d e r . V a l u e C h a n g e d   + =   t h i s . H a n d l e S l i d e r Z o o m C h a n g e d ;  
 	 	 	 t h i s . i n f o . I t e m s . A d d ( s l i d e r ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( s l i d e r ,   R e s . S t r i n g s . S t a t u s . Z o o m . S l i d e r ) ;  
  
 	 	 	 t h i s . I n f o A d d ( " S t a t u s M o u s e " ,   1 1 0 ) ;  
 	 	 	 t h i s . I n f o A d d ( " S t a t u s M o d i f " ,   3 0 0 ) ;  
  
 	 	 	 t h i s . i n f o . I t e m s [ " S t a t u s M o d i f " ] . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	  
 	 	 	 t h i s . r e s i z e   =   n e w   R e s i z e K n o b ( ) ;  
 	 	 	 t h i s . r e s i z e . M a r g i n s   =   n e w   M a r g i n s ( 2 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . i n f o . I t e m s . A d d ( t h i s . r e s i z e ) ;  
 	 	 	 t h i s . r e s i z e . D o c k   =   D o c k S t y l e . R i g h t ;     / /   d o i t   ê t r e   f a i t   a p r è s   l e   I t e m s . A d d   !  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . r e s i z e ,   R e s . S t r i n g s . D i a l o g . T o o l t i p . R e s i z e ) ;  
  
 	 	 	 / / 	 C r é e   l a   b a r r e   d ' o u t i l s   v e r t i c a l e   g a u c h e .  
 	 	 	 t h i s . v T o o l B a r   =   n e w   V T o o l B a r ( t h i s ) ;  
 	 	 	 t h i s . v T o o l B a r . D o c k   =   D o c k S t y l e . L e f t ;  
  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l S e l e c t S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l G l o b a l S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l S h a p e r S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l E d i t S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l Z o o m S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l H a n d S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l P i c k e r S t a t e . C o m m a n d ) ;  
 	 	 	 i f   (   t h i s . d o c u m e n t T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m   )  
 	 	 	 {  
 	 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l H o t S p o t S t a t e . C o m m a n d ) ;  
 	 	 	 }  
 	 	 	 t h i s . V T o o l B a r A d d ( n u l l ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l L i n e S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l R e c t a n g l e S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l C i r c l e S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l E l l i p s e S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l P o l y S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l F r e e S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l B e z i e r S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l R e g u l a r S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l S u r f a c e S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l V o l u m e S t a t e . C o m m a n d ) ;  
 	 	 	 / / t h i s . V T o o l B a r A d d ( t h i s . t o o l T e x t L i n e S t a t e . C o m m a n d ) ;  
 	 	 	 / / t h i s . V T o o l B a r A d d ( t h i s . t o o l T e x t B o x S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l T e x t L i n e 2 S t a t e . C o m m a n d ) ;  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l T e x t B o x 2 S t a t e . C o m m a n d ) ;  
 	 	 	 i f   (   t h i s . u s e A r r a y   )  
 	 	 	 {  
 	 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l A r r a y S t a t e . C o m m a n d ) ;  
 	 	 	 }  
 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l I m a g e S t a t e . C o m m a n d ) ;  
 	 	 	 i f   (   t h i s . d o c u m e n t T y p e   ! =   D o c u m e n t T y p e . P i c t o g r a m   )  
 	 	 	 {  
 	 	 	 	 t h i s . V T o o l B a r A d d ( t h i s . t o o l D i m e n s i o n S t a t e . C o m m a n d ) ;  
 	 	 	 }  
 	 	 	 t h i s . V T o o l B a r A d d ( n u l l ) ;  
  
 	 	 	 / / 	 C r é e   l a   p a r t i e   p o u r   l e s   d o c u m e n t s .  
 	 	 	 t h i s . b o o k D o c u m e n t s   =   n e w   T a b B o o k ( t h i s ) ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . P r e f e r r e d W i d t h   =   t h i s . p a n e l s W i d t h ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . M a r g i n s   =   n e w   M a r g i n s ( 1 ,   0 ,   3 ,   1 ) ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . A r r o w s   =   T a b B o o k A r r o w s . R i g h t ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . H a s C l o s e B u t t o n   =   t r u e ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . C l o s e B u t t o n . C o m m a n d O b j e c t   =   C o m m a n d . G e t ( " C l o s e " ) ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . A c t i v e P a g e C h a n g e d   + =   n e w   E v e n t H a n d l e r < C a n c e l E v e n t A r g s > ( t h i s . H a n d l e B o o k D o c u m e n t s A c t i v e P a g e C h a n g e d ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b o o k D o c u m e n t s . C l o s e B u t t o n ,   R e s . S t r i n g s . T o o l t i p . T a b B o o k . C l o s e ) ;  
  
 	 	 	 t h i s . S e t A c t i v e R i b b o n ( t h i s . r i b b o n M a i n ) ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   U p d a t e Q u i c k C o m m a n d s ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   t o u t e s   l e s   i c ô n e s   d e   l a   p a r t i e   r a p i d e ,   à   d r o i t e   d e s   c h o i x   d u   r u b a n   a c t i f .  
 	 	 	 / / 	 S u p p r i m e   t o u s   l e s   I c o n B u t t o n s .  
 	 	 	 t h i s . r i b b o n B o o k . B u t t o n s . C l e a r ( ) ;  
 	 	 	  
 	 	 	 b o o l   f i r s t   =   t r u e ;  
 	 	 	 f o r e a c h   (   s t r i n g   x c m d   i n   t h i s . g l o b a l S e t t i n g s . Q u i c k C o m m a n d s   )  
 	 	 	 {  
 	 	 	 	 b o o l       u s e d   =   G l o b a l S e t t i n g s . Q u i c k U s e d ( x c m d ) ;  
 	 	 	 	 b o o l       s e p     =   G l o b a l S e t t i n g s . Q u i c k S e p ( x c m d ) ;  
 	 	 	 	 s t r i n g   c m d     =   G l o b a l S e t t i n g s . Q u i c k C m d ( x c m d ) ;  
  
 	 	 	 	 i f   (   u s e d   )  
 	 	 	 	 {  
 	 	 	 	 	 i f   (   f i r s t   )     / /   p r e m i è r e   i c ô n e   ?  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . R i b b o n A d d ( n u l l ) ;     / /   s é p a r a t e u r   a u   d é b u t  
 	 	 	 	 	 	 f i r s t   =   f a l s e ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 C o m m a n d   c   =   C o m m o n . W i d g e t s . C o m m a n d . G e t ( c m d ) ;  
 	 	 	 	 	 t h i s . R i b b o n A d d ( c ) ;  
  
 	 	 	 	 	 i f   (   s e p   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . R i b b o n A d d ( n u l l ) ;     / /   s é p a r a t e u r   a p r è s   l ' i c ô n e  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e D o c u m e n t L a y o u t ( D o c u m e n t   d o c u m e n t )  
 	 	 {  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 d o u b l e   s w   =   1 7 ;     / /   l a r g e u r   d ' u n   a s c e n s e u r  
 	 	 	 d o u b l e   s r   =   1 3 ;     / /   l a r g e u r   d ' u n e   r è g l e  
 	 	 	 d o u b l e   w m   =   4 ;     / /   m a r g e s   a u t o u r   d u   v i e w e r  
 	 	 	 d o u b l e   l m   =   s r - 1 ;  
 	 	 	 d o u b l e   t m   =   s r - 1 ;  
 	 	 	  
 	 	 	 d i . t a b P a g e   =   n e w   T a b P a g e ( ) ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . I t e m s . I n s e r t ( t h i s . c u r r e n t D o c u m e n t ,   d i . t a b P a g e ) ;  
  
 	 	 	 F r a m e B o x   t o p P a n e   =   n e w   F r a m e B o x ( d i . t a b P a g e ) ;  
 	 	 	 t o p P a n e . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 d i . p a g e P a n e   =   n e w   F r a m e B o x ( d i . t a b P a g e ) ;  
 	 	 	 d i . p a g e P a n e . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 d i . p a g e P a n e . P a d d i n g   =   n e w   M a r g i n s ( w m ,   w m ,   0 ,   w m ) ;  
 	 	 	 d i . p a g e P a n e . V i s i b i l i t y   =   f a l s e ;  
  
 	 	 	 d i . m a i n P a n e   =   n e w   F r a m e B o x ( t o p P a n e ) ;  
 	 	 	 d i . m a i n P a n e . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 d i . l a y e r P a n e   =   n e w   F r a m e B o x ( t o p P a n e ) ;  
 	 	 	 d i . l a y e r P a n e . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 d i . l a y e r P a n e . P a d d i n g   =   n e w   M a r g i n s ( 0 ,   w m ,   w m ,   w m ) ;  
 	 	 	 d i . l a y e r P a n e . V i s i b i l i t y   =   f a l s e ;  
  
 	 	 	 / / 	 P a r t i e   g a u c h e   p r i n c i p a l e .  
 	 	 	 W i d g e t   m a i n V i e w P a r e n t   =   d i . m a i n P a n e ;  
 	 	 	 V i e w e r   v i e w e r   =   n e w   V i e w e r ( d o c u m e n t ) ;  
 	 	 	 v i e w e r . S e t P a r e n t ( m a i n V i e w P a r e n t ) ;  
 	 	 	 v i e w e r . A n c h o r   =   A n c h o r S t y l e s . A l l ;  
 	 	 	 v i e w e r . M a r g i n s   =   n e w   M a r g i n s ( w m + l m ,   w m + s w + 1 ,   6 + t m ,   w m + s w + 1 ) ;  
 	 	 	 d o c u m e n t . M o d i f i e r . A c t i v e V i e w e r   =   v i e w e r ;  
 	 	 	 d o c u m e n t . M o d i f i e r . A t t a c h V i e w e r ( v i e w e r ) ;  
  
 	 	 	 d i . h R u l e r   =   n e w   D o c W i d g e t s . H R u l e r ( m a i n V i e w P a r e n t ) ;  
 	 	 	 d i . h R u l e r . D o c u m e n t   =   d o c u m e n t ;  
 	 	 	 d i . h R u l e r . A n c h o r   =   A n c h o r S t y l e s . L e f t A n d R i g h t   |   A n c h o r S t y l e s . T o p ;  
 	 	 	 d i . h R u l e r . M a r g i n s   =   n e w   M a r g i n s ( w m + l m ,   w m + s w + 1 ,   6 ,   0 ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( d i . h R u l e r ,   " * " ) ;  
  
 	 	 	 d i . v R u l e r   =   n e w   D o c W i d g e t s . V R u l e r ( m a i n V i e w P a r e n t ) ;  
 	 	 	 d i . v R u l e r . D o c u m e n t   =   d o c u m e n t ;  
 	 	 	 d i . v R u l e r . A n c h o r   =   A n c h o r S t y l e s . T o p A n d B o t t o m   |   A n c h o r S t y l e s . L e f t ;  
 	 	 	 d i . v R u l e r . M a r g i n s   =   n e w   M a r g i n s ( w m ,   0 ,   6 + t m ,   w m + s w + 1 ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( d i . v R u l e r ,   " * " ) ;  
  
 	 	 	 / / 	 P a r t i e   b a s s e   p o u r   l e s   p a g e s   m i n i a t u r e s .  
 	 	 	 d i . p a g e M i n i a t u r e s   =   n e w   C o n t a i n e r s . P a g e M i n i a t u r e s ( d o c u m e n t ) ;  
 	 	 	 d o c u m e n t . P a g e M i n i a t u r e s   =   d i . p a g e M i n i a t u r e s ;  
 	 	 	 d i . p a g e M i n i a t u r e s . C r e a t e I n t e r f a c e ( d i . p a g e P a n e ) ;  
  
 	 	 	 / / 	 P a r t i e   d r o i t e   p o u r   l e s   c a l q u e s   m i n i a t u r e s .  
 	 	 	 d i . l a y e r M i n i a t u r e s   =   n e w   C o n t a i n e r s . L a y e r M i n i a t u r e s ( d o c u m e n t ) ;  
 	 	 	 d o c u m e n t . L a y e r M i n i a t u r e s   =   d i . l a y e r M i n i a t u r e s ;  
 	 	 	 d i . l a y e r M i n i a t u r e s . C r e a t e I n t e r f a c e ( d i . l a y e r P a n e ) ;  
  
 	 	 	 / / 	 B a n d e   h o r i z o n t a l e   q u i   c o n t i e n t   l e s   b o u t o n s   d e s   p a g e s   e t   l ' a s c e n s e u r .  
 	 	 	 W i d g e t   h B a n d   =   n e w   W i d g e t ( m a i n V i e w P a r e n t ) ;  
 	 	 	 h B a n d . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 h B a n d . A n c h o r   =   A n c h o r S t y l e s . L e f t A n d R i g h t   |   A n c h o r S t y l e s . B o t t o m ;  
 	 	 	 h B a n d . M a r g i n s   =   n e w   M a r g i n s ( w m ,   w m + s w + 1 ,   0 ,   w m ) ;  
  
 	 	 	 d i . q u i c k P a g e M i n i a t u r e s   =   n e w   G l y p h B u t t o n ( " P a g e M i n i a t u r e s " ) ;  
 	 	 	 d i . q u i c k P a g e M i n i a t u r e s . S e t P a r e n t ( h B a n d ) ;  
 	 	 	 d i . q u i c k P a g e M i n i a t u r e s . G l y p h S h a p e   =   G l y p h S h a p e . A r r o w U p ;  
 	 	 	 d i . q u i c k P a g e M i n i a t u r e s . P r e f e r r e d W i d t h   =   s w ;  
 	 	 	 d i . q u i c k P a g e M i n i a t u r e s . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 d i . q u i c k P a g e M i n i a t u r e s . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 d i . q u i c k P a g e M i n i a t u r e s . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   0 ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( d i . q u i c k P a g e M i n i a t u r e s ,   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . P a g e M i n i a t u r e s " ) ) ;  
  
 	 	 	 G l y p h B u t t o n   q u i c k P a g e P r e v   =   n e w   G l y p h B u t t o n ( " P a g e P r e v " ) ;  
 	 	 	 q u i c k P a g e P r e v . S e t P a r e n t ( h B a n d ) ;  
 	 	 	 q u i c k P a g e P r e v . G l y p h S h a p e   =   G l y p h S h a p e . A r r o w L e f t ;  
 	 	 	 q u i c k P a g e P r e v . P r e f e r r e d W i d t h   =   s w ;  
 	 	 	 q u i c k P a g e P r e v . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 q u i c k P a g e P r e v . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( q u i c k P a g e P r e v ,   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . P a g e P r e v " ) ) ;  
  
 	 	 	 d i . q u i c k P a g e M e n u   =   n e w   B u t t o n ( h B a n d ) ;  
 	 	 	 d i . q u i c k P a g e M e n u . B u t t o n S t y l e   =   B u t t o n S t y l e . I c o n ;  
 	 	 	 d i . q u i c k P a g e M e n u . C o m m a n d O b j e c t   =   C o m m a n d . G e t ( " P a g e M e n u " ) ;  
 	 	 	 d i . q u i c k P a g e M e n u . C l i c k e d   + =   t h i s . H a n d l e Q u i c k P a g e M e n u ;  
 	 	 	 d i . q u i c k P a g e M e n u . P r e f e r r e d W i d t h   =   S y s t e m . M a t h . F l o o r ( s w * 2 . 0 ) ;  
 	 	 	 d i . q u i c k P a g e M e n u . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 d i . q u i c k P a g e M e n u . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( d i . q u i c k P a g e M e n u ,   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . P a g e M e n u " ) ) ;  
  
 	 	 	 G l y p h B u t t o n   q u i c k P a g e N e x t   =   n e w   G l y p h B u t t o n ( " P a g e N e x t " ) ;  
 	 	 	 q u i c k P a g e N e x t . S e t P a r e n t ( h B a n d ) ;  
 	 	 	 q u i c k P a g e N e x t . G l y p h S h a p e   =   G l y p h S h a p e . A r r o w R i g h t ;  
 	 	 	 q u i c k P a g e N e x t . P r e f e r r e d W i d t h   =   s w ;  
 	 	 	 q u i c k P a g e N e x t . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 q u i c k P a g e N e x t . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 q u i c k P a g e N e x t . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   0 ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( q u i c k P a g e N e x t ,   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . P a g e N e x t " ) ) ;  
  
 	 	 	 G l y p h B u t t o n   q u i c k P a g e N e w   =   n e w   G l y p h B u t t o n ( " P a g e N e w " ) ;  
 	 	 	 q u i c k P a g e N e w . S e t P a r e n t ( h B a n d ) ;  
 	 	 	 q u i c k P a g e N e w . G l y p h S h a p e   =   G l y p h S h a p e . P l u s ;  
 	 	 	 q u i c k P a g e N e w . P r e f e r r e d W i d t h   =   s w ;  
 	 	 	 q u i c k P a g e N e w . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 q u i c k P a g e N e w . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 q u i c k P a g e N e w . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   0 ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( q u i c k P a g e N e w ,   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . P a g e N e w " ) ) ;  
  
 	 	 	 d i . h S c r o l l e r   =   n e w   H S c r o l l e r ( h B a n d ) ;  
 	 	 	 d i . h S c r o l l e r . V a l u e C h a n g e d   + =   t h i s . H a n d l e H S c r o l l e r V a l u e C h a n g e d ;  
 	 	 	 d i . h S c r o l l e r . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 / / 	 B a n d e   v e r t i c a l e   q u i   c o n t i e n t   l e s   b o u t o n s   d e s   c a l q u e s   e t   l ' a s c e n s e u r .  
 	 	 	 W i d g e t   v B a n d   =   n e w   W i d g e t ( m a i n V i e w P a r e n t ) ;  
 	 	 	 v B a n d . P r e f e r r e d W i d t h   =   s w ;  
 	 	 	 v B a n d . A n c h o r   =   A n c h o r S t y l e s . T o p A n d B o t t o m   |   A n c h o r S t y l e s . R i g h t ;  
 	 	 	 v B a n d . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   w m ,   6 ,   w m + s w + 1 ) ;  
  
 	 	 	 d i . q u i c k L a y e r M i n i a t u r e s   =   n e w   G l y p h B u t t o n ( " L a y e r M i n i a t u r e s " ) ;  
 	 	 	 d i . q u i c k L a y e r M i n i a t u r e s . S e t P a r e n t ( v B a n d ) ;  
 	 	 	 d i . q u i c k L a y e r M i n i a t u r e s . G l y p h S h a p e   =   G l y p h S h a p e . A r r o w L e f t ;  
 	 	 	 d i . q u i c k L a y e r M i n i a t u r e s . P r e f e r r e d W i d t h   =   s w ;  
 	 	 	 d i . q u i c k L a y e r M i n i a t u r e s . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 d i . q u i c k L a y e r M i n i a t u r e s . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 d i . q u i c k L a y e r M i n i a t u r e s . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   2 ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( d i . q u i c k L a y e r M i n i a t u r e s ,   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . L a y e r M i n i a t u r e s " ) ) ;  
  
 	 	 	 G l y p h B u t t o n   q u i c k L a y e r N e x t   =   n e w   G l y p h B u t t o n ( " L a y e r N e x t " ) ;  
 	 	 	 q u i c k L a y e r N e x t . S e t P a r e n t ( v B a n d ) ;  
 	 	 	 q u i c k L a y e r N e x t . G l y p h S h a p e   =   G l y p h S h a p e . A r r o w U p ;  
 	 	 	 q u i c k L a y e r N e x t . P r e f e r r e d W i d t h   =   s w ;  
 	 	 	 q u i c k L a y e r N e x t . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 q u i c k L a y e r N e x t . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( q u i c k L a y e r N e x t ,   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . L a y e r N e x t " ) ) ;  
  
 	 	 	 d i . q u i c k L a y e r M e n u   =   n e w   B u t t o n ( v B a n d ) ;  
 	 	 	 d i . q u i c k L a y e r M e n u . B u t t o n S t y l e   =   B u t t o n S t y l e . I c o n ;  
 	 	 	 d i . q u i c k L a y e r M e n u . C o m m a n d O b j e c t   =   C o m m a n d . G e t ( " L a y e r M e n u " ) ;  
 	 	 	 d i . q u i c k L a y e r M e n u . C l i c k e d   + =   t h i s . H a n d l e Q u i c k L a y e r M e n u ;  
 	 	 	 d i . q u i c k L a y e r M e n u . P r e f e r r e d W i d t h   =   s w ;  
 	 	 	 d i . q u i c k L a y e r M e n u . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 d i . q u i c k L a y e r M e n u . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( d i . q u i c k L a y e r M e n u ,   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . L a y e r M e n u " ) ) ;  
  
 	 	 	 G l y p h B u t t o n   q u i c k L a y e r P r e v   =   n e w   G l y p h B u t t o n ( " L a y e r P r e v " ) ;  
 	 	 	 q u i c k L a y e r P r e v . S e t P a r e n t ( v B a n d ) ;  
 	 	 	 q u i c k L a y e r P r e v . G l y p h S h a p e   =   G l y p h S h a p e . A r r o w D o w n ;  
 	 	 	 q u i c k L a y e r P r e v . P r e f e r r e d W i d t h   =   s w ;  
 	 	 	 q u i c k L a y e r P r e v . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 q u i c k L a y e r P r e v . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 q u i c k L a y e r P r e v . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   2 ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( q u i c k L a y e r P r e v ,   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . L a y e r P r e v " ) ) ;  
  
 	 	 	 G l y p h B u t t o n   q u i c k L a y e r N e w   =   n e w   G l y p h B u t t o n ( " L a y e r N e w " ) ;  
 	 	 	 q u i c k L a y e r N e w . S e t P a r e n t ( v B a n d ) ;  
 	 	 	 q u i c k L a y e r N e w . G l y p h S h a p e   =   G l y p h S h a p e . P l u s ;  
 	 	 	 q u i c k L a y e r N e w . P r e f e r r e d W i d t h   =   s w ;  
 	 	 	 q u i c k L a y e r N e w . P r e f e r r e d H e i g h t   =   s w ;  
 	 	 	 q u i c k L a y e r N e w . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 q u i c k L a y e r N e w . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   2 ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( q u i c k L a y e r N e w ,   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . L a y e r N e w " ) ) ;  
  
 	 	 	 d i . v S c r o l l e r   =   n e w   V S c r o l l e r ( v B a n d ) ;  
 	 	 	 d i . v S c r o l l e r . V a l u e C h a n g e d   + =   t h i s . H a n d l e V S c r o l l e r V a l u e C h a n g e d ;  
 	 	 	 d i . v S c r o l l e r . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 d i . b o o k P a n e l s   =   n e w   T a b B o o k ( t h i s ) ;  
 	 	 	 d i . b o o k P a n e l s . P r e f e r r e d W i d t h   =   t h i s . p a n e l s W i d t h ;  
 	 	 	 d i . b o o k P a n e l s . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 d i . b o o k P a n e l s . M a r g i n s   =   n e w   M a r g i n s ( 1 ,   1 ,   3 ,   1 ) ;  
 	 	 	 d i . b o o k P a n e l s . A r r o w s   =   T a b B o o k A r r o w s . S t r e t c h ;  
 	 	 	 d i . b o o k P a n e l s . A c t i v e P a g e C h a n g e d   + =   n e w   E v e n t H a n d l e r < C a n c e l E v e n t A r g s > ( t h i s . H a n d l e B o o k P a n e l s A c t i v e P a g e C h a n g e d ) ;  
  
 	 	 	 T a b P a g e   b o o k P r i n c i p a l   =   n e w   T a b P a g e ( ) ;  
 	 	 	 b o o k P r i n c i p a l . T a b T i t l e   =   R e s . S t r i n g s . T a b P a g e . P r i n c i p a l ;  
 	 	 	 b o o k P r i n c i p a l . N a m e   =   " P r i n c i p a l " ;  
 	 	 	 d i . b o o k P a n e l s . I t e m s . A d d ( b o o k P r i n c i p a l ) ;  
  
 	 	 	 T a b P a g e   b o o k S t y l e s   =   n e w   T a b P a g e ( ) ;  
 	 	 	 b o o k S t y l e s . T a b T i t l e   =   R e s . S t r i n g s . T a b P a g e . S t y l e s ;  
 	 	 	 b o o k S t y l e s . N a m e   =   " S t y l e s " ;  
 	 	 	 d i . b o o k P a n e l s . I t e m s . A d d ( b o o k S t y l e s ) ;  
  
 	 	 	 T a b P a g e   b o o k A u t o s   =   n u l l ;  
 	 	 	 i f   (   t h i s . d e b u g M o d e   = =   D e b u g M o d e . D e b u g C o m m a n d s   )  
 	 	 	 {  
 	 	 	 	 b o o k A u t o s   =   n e w   T a b P a g e ( ) ;  
 	 	 	 	 b o o k A u t o s . T a b T i t l e   =   R e s . S t r i n g s . T a b P a g e . A u t o s ;  
 	 	 	 	 b o o k A u t o s . N a m e   =   " A u t o s " ;  
 	 	 	 	 d i . b o o k P a n e l s . I t e m s . A d d ( b o o k A u t o s ) ;  
 	 	 	 }  
  
 	 	 	 T a b P a g e   b o o k P a g e s   =   n e w   T a b P a g e ( ) ;  
 	 	 	 b o o k P a g e s . T a b T i t l e   =   R e s . S t r i n g s . T a b P a g e . P a g e s ;  
 	 	 	 b o o k P a g e s . N a m e   =   " P a g e s " ;  
 	 	 	 d i . b o o k P a n e l s . I t e m s . A d d ( b o o k P a g e s ) ;  
  
 	 	 	 T a b P a g e   b o o k L a y e r s   =   n e w   T a b P a g e ( ) ;  
 	 	 	 b o o k L a y e r s . T a b T i t l e   =   R e s . S t r i n g s . T a b P a g e . L a y e r s ;  
 	 	 	 b o o k L a y e r s . N a m e   =   " L a y e r s " ;  
 	 	 	 d i . b o o k P a n e l s . I t e m s . A d d ( b o o k L a y e r s ) ;  
  
 # i f   f a l s e  
 	 	 	 T a b P a g e   b o o k O p e r   =   n e w   T a b P a g e ( ) ;  
 	 	 	 b o o k O p e r . T a b T i t l e   =   R e s . S t r i n g s . T a b P a g e . O p e r a t i o n s ;  
 	 	 	 b o o k O p e r . N a m e   =   " O p e r a t i o n s " ;  
 	 	 	 d i . b o o k P a n e l s . I t e m s . A d d ( b o o k O p e r ) ;  
 # e n d i f  
  
 	 	 	 d i . b o o k P a n e l s . A c t i v e P a g e   =   b o o k P r i n c i p a l ;  
  
 	 	 	 d i . c o n t a i n e r P r i n c i p a l   =   n e w   C o n t a i n e r s . P r i n c i p a l ( d o c u m e n t ) ;  
 	 	 	 d i . c o n t a i n e r P r i n c i p a l . S e t P a r e n t ( b o o k P r i n c i p a l ) ;  
 	 	 	 d i . c o n t a i n e r P r i n c i p a l . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 d i . c o n t a i n e r P r i n c i p a l . M a r g i n s   =   n e w   M a r g i n s ( 4 ,   4 ,   1 0 ,   4 ) ;  
 	 	 	 d o c u m e n t . M o d i f i e r . A t t a c h C o n t a i n e r ( d i . c o n t a i n e r P r i n c i p a l ) ;  
  
 	 	 	 d i . c o n t a i n e r S t y l e s   =   n e w   C o n t a i n e r s . S t y l e s ( d o c u m e n t ) ;  
 	 	 	 d i . c o n t a i n e r S t y l e s . S e t P a r e n t ( b o o k S t y l e s ) ;  
 	 	 	 d i . c o n t a i n e r S t y l e s . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 d i . c o n t a i n e r S t y l e s . M a r g i n s   =   n e w   M a r g i n s ( 4 ,   4 ,   1 0 ,   4 ) ;  
 	 	 	 d o c u m e n t . M o d i f i e r . A t t a c h C o n t a i n e r ( d i . c o n t a i n e r S t y l e s ) ;  
  
 	 	 	 i f   (   b o o k A u t o s   ! =   n u l l   )  
 	 	 	 {  
 	 	 	 	 d i . c o n t a i n e r A u t o s   =   n e w   C o n t a i n e r s . A u t o s ( d o c u m e n t ) ;  
 	 	 	 	 d i . c o n t a i n e r A u t o s . S e t P a r e n t ( b o o k A u t o s ) ;  
 	 	 	 	 d i . c o n t a i n e r A u t o s . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 	 d i . c o n t a i n e r A u t o s . M a r g i n s   =   n e w   M a r g i n s ( 4 ,   4 ,   1 0 ,   4 ) ;  
 	 	 	 	 d o c u m e n t . M o d i f i e r . A t t a c h C o n t a i n e r ( d i . c o n t a i n e r A u t o s ) ;  
 	 	 	 }  
  
 	 	 	 d i . c o n t a i n e r P a g e s   =   n e w   C o n t a i n e r s . P a g e s ( d o c u m e n t ) ;  
 	 	 	 d i . c o n t a i n e r P a g e s . S e t P a r e n t ( b o o k P a g e s ) ;  
 	 	 	 d i . c o n t a i n e r P a g e s . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 d i . c o n t a i n e r P a g e s . M a r g i n s   =   n e w   M a r g i n s ( 4 ,   4 ,   1 0 ,   4 ) ;  
 	 	 	 d o c u m e n t . M o d i f i e r . A t t a c h C o n t a i n e r ( d i . c o n t a i n e r P a g e s ) ;  
  
 	 	 	 d i . c o n t a i n e r L a y e r s   =   n e w   C o n t a i n e r s . L a y e r s ( d o c u m e n t ) ;  
 	 	 	 d i . c o n t a i n e r L a y e r s . S e t P a r e n t ( b o o k L a y e r s ) ;  
 	 	 	 d i . c o n t a i n e r L a y e r s . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 d i . c o n t a i n e r L a y e r s . M a r g i n s   =   n e w   M a r g i n s ( 4 ,   4 ,   1 0 ,   4 ) ;  
 	 	 	 d o c u m e n t . M o d i f i e r . A t t a c h C o n t a i n e r ( d i . c o n t a i n e r L a y e r s ) ;  
  
 # i f   f a l s e  
 	 	 	 d i . c o n t a i n e r O p e r a t i o n s   =   n e w   C o n t a i n e r s . O p e r a t i o n s ( d o c u m e n t ) ;  
 	 	 	 d i . c o n t a i n e r O p e r a t i o n s . P a r e n t   =   b o o k O p e r ;  
 	 	 	 d i . c o n t a i n e r O p e r a t i o n s . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 d i . c o n t a i n e r O p e r a t i o n s . M a r g i n s   =   n e w   M a r g i n s ( 4 ,   4 ,   1 0 ,   4 ) ;  
 	 	 	 d o c u m e n t . M o d i f i e r . A t t a c h C o n t a i n e r ( d i . c o n t a i n e r O p e r a t i o n s ) ;  
 # e n d i f  
 	 	 }  
  
 	 	 p u b l i c   H M e n u   G e t M e n u ( )  
 	 	 {  
 	 	 	 r e t u r n   t h i s . m e n u ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   M e n u A d d S u b ( V M e n u   m e n u ,   V M e n u   s u b ,   s t r i n g   c m d )  
 	 	 {  
 	 	 	 / / 	 A j o u t e   u n   s o u s - m e n u   d a n s   u n   m e n u .  
 	 	 	 f o r   (   i n t   i = 0   ;   i < m e n u . I t e m s . C o u n t   ;   i + +   )  
 	 	 	 {  
 	 	 	 	 M e n u I t e m   i t e m   =   m e n u . I t e m s [ i ]   a s   M e n u I t e m ;  
 	 	 	 	 i f   ( i t e m . C o m m a n d O b j e c t . C o m m a n d I d   = =   c m d )  
 	 	 	 	 {  
 	 	 	 	 	 i t e m . S u b m e n u   =   s u b ;  
 / / ? 	 	 	 	 	 i t e m . C o m m a n d O b j e c t   =   C o m m a n d . G e t ( c m d ) ;  
 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t ( f a l s e ,   " M e n u A d d S u b :   s u b m e n u   n o t   f o u n d " ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   M e n u A d d ( V M e n u   v m e n u ,   s t r i n g   c o m m a n d )  
 	 	 {  
 	 	 	 / / 	 A j o u t e   u n e   i c ô n e .  
 	 	 	 i f   (   c o m m a n d   = =   n u l l   )  
 	 	 	 {  
 	 	 	 	 v m e n u . I t e m s . A d d ( n e w   M e n u S e p a r a t o r ( ) ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 C o m m a n d   c   =   C o m m o n . W i d g e t s . C o m m a n d . G e t ( c o m m a n d ) ;  
  
 	 	 	 	 M e n u I t e m   i t e m   =   n e w   M e n u I t e m ( c . C o m m a n d I d ,   c . I c o n ,   c . D e s c r i p t i o n ,   M i s c . G e t S h o r t c u t ( c ) ,   c . C o m m a n d I d ) ;  
 	 	 	 	 v m e n u . I t e m s . A d d ( i t e m ) ;  
 	 	 	 }  
 	 	 }  
 	 	  
 	 	 p r o t e c t e d   v o i d   M e n u A d d ( V M e n u   v m e n u ,   s t r i n g   i c o n ,   s t r i n g   c o m m a n d ,   s t r i n g   t e x t ,   s t r i n g   s h o r t c u t )  
 	 	 {  
 	 	 	 / / 	 A j o u t e   u n e   i c ô n e .  
 	 	 	 t h i s . M e n u A d d ( v m e n u ,   i c o n ,   c o m m a n d ,   t e x t ,   s h o r t c u t ,   c o m m a n d ) ;  
 	 	 }  
 	 	  
 	 	 p r o t e c t e d   v o i d   M e n u A d d ( V M e n u   v m e n u ,   s t r i n g   i c o n ,   s t r i n g   c o m m a n d ,   s t r i n g   t e x t ,   s t r i n g   s h o r t c u t ,   s t r i n g   n a m e )  
 	 	 {  
 	 	 	 i f   (   t e x t   = =   " "   )  
 	 	 	 {  
 	 	 	 	 v m e n u . I t e m s . A d d ( n e w   M e n u S e p a r a t o r ( ) ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 M e n u I t e m   i t e m ;  
 	 	 	 	  
 	 	 	 	 i f   (   i c o n   = =   " y / n "   )  
 	 	 	 	 {  
 	 	 	 	 	 i t e m   =   M e n u I t e m . C r e a t e Y e s N o ( c o m m a n d ,   t e x t ,   s h o r t c u t ,   n a m e ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 i t e m   =   n e w   M e n u I t e m ( c o m m a n d ,   i c o n ,   t e x t ,   s h o r t c u t ,   n a m e ) ;  
 	 	 	 	 }  
 	 	 	 	  
 	 	 	 	 v m e n u . I t e m s . A d d ( i t e m ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   W i d g e t   V T o o l B a r A d d ( C o m m a n d   c o m m a n d )  
 	 	 {  
 	 	 	 / / 	 A j o u t e   u n e   i c ô n e .  
 	 	 	 i f   (   c o m m a n d   = =   n u l l   )  
 	 	 	 {  
 	 	 	 	 I c o n S e p a r a t o r   s e p   =   n e w   I c o n S e p a r a t o r ( ) ;  
 	 	 	 	 s e p . I s H o r i z o n t a l   =   f a l s e ;  
 	 	 	 	 t h i s . v T o o l B a r . I t e m s . A d d ( s e p ) ;  
 	 	 	 	 r e t u r n   s e p ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 I c o n B u t t o n   b u t t o n   =   n e w   I c o n B u t t o n ( c o m m a n d ) ;  
 	 	 	 	 t h i s . v T o o l B a r . I t e m s . A d d ( b u t t o n ) ;  
 	 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( b u t t o n ,   c o m m a n d . G e t D e s c r i p t i o n W i t h S h o r t c u t ( ) ) ;  
 	 	 	 	 r e t u r n   b u t t o n ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   S t a t u s F i e l d   I n f o A d d ( s t r i n g   n a m e ,   d o u b l e   w i d t h )  
 	 	 {  
 	 	 	 S t a t u s F i e l d   f i e l d   =   n e w   S t a t u s F i e l d ( ) ;  
 	 	 	 f i e l d . P r e f e r r e d W i d t h   =   w i d t h ;  
 	 	 	 t h i s . i n f o . I t e m s . A d d ( f i e l d ) ;  
  
 	 	 	 i n t   i   =   t h i s . i n f o . C h i l d r e n . C o u n t - 1 ;  
 	 	 	 t h i s . i n f o . I t e m s [ i ] . N a m e   =   n a m e ;  
 	 	 	 r e t u r n   f i e l d ;  
 	 	 }  
  
 	 	 p r o t e c t e d   I c o n B u t t o n   I n f o A d d ( s t r i n g   c o m m a n d N a m e )  
 	 	 {  
 	 	 	 C o m m a n d   c o m m a n d   =   C o m m o n . W i d g e t s . C o m m a n d . G e t   ( c o m m a n d N a m e ) ;  
  
 	 	 	 I c o n B u t t o n   b u t t o n   =   n e w   I c o n B u t t o n ( c o m m a n d ) ;  
 	 	 	 b u t t o n . P r e f e r r e d I c o n S i z e   =   M i s c . I c o n P r e f e r r e d S i z e ( " S m a l l " ) ;  
 	 	 	 d o u b l e   h   =   t h i s . i n f o . P r e f e r r e d H e i g h t - 3 ;  
 	 	 	 b u t t o n . P r e f e r r e d S i z e   =   n e w   S i z e ( h ,   h ) ;  
 	 	 	 t h i s . i n f o . I t e m s . A d d ( b u t t o n ) ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( b u t t o n ,   c o m m a n d . G e t D e s c r i p t i o n W i t h S h o r t c u t ( ) ) ;  
 	 	 	 r e t u r n   b u t t o n ;  
 	 	 }  
  
  
 	 	 # r e g i o n   R i b b o n s  
 	 	 v o i d   H a n d l e R i b b o n B o o k A c t i v e P a g e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 L e   r u b a n   a c t i f   a   é t é   c h a n g é   ( n o u v e l l e   p a g e   v i s i b l e ) .  
 	 	 	 R i b b o n P a g e   p a g e   =   t h i s . r i b b o n B o o k . A c t i v e P a g e ;  
 	 	 	 t h i s . S e t A c t i v e R i b b o n ( p a g e ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   R i b b o n P a g e   G e t R i b b o n ( s t r i n g   n a m e )  
 	 	 {  
 	 	 	 / / 	 D o n n e   l e   r u b a n   c o r r e s p o n d a n t   à   u n   n o m .  
 	 	 	 i f   (   n a m e   = =   t h i s . r i b b o n M a i n . N a m e   )     r e t u r n   t h i s . r i b b o n M a i n ;  
 	 	 	 i f   (   n a m e   = =   t h i s . r i b b o n G e o m . N a m e   )     r e t u r n   t h i s . r i b b o n G e o m ;  
 	 	 	 i f   (   n a m e   = =   t h i s . r i b b o n O p e r . N a m e   )     r e t u r n   t h i s . r i b b o n O p e r ;  
 	 	 	 i f   (   n a m e   = =   t h i s . r i b b o n T e x t . N a m e   )     r e t u r n   t h i s . r i b b o n T e x t ;  
 	 	 	 r e t u r n   n u l l ;  
 	 	 }  
  
 	 	 p r o t e c t e d   R i b b o n P a g e   L a s t R i b b o n ( s t r i n g   n o t N a m e )  
 	 	 {  
 	 	 	 / / 	 C h e r c h e   l e   d e r n i e r   r u b a n   u t i l i s é   d i f f é r e n t   d ' u n   n o m   d o n n é .  
 	 	 	 i f   (   t h i s . r i b b o n L i s t   = =   n u l l   )     r e t u r n   n u l l ;  
  
 	 	 	 f o r   (   i n t   i = t h i s . r i b b o n L i s t . C o u n t - 1   ;   i > = 0   ;   i - -   )  
 	 	 	 {  
 	 	 	 	 s t r i n g   n a m e   =   t h i s . r i b b o n L i s t [ i ]   a s   s t r i n g ;  
 	 	 	 	 i f   (   n a m e   ! =   n o t N a m e   )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   t h i s . G e t R i b b o n ( n a m e ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 r e t u r n   n u l l ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   S e t A c t i v e R i b b o n ( R i b b o n P a g e   a c t i v e )  
 	 	 {  
 	 	 	 / / 	 A c t i v e   u n   r u b a n .  
 	 	 	 t h i s . r i b b o n A c t i v e   =   a c t i v e ;  
 	 	 	 t h i s . r i b b o n B o o k . A c t i v e P a g e   =   a c t i v e ;  
  
 	 	 	 i f   (   t h i s . r i b b o n L i s t   = =   n u l l   )  
 	 	 	 {  
 	 	 	 	 t h i s . r i b b o n L i s t   =   n e w   S y s t e m . C o l l e c t i o n s . A r r a y L i s t ( ) ;  
 	 	 	 }  
  
 	 	 	 i f   (   a c t i v e   = =   n u l l   )  
 	 	 	 {  
 	 	 	 	 t h i s . r i b b o n L i s t . A d d ( " " ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . r i b b o n L i s t . A d d ( a c t i v e . N a m e ) ;  
 	 	 	 }  
  
 	 	 	 i f   (   t h i s . r i b b o n L i s t . C o u n t   >   1 0   )  
 	 	 	 {  
 	 	 	 	 t h i s . r i b b o n L i s t . R e m o v e A t ( 0 ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   R i b b o n s N o t i f y C h a n g e d ( s t r i n g   c h a n g e d )  
 	 	 {  
 	 	 	 / / 	 P a s s e   e n   r e v u e   t o u t e s   l e s   s e c t i o n s   d e   t o u t e s   l e s   p a g e s .  
 	 	 	 f o r e a c h   ( W i d g e t   w i d g e t   i n   t h i s . r i b b o n B o o k . P a g e s )  
 	 	 	 {  
 	 	 	 	 R i b b o n P a g e   p a g e   =   w i d g e t   a s   R i b b o n P a g e ;  
 	 	 	 	 i f   ( p a g e   = =   n u l l )     c o n t i n u e ;  
  
 	 	 	 	 f o r e a c h   ( R i b b o n s . A b s t r a c t   s e c t i o n   i n   p a g e . I t e m s )  
 	 	 	 	 {  
 	 	 	 	 	 s e c t i o n . N o t i f y C h a n g e d ( c h a n g e d ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   R i b b o n s N o t i f y T e x t S t y l e s C h a n g e d ( S y s t e m . C o l l e c t i o n s . A r r a y L i s t   t e x t S t y l e L i s t )  
 	 	 {  
 	 	 	 / / 	 P a s s e   e n   r e v u e   t o u t e s   l e s   s e c t i o n s   d e   t o u t e s   l e s   p a g e s .  
 	 	 	 f o r e a c h   ( W i d g e t   w i d g e t   i n   t h i s . r i b b o n B o o k . P a g e s )  
 	 	 	 {  
 	 	 	 	 R i b b o n P a g e   p a g e   =   w i d g e t   a s   R i b b o n P a g e ;  
 	 	 	 	 i f   ( p a g e   = =   n u l l )     c o n t i n u e ;  
  
 	 	 	 	 f o r e a c h   ( R i b b o n s . A b s t r a c t   s e c t i o n   i n   p a g e . I t e m s )  
 	 	 	 	 {  
 	 	 	 	 	 s e c t i o n . N o t i f y T e x t S t y l e s C h a n g e d ( t e x t S t y l e L i s t ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   R i b b o n s N o t i f y T e x t S t y l e s C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 P a s s e   e n   r e v u e   t o u t e s   l e s   s e c t i o n s   d e   t o u t e s   l e s   p a g e s .  
 	 	 	 f o r e a c h   ( W i d g e t   w i d g e t   i n   t h i s . r i b b o n B o o k . P a g e s )  
 	 	 	 {  
 	 	 	 	 R i b b o n P a g e   p a g e   =   w i d g e t   a s   R i b b o n P a g e ;  
 	 	 	 	 i f   ( p a g e   = =   n u l l )     c o n t i n u e ;  
  
 	 	 	 	 f o r e a c h   ( R i b b o n s . A b s t r a c t   s e c t i o n   i n   p a g e . I t e m s )  
 	 	 	 	 {  
 	 	 	 	 	 s e c t i o n . N o t i f y T e x t S t y l e s C h a n g e d ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   R i b b o n s S e t D o c u m e n t ( S e t t i n g s . G l o b a l S e t t i n g s   g s ,   D o c u m e n t   d o c u m e n t )  
 	 	 {  
 	 	 	 / / 	 P a s s e   e n   r e v u e   t o u t e s   l e s   s e c t i o n s   d e   t o u t e s   l e s   p a g e s .  
 	 	 	 f o r e a c h   ( W i d g e t   w i d g e t   i n   t h i s . r i b b o n B o o k . P a g e s )  
 	 	 	 {  
 	 	 	 	 R i b b o n P a g e   p a g e   =   w i d g e t   a s   R i b b o n P a g e ;  
 	 	 	 	 i f   ( p a g e   = =   n u l l )     c o n t i n u e ;  
  
 	 	 	 	 f o r e a c h   ( R i b b o n s . A b s t r a c t   s e c t i o n   i n   p a g e . I t e m s )  
 	 	 	 	 {  
 	 	 	 	 	 s e c t i o n . S e t D o c u m e n t ( g s ,   d o c u m e n t ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   W i d g e t   R i b b o n A d d ( C o m m a n d   c o m m a n d )  
 	 	 {  
 	 	 	 / / 	 A j o u t e   u n e   i c ô n e .  
 	 	 	 i f   ( c o m m a n d   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 I c o n S e p a r a t o r   s e p   =   n e w   I c o n S e p a r a t o r ( ) ;  
 	 	 	 	 s e p . I s H o r i z o n t a l   =   t r u e ;  
 	 	 	 	 t h i s . r i b b o n B o o k . B u t t o n s . A d d ( s e p ) ;  
 	 	 	 	 r e t u r n   s e p ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 I c o n B u t t o n   b u t t o n   =   n e w   I c o n B u t t o n ( c o m m a n d ) ;  
 	 	 	 	 t h i s . r i b b o n B o o k . B u t t o n s . A d d ( b u t t o n ) ;  
 	 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( b u t t o n ,   c o m m a n d . G e t D e s c r i p t i o n W i t h S h o r t c u t ( ) ) ;  
 	 	 	 	 r e t u r n   b u t t o n ;  
 	 	 	 }  
 	 	 }  
 	 	 # e n d r e g i o n  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e D l g C l o s e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 U n   d i a l o g u e   a   é t é   f e r m é .  
 	 	 	 i f   (   s e n d e r   = =   t h i s . d l g G l y p h s   )  
 	 	 	 {  
 	 	 	 	 t h i s . g l y p h s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 i f   (   s e n d e r   = =   t h i s . d l g I n f o s   )  
 	 	 	 {  
 	 	 	 	 t h i s . i n f o s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 i f   (   s e n d e r   = =   t h i s . d l g P a g e S t a c k   )  
 	 	 	 {  
 	 	 	 	 t h i s . p a g e S t a c k S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 i f   (   s e n d e r   = =   t h i s . d l g S e t t i n g s   )  
 	 	 	 {  
 	 	 	 	 t h i s . s e t t i n g s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 i f   (   s e n d e r   = =   t h i s . d l g R e p l a c e   )  
 	 	 	 {  
 	 	 	 	 t h i s . r e p l a c e S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e H S c r o l l e r V a l u e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 H S c r o l l e r   s c r o l l e r   =   s e n d e r   a s   H S c r o l l e r ;  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . D r a w i n g C o n t e x t . O r i g i n X   =   ( d o u b l e )   - s c r o l l e r . V a l u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e V S c r o l l e r V a l u e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 V S c r o l l e r   s c r o l l e r   =   s e n d e r   a s   V S c r o l l e r ;  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . D r a w i n g C o n t e x t . O r i g i n Y   =   ( d o u b l e )   - s c r o l l e r . V a l u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B o o k P a n e l s A c t i v e P a g e C h a n g e d ( o b j e c t   s e n d e r ,   C a n c e l E v e n t A r g s   e )  
 	 	 {  
 	 	 	 T a b B o o k   b o o k   =   s e n d e r   a s   T a b B o o k ;  
 	 	 	 T a b P a g e   p a g e   =   b o o k . A c t i v e P a g e ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T a b B o o k C h a n g e d ( p a g e . N a m e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S t a t u s Z o o m C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	 S t a t u s F i e l d   s f   =   s e n d e r   a s   S t a t u s F i e l d ;  
 	 	 	 i f   (   s f   = =   n u l l   )     r e t u r n ;  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 V M e n u   m e n u   =   M e n u s . Z o o m M e n u . C r e a t e Z o o m M e n u ( c o n t e x t . Z o o m ,   c o n t e x t . Z o o m P a g e ,   n u l l ) ;  
 	 	 	 m e n u . H o s t   =   s f . W i n d o w ;  
 	 	 	 T e x t F i e l d C o m b o . A d j u s t C o m b o S i z e ( s f ,   m e n u ,   f a l s e ) ;  
 	 	 	 m e n u . S h o w A s C o m b o L i s t ( s f ,   P o i n t . Z e r o ,   s f ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S l i d e r Z o o m C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	 A b s t r a c t S l i d e r   s l i d e r   =   s e n d e r   a s   A b s t r a c t S l i d e r ;  
 	 	 	 i f   (   s l i d e r   = =   n u l l   )     r e t u r n ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m V a l u e ( ( d o u b l e )   s l i d e r . V a l u e ,   s l i d e r . I s I n i t i a l C h a n g e ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   o v e r r i d e   v o i d   U p d a t e C l i e n t G e o m e t r y ( )  
 	 	 {  
 	 	 	 b a s e . U p d a t e C l i e n t G e o m e t r y ( ) ;  
 	 	 }  
  
  
 	 	 [ C o m m a n d   ( " T o o l S e l e c t " ) ]  
 	 	 [ C o m m a n d   ( " T o o l G l o b a l " ) ]  
 	 	 [ C o m m a n d   ( " T o o l S h a p e r " ) ]  
 	 	 [ C o m m a n d   ( " T o o l E d i t " ) ]  
 	 	 [ C o m m a n d   ( " T o o l Z o o m " ) ]  
 	 	 [ C o m m a n d   ( " T o o l H a n d " ) ]  
 	 	 [ C o m m a n d   ( " T o o l P i c k e r " ) ]  
 	 	 [ C o m m a n d   ( " T o o l H o t S p o t " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t L i n e " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t R e c t a n g l e " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t C i r c l e " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t E l l i p s e " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t P o l y " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t F r e e " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t B e z i e r " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t R e g u l a r " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t S u r f a c e " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t V o l u m e " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t T e x t L i n e " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t T e x t L i n e 2 " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t T e x t B o x " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t T e x t B o x 2 " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t A r r a y " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t I m a g e " ) ]  
 	 	 [ C o m m a n d   ( " O b j e c t D i m e n s i o n " ) ]  
 	 	 v o i d   C o m m a n d T o o l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o o l   =   e . C o m m a n d . C o m m a n d I d ;  
 	 	 	 t h i s . D i s p a t c h D u m m y M o u s e M o v e E v e n t ( ) ;  
 	 	 }  
  
  
 	 	  
 	 	 # r e g i o n   I O  
 	 	 p r o t e c t e d   C o m m o n . D i a l o g s . D i a l o g R e s u l t   D i a l o g S a v e ( )  
 	 	 {  
 	 	 	 / / 	 A f f i c h e   l e   d i a l o g u e   p o u r   d e m a n d e r   s ' i l   f a u t   e n r e g i s t r e r   l e  
 	 	 	 / / 	 d o c u m e n t   m o d i f i é ,   a v a n t   d e   p a s s e r   à   u n   a u t r e   d o c u m e n t .  
 	 	 	 i f   (   ! t h i s . C u r r e n t D o c u m e n t . I s D i r t y S e r i a l i z e   | |  
 	 	 	 	   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S t a t i s t i c T o t a l O b j e c t s ( )   = =   0   )  
 	 	 	 {  
 	 	 	 	 r e t u r n   C o m m o n . D i a l o g s . D i a l o g R e s u l t . N o n e ;  
 	 	 	 }  
  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 s t r i n g   t i t l e   =   R e s . S t r i n g s . A p p l i c a t i o n . T i t l e S h o r t ;  
 	 	 	 s t r i n g   i c o n   =   " m a n i f e s t : E p s i t e c . C o m m o n . D i a l o g s . I m a g e s . Q u e s t i o n . i c o n " ;  
 	 	 	 s t r i n g   s h o r t F i l e n a m e   =   M i s c . E x t r a c t N a m e ( t h i s . C u r r e n t D o c u m e n t . F i l e n a m e ,   t h i s . C u r r e n t D o c u m e n t . I s D i r t y S e r i a l i z e ) ;  
 	 	 	 s t r i n g   s t a t i s t i c   =   s t r i n g . F o r m a t ( " < f o n t   s i z e = \ " 8 0 % \ " > { 0 } < / f o n t > < b r / > " ,   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S t a t i s t i c ( f a l s e ,   f a l s e ) ) ;  
 	 	 	 s t r i n g   q u e s t i o n 1   =   s t r i n g . F o r m a t ( R e s . S t r i n g s . D i a l o g . Q u e s t i o n . S a v e . P a r t 1 ,   s h o r t F i l e n a m e ) ;  
 	 	 	 s t r i n g   q u e s t i o n 2   =   R e s . S t r i n g s . D i a l o g . Q u e s t i o n . S a v e . P a r t 2 ;  
 	 	 	 s t r i n g   m e s s a g e   =   s t r i n g . F o r m a t ( " < f o n t   s i z e = \ " 1 0 0 % \ " > { 0 } < / f o n t > < b r / > < b r / > { 1 } { 2 } " ,   q u e s t i o n 1 ,   s t a t i s t i c ,   q u e s t i o n 2 ) ;  
 	 	 	 C o m m o n . D i a l o g s . I D i a l o g   d i a l o g   =   C o m m o n . D i a l o g s . M e s s a g e D i a l o g . C r e a t e Y e s N o C a n c e l ( t i t l e ,   i c o n ,   m e s s a g e ,   n u l l ,   n u l l ,   t h i s . c o m m a n d D i s p a t c h e r ) ;  
 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 r e t u r n   d i a l o g . R e s u l t ;  
 	 	 }  
  
 	 	 p r o t e c t e d   C o m m o n . D i a l o g s . D i a l o g R e s u l t   D i a l o g W a r n i n g s ( S y s t e m . C o l l e c t i o n s . A r r a y L i s t   w a r n i n g s )  
 	 	 {  
 	 	 	 / / 	 A f f i c h e   l e   d i a l o g u e   p o u r   s i g n a l e r   l a   l i s t e   d e   t o u s   l e s   p r o b l è m e s .  
 	 	 	 i f   (   w a r n i n g s   = =   n u l l   | |   w a r n i n g s . C o u n t   = =   0   )     r e t u r n   C o m m o n . D i a l o g s . D i a l o g R e s u l t . N o n e ;  
  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
 	 	 	 w a r n i n g s . S o r t ( ) ;  
  
 	 	 	 s t r i n g   t i t l e   =   R e s . S t r i n g s . A p p l i c a t i o n . T i t l e S h o r t ;  
 	 	 	 s t r i n g   i c o n   =   " m a n i f e s t : E p s i t e c . C o m m o n . D i a l o g s . I m a g e s . W a r n i n g . i c o n " ;  
  
 	 	 	 s t r i n g   c h i p   =   " < l i s t   t y p e = \ " f i x \ "   w i d t h = \ " 1 . 5 \ " / > " ;  
 	 	 	 S y s t e m . T e x t . S t r i n g B u i l d e r   b u i l d e r   =   n e w   S y s t e m . T e x t . S t r i n g B u i l d e r ( ) ;  
 	 	 	 b u i l d e r . A p p e n d ( R e s . S t r i n g s . D i a l o g . W a r n i n g . T e x t 1 ) ;  
 	 	 	 b u i l d e r . A p p e n d ( " < b r / > " ) ;  
 	 	 	 b u i l d e r . A p p e n d ( " < b r / > " ) ;  
 	 	 	 f o r e a c h   (   s t r i n g   s   i n   w a r n i n g s   )  
 	 	 	 {  
 	 	 	 	 b u i l d e r . A p p e n d ( c h i p ) ;  
 	 	 	 	 b u i l d e r . A p p e n d ( s ) ;  
 	 	 	 	 b u i l d e r . A p p e n d ( " < b r / > " ) ;  
 	 	 	 }  
 	 	 	 b u i l d e r . A p p e n d ( " < b r / > " ) ;  
 	 	 	 b u i l d e r . A p p e n d ( R e s . S t r i n g s . D i a l o g . W a r n i n g . T e x t 2 ) ;  
 	 	 	 b u i l d e r . A p p e n d ( " < b r / > " ) ;  
 	 	 	 b u i l d e r . A p p e n d ( R e s . S t r i n g s . D i a l o g . W a r n i n g . T e x t 3 ) ;  
 	 	 	 b u i l d e r . A p p e n d ( " < b r / > " ) ;  
 	 	 	 s t r i n g   m e s s a g e   =   b u i l d e r . T o S t r i n g ( ) ;  
  
 	 	 	 C o m m o n . D i a l o g s . I D i a l o g   d i a l o g   =   C o m m o n . D i a l o g s . M e s s a g e D i a l o g . C r e a t e O k ( t i t l e ,   i c o n ,   m e s s a g e ,   " " ,   t h i s . c o m m a n d D i s p a t c h e r ) ;  
 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 r e t u r n   d i a l o g . R e s u l t ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   D i a l o g W a r n i n g R e d i r e c t i o n ( )  
 	 	 {  
 	 	 	 / / 	 A f f i c h e   l ' a v e r t i s s e m e n t   d e   c h a n g e m e n t   ' E x e m p l e s   o r i g i n a u x '   v e r s   ' M e s   e x e m p l e s ' .  
 	 	 	 s t r i n g   m e s s a g e   =   s t r i n g . F o r m a t ( R e s . S t r i n g s . D i a l o g . W a r n i n g . R e d i r e c t i o n ,   D o c u m e n t . O r i g i n a l S a m p l e s D i s p l a y N a m e ,   D o c u m e n t . M y S a m p l e s D i s p l a y N a m e ) ;     / /   T O D O :   m e t t r e   d a n s   l e s   r e s s o u r c e s   !  
 	 	 	 t h i s . D i a l o g E r r o r   ( m e s s a g e ,   e s c a p e E r r o r M e s s a g e :   f a l s e ) ;  
 	 	 }  
  
 	 	 p u b l i c   s t r i n g   S h o r t W i n d o w T i t l e  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   R e s . S t r i n g s . A p p l i c a t i o n . T i t l e S h o r t ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   C o m m o n . D i a l o g s . D i a l o g R e s u l t   D i a l o g E r r o r ( s t r i n g   e r r o r ,   b o o l   e s c a p e E r r o r M e s s a g e   =   t r u e )  
 	 	 {  
 	 	 	 / / 	 A f f i c h e   l e   d i a l o g u e   p o u r   s i g n a l e r   u n e   e r r e u r .  
 	 	 	 i f   (   t h i s . W i n d o w   = =   n u l l   )     r e t u r n   C o m m o n . D i a l o g s . D i a l o g R e s u l t . N o n e ;  
 	 	 	 i f   (   e r r o r   = =   " "   )     r e t u r n   C o m m o n . D i a l o g s . D i a l o g R e s u l t . N o n e ;  
  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 F o r m a t t e d T e x t   m e s s a g e   =   e s c a p e E r r o r M e s s a g e   ?   C o m m o n . D i a l o g s . H e l p e r s . M e s s a g e B u i l d e r . F o r m a t M e s s a g e   ( e r r o r )   :   n e w   F o r m a t t e d T e x t   ( e r r o r ) ;  
 	 	 	 r e t u r n   C o m m o n . D i a l o g s . M e s s a g e D i a l o g . S h o w E r r o r ( m e s s a g e ,   t h i s . W i n d o w ) ;  
 	 	 }  
  
 	 	 p u b l i c   C o m m o n . D i a l o g s . D i a l o g R e s u l t   D i a l o g Q u e s t i o n ( s t r i n g   m e s s a g e )  
 	 	 {  
 	 	 	 / / 	 A f f i c h e   l e   d i a l o g u e   p o u r   p o s e r   u n e   q u e s t i o n   o u i / n o n .  
 	 	 	 i f   (   t h i s . W i n d o w   = =   n u l l   )     r e t u r n   C o m m o n . D i a l o g s . D i a l o g R e s u l t . N o n e ;  
  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 s t r i n g   t i t l e   =   R e s . S t r i n g s . A p p l i c a t i o n . T i t l e S h o r t ;  
 	 	 	 s t r i n g   i c o n   =   " m a n i f e s t : E p s i t e c . C o m m o n . D i a l o g s . I m a g e s . Q u e s t i o n . i c o n " ;  
  
 	 	 	 C o m m o n . D i a l o g s . I D i a l o g   d i a l o g   =   C o m m o n . D i a l o g s . M e s s a g e D i a l o g . C r e a t e Y e s N o ( t i t l e ,   i c o n ,   m e s s a g e ,   " " ,   " " ,   t h i s . c o m m a n d D i s p a t c h e r ) ;  
 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 r e t u r n   d i a l o g . R e s u l t ;  
 	 	 }  
  
 	 	 p r o t e c t e d   s t a t i c   s t r i n g   A d j u s t F i l e n a m e ( s t r i n g   f i l e n a m e )  
 	 	 {  
 	 	 	 / / 	 S i   o n   a   t a p é   " t o t o " ,   m a i s   q u ' i l   e x i s t e   l e   f i c h i e r   " T o t o " ,  
 	 	 	 / / 	 m e t   l e   " v r a i "   n o m   d a n s   f i l e n a m e .  
 	 	 	 s t r i n g   p a t h   =   S y s t e m . I O . P a t h . G e t D i r e c t o r y N a m e ( f i l e n a m e ) ;  
 	 	 	 s t r i n g   n a m e   =   S y s t e m . I O . P a t h . G e t F i l e N a m e ( f i l e n a m e ) ;  
 	 	 	 s t r i n g [ ]   s ;  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 s   =   S y s t e m . I O . D i r e c t o r y . G e t F i l e s ( p a t h ,   n a m e ) ;  
 	 	 	 }  
 	 	 	 c a t c h  
 	 	 	 {  
 	 	 	 	 r e t u r n   f i l e n a m e ;  
 	 	 	 }  
 	 	 	 i f   (   s . L e n g t h   = =   1   )  
 	 	 	 {  
 	 	 	 	 f i l e n a m e   =   s [ 0 ] ;  
 	 	 	 }  
 	 	 	 r e t u r n   f i l e n a m e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   O p e n ( )  
 	 	 {  
 	 	 	 / / 	 D e m a n d e   u n   n o m   d e   f i c h i e r   p u i s   o u v r e   l e   f i c h i e r .  
 	 	 	 / / 	 A f f i c h e   l ' e r r e u r   é v e n t u e l l e .  
 	 	 	 / / 	 R e t o u r n e   f a l s e   s i   l e   f i c h i e r   n ' a   p a s   é t é   o u v e r t .  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 t h i s . d l g F i l e O p e n . T i t l e   =   ( t h i s . d o c u m e n t T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m )   ?   R e s . S t r i n g s . D i a l o g . O p e n . T i t l e P i c   :   R e s . S t r i n g s . D i a l o g . O p e n . T i t l e D o c ;  
  
 	 	 	 t h i s . d l g F i l e O p e n . I n i t i a l D i r e c t o r y   =   t h i s . g l o b a l S e t t i n g s . I n i t i a l D i r e c t o r y ;  
 	 	 	 t h i s . d l g F i l e O p e n . I n i t i a l F i l e N a m e   =   " " ;  
  
 	 	 	 t h i s . d l g F i l e O p e n . S h o w D i a l o g ( ) ;     / /   c h o i x   d ' u n   f i c h i e r . . .  
 	 	 	 i f   ( t h i s . d l g F i l e O p e n . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 t h i s . g l o b a l S e t t i n g s . I n i t i a l D i r e c t o r y   =   t h i s . d l g F i l e O p e n . I n i t i a l D i r e c t o r y ;  
  
 	 	 	 s t r i n g [ ]   n a m e s   =   t h i s . d l g F i l e O p e n . F i l e N a m e s ;  
 	 	 	 f o r   ( i n t   i = 0 ;   i < n a m e s . L e n g t h ;   i + + )  
 	 	 	 {  
 	 	 	 	 t h i s . O p e n ( n a m e s [ i ] ) ;  
 	 	 	 }  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   O p e n M o d e l ( )  
 	 	 {  
 	 	 	 / / 	 D e m a n d e   u n   n o m   d e   f i c h i e r   m o d è l e   p u i s   o u v r e   l e   f i c h i e r .  
 	 	 	 / / 	 A f f i c h e   l ' e r r e u r   é v e n t u e l l e .  
 	 	 	 / / 	 R e t o u r n e   f a l s e   s i   l e   f i c h i e r   n ' a   p a s   é t é   o u v e r t .  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 t h i s . d l g F i l e O p e n M o d e l . I n i t i a l D i r e c t o r y   =   t h i s . g l o b a l S e t t i n g s . N e w D o c u m e n t ;  
 	 	 	 t h i s . d l g F i l e O p e n M o d e l . I n i t i a l F i l e N a m e   =   " " ;  
  
 	 	 	 t h i s . d l g F i l e O p e n M o d e l . S h o w D i a l o g ( ) ;     / /   c h o i x   d ' u n   f i c h i e r . . .  
 	 	 	 i f   ( t h i s . d l g F i l e O p e n M o d e l . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 t h i s . g l o b a l S e t t i n g s . N e w D o c u m e n t   =   t h i s . d l g F i l e O p e n M o d e l . I n i t i a l D i r e c t o r y ;  
  
 	 	 	 s t r i n g [ ]   n a m e s   =   t h i s . d l g F i l e O p e n M o d e l . F i l e N a m e s ;  
 	 	 	 f o r   ( i n t   i = 0 ;   i < n a m e s . L e n g t h ;   i + + )  
 	 	 	 {  
 	 	 	 	 t h i s . O p e n ( n a m e s [ i ] ) ;  
 	 	 	 }  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p u b l i c   b o o l   O p e n ( s t r i n g   f i l e n a m e )  
 	 	 {  
 	 	 	 / / 	 O u v r e   u n   f i c h e r   d ' a p r è s   s o n   n o m .  
 	 	 	 / / 	 A f f i c h e   l ' e r r e u r   é v e n t u e l l e .  
 	 	 	 / / 	 R e t o u r n e   f a l s e   s i   l e   f i c h i e r   n ' a   p a s   é t é   o u v e r t .  
 	 	 	 t h i s . M o u s e S h o w W a i t ( ) ;  
  
 	 	 	 s t r i n g   e r r   =   " " ;  
 	 	 	 i f   (   M i s c . I s E x t e n s i o n ( f i l e n a m e ,   " . c r c o l o r s " )   )  
 	 	 	 {  
 	 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C r e a t e D o c u m e n t ( t h i s . W i n d o w ) ;  
 	 	 	 	 }  
 	 	 	 	 e r r   =   t h i s . P a l e t t e R e a d ( f i l e n a m e ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 / / 	 C h e r c h e   s i   c e   n o m   d e   f i c h i e r   e s t   d é j à   o u v e r t   ?  
 	 	 	 	 i n t   t o t a l   =   t h i s . b o o k D o c u m e n t s . P a g e C o u n t ;  
 	 	 	 	 f o r   (   i n t   i = 0   ;   i < t o t a l   ;   i + +   )  
 	 	 	 	 {  
 	 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . d o c u m e n t s [ i ] ;  
 	 	 	 	 	 i f   (   d i . d o c u m e n t . F i l e n a m e   = =   f i l e n a m e   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 i f   ( M i s c . I s E x t e n s i o n ( f i l e n a m e ,   " . c r m o d " ) )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . g l o b a l S e t t i n g s . L a s t M o d e l A d d ( f i l e n a m e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . g l o b a l S e t t i n g s . L a s t F i l e n a m e A d d ( f i l e n a m e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 t h i s . U s e D o c u m e n t ( i ) ;  
 	 	 	 	 	 	 t h i s . M o u s e H i d e W a i t ( ) ;  
 	 	 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 f i l e n a m e   =   D o c u m e n t E d i t o r . A d j u s t F i l e n a m e ( f i l e n a m e ) ;  
  
 	 	 	 	 i f   (   ! t h i s . I s R e c y c l a b l e D o c u m e n t ( )   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C r e a t e D o c u m e n t   ( t h i s . W i n d o w ) ;  
 	 	 	 	 }  
 	 	 	 	 e r r   =   t h i s . C u r r e n t D o c u m e n t . R e a d ( f i l e n a m e ) ;  
 	 	 	 	 t h i s . i n i t i a l i z a t i o n I n P r o g r e s s   =   t r u e ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . I n i t i a l i z a t i o n I n P r o g r e s s   =   t r u e ;  
 	 	 	 	 t h i s . U p d a t e A f t e r R e a d ( ) ;  
 	 	 	 	 t h i s . U p d a t e R u l e r s ( ) ;  
 	 	 	 	 i f   (   e r r   = =   " "   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . U p d a t e B o o k D o c u m e n t s ( ) ;  
 	 	 	 	 	 t h i s . D i a l o g W a r n i n g s ( t h i s . C u r r e n t D o c u m e n t . R e a d W a r n i n g s ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 / / ? t h i s . M o u s e H i d e W a i t ( ) ;  
 	 	 	 t h i s . D i a l o g E r r o r ( e r r ) ;  
 	 	 	 r e t u r n   ( e r r   = =   " " ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   S a v e ( b o o l   a s k )  
 	 	 {  
 	 	 	 / / 	 D e m a n d e   u n   n o m   d e   f i c h i e r   p u i s   e n r e g i s t r e   l e   f i c h i e r .  
 	 	 	 / / 	 S i   l e   d o c u m e n t   a   d é j à   u n   n o m   d e   f i c h i e r   e t   q u e   a s k = f a l s e ,  
 	 	 	 / / 	 l ' e n r e g i s t r e m e n t   e s t   f a i t   d i r e c t e m e n t   a v e c   l e   n o m   c o n n u .  
 	 	 	 / / 	 A f f i c h e   l ' e r r e u r   é v e n t u e l l e .  
 	 	 	 / / 	 R e t o u r n e   f a l s e   s i   l e   f i c h i e r   n ' a   p a s   é t é   e n r e g i s t r é .  
 	 	 	 s t r i n g   f i l e n a m e   =   t h i s . C u r r e n t D o c u m e n t . F i l e n a m e ;  
 	 	 	 i f   ( D o c u m e n t . R e d i r e c t P a t h   ( r e f   f i l e n a m e ) )  
 	 	 	 {  
 	 	 	 	 t h i s . D i a l o g W a r n i n g R e d i r e c t i o n ( ) ;  
 	 	 	 	 a s k   =   t r u e ;  
 	 	 	 }  
  
 	 	 	 i f   ( f i l e n a m e   = =   " "   | |   a s k )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 	 t h i s . d l g F i l e S a v e . T i t l e   =   ( t h i s . d o c u m e n t T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m )   ?   R e s . S t r i n g s . D i a l o g . S a v e . T i t l e P i c   :   R e s . S t r i n g s . D i a l o g . S a v e . T i t l e D o c ;  
  
 	 	 	 	 i f   ( f i l e n a m e   = =   " " )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . d l g F i l e S a v e . I n i t i a l D i r e c t o r y   =   t h i s . g l o b a l S e t t i n g s . I n i t i a l D i r e c t o r y ;  
 	 	 	 	 	 t h i s . d l g F i l e S a v e . I n i t i a l F i l e N a m e   =   " " ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . d l g F i l e S a v e . I n i t i a l D i r e c t o r y   =   S y s t e m . I O . P a t h . G e t D i r e c t o r y N a m e ( f i l e n a m e ) ;  
 	 	 	 	 	 t h i s . d l g F i l e S a v e . I n i t i a l F i l e N a m e   =   f i l e n a m e ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . d l g F i l e S a v e . I s D i r e c t o r y R e d i r e c t e d   & &   t h i s . d l g F i l e S a v e . I n i t i a l F i l e N a m e   ! =   " " )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . D i a l o g W a r n i n g R e d i r e c t i o n ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . d l g F i l e S a v e . S h o w D i a l o g ( ) ;     / /   c h o i x   d ' u n   f i c h i e r . . .  
 	 	 	 	 i f   ( t h i s . d l g F i l e S a v e . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 }  
  
 	 	 	 	 f i l e n a m e   =   t h i s . d l g F i l e S a v e . F i l e N a m e ;  
 	 	 	 	 f i l e n a m e   =   D o c u m e n t E d i t o r . A d j u s t F i l e n a m e ( f i l e n a m e ) ;  
 	 	 	 }  
  
 	 	 	 i f   ( D o c u m e n t . R e d i r e c t P a t h   ( r e f   f i l e n a m e ) )  
 	 	 	 {  
 	 	 	 	 t h i s . D i a l o g W a r n i n g R e d i r e c t i o n ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . M o u s e S h o w W a i t ( ) ;  
 	 	 	 s t r i n g   e r r   =   t h i s . C u r r e n t D o c u m e n t . W r i t e ( f i l e n a m e ) ;  
 	 	 	 i f   (   e r r   = =   " "   )  
 	 	 	 {  
 	 	 	 	 t h i s . g l o b a l S e t t i n g s . I n i t i a l D i r e c t o r y   =   S y s t e m . I O . P a t h . G e t D i r e c t o r y N a m e ( f i l e n a m e ) ;  
 	 	 	 	 t h i s . g l o b a l S e t t i n g s . L a s t F i l e n a m e A d d ( f i l e n a m e ) ;  
 	 	 	 }  
 	 	 	 t h i s . M o u s e H i d e W a i t ( ) ;  
 	 	 	 t h i s . D i a l o g E r r o r ( e r r ) ;  
 	 	 	 r e t u r n   ( e r r   = =   " " ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   S a v e M o d e l ( )  
 	 	 {  
 	 	 	 / / 	 D e m a n d e   u n   n o m   d e   f i c h i e r   m o d è l e   p u i s   e n r e g i s t r e   l e   f i c h i e r .  
 	 	 	 / / 	 R e t o u r n e   f a l s e   s i   l e   f i c h i e r   n ' a   p a s   é t é   e n r e g i s t r é .  
 	 	 	 s t r i n g   f i l e n a m e ;  
  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 s t r i n g   n e w D o c u m e n t   =   t h i s . g l o b a l S e t t i n g s . N e w D o c u m e n t ;  
  
 	 	 	 i f   ( M i s c . I s E x t e n s i o n ( n e w D o c u m e n t ,   " . c r m o d " ) )     / /   a n c i e n n e   d é f i n i t i o n   ?  
 	 	 	 {  
 	 	 	 	 n e w D o c u m e n t   =   S y s t e m . I O . P a t h . G e t D i r e c t o r y N a m e ( n e w D o c u m e n t ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . d l g F i l e S a v e M o d e l . I n i t i a l D i r e c t o r y   =   n e w D o c u m e n t ;  
 	 	 	 t h i s . d l g F i l e S a v e M o d e l . I n i t i a l F i l e N a m e   =   " " ;  
  
 	 	 	 t h i s . d l g F i l e S a v e M o d e l . S h o w D i a l o g ( ) ;     / /   c h o i x   d ' u n   f i c h i e r . . .  
 	 	 	 i f   ( t h i s . d l g F i l e S a v e M o d e l . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 f i l e n a m e   =   t h i s . d l g F i l e S a v e M o d e l . F i l e N a m e ;  
 	 	 	 f i l e n a m e   =   D o c u m e n t E d i t o r . A d j u s t F i l e n a m e ( f i l e n a m e ) ;  
  
 	 	 	 i f   ( D o c u m e n t . R e d i r e c t P a t h   ( r e f   f i l e n a m e ) )  
 	 	 	 {  
 	 	 	 	 t h i s . D i a l o g W a r n i n g R e d i r e c t i o n ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . M o u s e S h o w W a i t ( ) ;  
 	 	 	 s t r i n g   e r r   =   t h i s . C u r r e n t D o c u m e n t . W r i t e ( f i l e n a m e ) ;  
 	 	 	 i f   (   e r r   = =   " "   )  
 	 	 	 {  
 	 	 	 	 t h i s . g l o b a l S e t t i n g s . N e w D o c u m e n t   =   t h i s . d l g F i l e S a v e M o d e l . I n i t i a l D i r e c t o r y ;  
 	 	 	 	 t h i s . g l o b a l S e t t i n g s . L a s t M o d e l A d d ( f i l e n a m e ) ;  
 	 	 	 }  
 	 	 	 t h i s . M o u s e H i d e W a i t ( ) ;  
 	 	 	 t h i s . D i a l o g E r r o r ( e r r ) ;  
 	 	 	 r e t u r n   ( e r r   = =   " " ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   A u t o S a v e ( )  
 	 	 {  
 	 	 	 / / 	 F a i t   t o u t   c e   q u ' i l   f a u t   p o u r   é v e n t u e l l e m e n t   s a u v e g a r d e r   l e   d o c u m e n t  
 	 	 	 / / 	 a v a n t   d e   p a s s e r   à   a u t r e   c h o s e .  
 	 	 	 / / 	 R e t o u r n e   f a l s e   s i   o n   n e   p e u t   p a s   c o n t i n u e r .  
 	 	 	 C o m m o n . D i a l o g s . D i a l o g R e s u l t   r e s u l t   =   t h i s . D i a l o g S a v e ( ) ;  
 	 	 	 i f   (   r e s u l t   = =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . Y e s   )  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . S a v e ( f a l s e ) ;  
 	 	 	 }  
 	 	 	 i f   (   r e s u l t   = =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . C a n c e l   )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   A u t o S a v e A l l ( )  
 	 	 {  
 	 	 	 / / 	 F a i t   t o u t   c e   q u ' i l   f a u t   p o u r   é v e n t u e l l e m e n t   s a u v e g a r d e r   t o u s   l e s  
 	 	 	 / / 	 d o c u m e n t s   a v a n t   d e   p a s s e r   à   a u t r e   c h o s e .  
 	 	 	 / / 	 R e t o u r n e   f a l s e   s i   o n   n e   p e u t   p a s   c o n t i n u e r .  
 	 	 	 i n t   c d   =   t h i s . c u r r e n t D o c u m e n t ;  
  
 	 	 	 i n t   t o t a l   =   t h i s . b o o k D o c u m e n t s . P a g e C o u n t ;  
 	 	 	 f o r   (   i n t   i = 0   ;   i < t o t a l   ;   i + +   )  
 	 	 	 {  
 	 	 	 	 t h i s . c u r r e n t D o c u m e n t   =   i ;  
 	 	 	 	 i f   (   ! t h i s . A u t o S a v e ( )   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . c u r r e n t D o c u m e n t   =   c d ;  
 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . c u r r e n t D o c u m e n t   =   c d ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   F o r c e S a v e A l l ( )  
 	 	 {  
 	 	 	 / / 	 S a u v e g a r d e   t o u s   l e s   d o c u m e n t s ,   m ê m e   c e u x   q u i   s o n t   à   j o u r .  
 	 	 	 i n t   c d   =   t h i s . c u r r e n t D o c u m e n t ;  
  
 	 	 	 i n t   t o t a l   =   t h i s . b o o k D o c u m e n t s . P a g e C o u n t ;  
 	 	 	 f o r   (   i n t   i = 0   ;   i < t o t a l   ;   i + +   )  
 	 	 	 {  
 	 	 	 	 t h i s . c u r r e n t D o c u m e n t   =   i ;  
 	 	 	 	 t h i s . S a v e ( f a l s e ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . c u r r e n t D o c u m e n t   =   c d ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   O v e r w r i t e A l l ( )  
 	 	 {  
 	 	 	 / / 	 O u v r e ,   r é é c r i t   e t   f e r m e   p l u s i e u r s   f i c h i e r s .  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 C o m m o n . D i a l o g s . F i l e O p e n D i a l o g   d i a l o g   =   n e w   C o m m o n . D i a l o g s . F i l e O p e n D i a l o g ( ) ;  
  
 	 	 	 d i a l o g . I n i t i a l D i r e c t o r y   =   t h i s . g l o b a l S e t t i n g s . I n i t i a l D i r e c t o r y ;  
 	 	 	 d i a l o g . F i l e N a m e   =   " " ;  
 	 	 	 i f   ( t h i s . d o c u m e n t T y p e   = =   D o c u m e n t T y p e . G r a p h i c )  
 	 	 	 {  
 	 	 	 	 d i a l o g . T i t l e   =   R e s . S t r i n g s . D i a l o g . O p e n . T i t l e D o c ;  
 	 	 	 	 d i a l o g . F i l t e r s . A d d ( " c r d o c " ,   R e s . S t r i n g s . D i a l o g . F i l e D o c ,   " * . c r d o c " ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 d i a l o g . T i t l e   =   R e s . S t r i n g s . D i a l o g . O p e n . T i t l e P i c ;  
 	 	 	 	 d i a l o g . F i l t e r s . A d d ( " i c o n " ,   R e s . S t r i n g s . D i a l o g . F i l e P i c ,   " * . i c o n " ) ;  
 	 	 	 }  
 	 	 	 d i a l o g . A c c e p t M u l t i p l e S e l e c t i o n   =   t r u e ;  
 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 i f   ( d i a l o g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 s t r i n g [ ]   n a m e s   =   d i a l o g . F i l e N a m e s ;  
 	 	 	 f o r   ( i n t   i = 0 ;   i < n a m e s . L e n g t h ;   i + + )  
 	 	 	 {  
 	 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . W r i t e L i n e ( s t r i n g . F o r m a t ( " O p e n   { 0 } " ,   n a m e s [ i ] ) ) ;  
 	 	 	 	 s t r i n g   e r r   =   t h i s . C u r r e n t D o c u m e n t . R e a d ( n a m e s [ i ] ) ;  
 	 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y ( e r r ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . W r i t e ( n a m e s [ i ] ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
 	 	 # e n d r e g i o n  
  
 	 	 [ C o m m a n d   ( " N e w " ) ]  
 	 	 v o i d   C o m m a n d N e w ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 i f   ( t h i s . d o c u m e n t T y p e   = =   D o c u m e n t T y p e . G r a p h i c )     / /   C r D o c   ?  
 	 	 	 {  
 	 	 	 	 s t r i n g   n e w D o c u m e n t   =   t h i s . g l o b a l S e t t i n g s . N e w D o c u m e n t ;  
  
 	 	 	 	 i f   ( M i s c . I s E x t e n s i o n ( n e w D o c u m e n t ,   " . c r m o d " ) )     / /   a n c i e n n e   d é f i n i t i o n   ?  
 	 	 	 	 {  
 	 	 	 	 	 n e w D o c u m e n t   =   S y s t e m . I O . P a t h . G e t D i r e c t o r y N a m e ( n e w D o c u m e n t ) ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . d l g F i l e N e w . I n i t i a l D i r e c t o r y   =   n e w D o c u m e n t ;  
  
 	 	 	 	 t h i s . d l g F i l e N e w . S h o w D i a l o g ( ) ;     / /   c h o i x   d ' u n   f i c h i e r . . .  
 	 	 	 	 i f   ( t h i s . d l g F i l e N e w . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . g l o b a l S e t t i n g s . N e w D o c u m e n t   =   t h i s . d l g F i l e N e w . I n i t i a l D i r e c t o r y ;  
  
 	 	 	 	 s t r i n g   f i l e n a m e   =   t h i s . d l g F i l e N e w . F i l e N a m e ;  
 	 	 	 	 i f   ( f i l e n a m e   = =   E p s i t e c . C o m m o n . D i a l o g s . A b s t r a c t F i l e D i a l o g . N e w E m p t y D o c u m e n t )     / /   n o u v e a u   d o c u m e n t   v i d e   ?  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . C r e a t e D o c u m e n t ( t h i s . W i n d o w ) ;  
 	 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . N e w ( ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . O p e n ( f i l e n a m e ) ;  
 	 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . C l e a r D i r t y S e r i a l i z e ( ) ;  
 	 	 	 	 }  
 	 	 	 	 t h i s . g l o b a l S e t t i n g s . L a s t M o d e l A d d ( f i l e n a m e ) ;  
 	 	 	 	 t h i s . i n i t i a l i z a t i o n I n P r o g r e s s   =   t r u e ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . I n i t i a l i z a t i o n I n P r o g r e s s   =   t r u e ;  
 	 	 	 }  
 	 	 	 e l s e     / /   C r P i c t o   ?  
 	 	 	 {  
 	 	 	 	 t h i s . C r e a t e D o c u m e n t ( t h i s . W i n d o w ) ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . N e w ( ) ;  
 	 	 	 	 t h i s . i n i t i a l i z a t i o n I n P r o g r e s s   =   t r u e ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . I n i t i a l i z a t i o n I n P r o g r e s s   =   t r u e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " O p e n " ) ]  
 	 	 v o i d   C o m m a n d O p e n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . O p e n ( ) ;  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . F o c u s ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " O p e n M o d e l " ) ]  
 	 	 v o i d   C o m m a n d O p e n M o d e l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . O p e n M o d e l ( ) ;  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . F o c u s ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S a v e " ) ]  
 	 	 v o i d   C o m m a n d S a v e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . S a v e ( f a l s e ) ;  
 	 	 }  
 	 	  
 	 	 [ C o m m a n d   ( " S a v e A s " ) ]  
 	 	 v o i d   C o m m a n d S a v e A s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . S a v e ( t r u e ) ;  
 	 	 }  
 	 	  
 	 	 [ C o m m a n d   ( " S a v e M o d e l " ) ]  
 	 	 v o i d   C o m m a n d S a v e M o d e l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . S a v e M o d e l ( ) ;  
 	 	 }  
 	 	  
 	 	 [ C o m m a n d   ( " C l o s e " ) ]  
 	 	 v o i d   C o m m a n d C l o s e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i f   (   ! t h i s . A u t o S a v e ( )   )     r e t u r n ;  
 	 	 	 t h i s . C l o s e D o c u m e n t ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C l o s e A l l " ) ]  
 	 	 v o i d   C o m m a n d C l o s e A l l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i f   (   ! t h i s . A u t o S a v e A l l ( )   )     r e t u r n ;  
 	 	 	 w h i l e   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 t h i s . C l o s e D o c u m e n t ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d ( " F o r c e S a v e A l l " ) ]  
 	 	 v o i d   C o m m a n d F o r c e S a v e A l l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . F o r c e S a v e A l l ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " O v e r w r i t e A l l " ) ]  
 	 	 v o i d   C o m m a n d O v e r w r i t e A l l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . O v e r w r i t e A l l ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " N e x t D o c u m e n t " ) ]  
 	 	 v o i d   C o m m a n d N e x t D o c u m e n t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i n t   d o c   =   t h i s . c u r r e n t D o c u m e n t + 1 ;  
 	 	 	 i f   (   d o c   > =   t h i s . b o o k D o c u m e n t s . P a g e C o u n t   )     d o c   =   0 ;  
 	 	 	 t h i s . U s e D o c u m e n t ( d o c ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P r e v D o c u m e n t " ) ]  
 	 	 v o i d   C o m m a n d P r e v D o c u m e n t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i n t   d o c   =   t h i s . c u r r e n t D o c u m e n t - 1 ;  
 	 	 	 i f   (   d o c   <   0   )     d o c   =   t h i s . b o o k D o c u m e n t s . P a g e C o u n t - 1 ;  
 	 	 	 t h i s . U s e D o c u m e n t ( d o c ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " L a s t F i l e " ) ]  
 	 	 v o i d   C o m m a n d L a s t F i l e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 s t r i n g   v a l u e   =   e . C o m m a n d S t a t e . A d v a n c e d S t a t e ;  
 	 	 	 t h i s . O p e n ( v a l u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " L a s t M o d e l " ) ]  
 	 	 v o i d   C o m m a n d L a s t M o d e l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 s t r i n g   v a l u e   =   e . C o m m a n d S t a t e . A d v a n c e d S t a t e ;  
 	 	 	 i f   ( v a l u e   = =   E p s i t e c . C o m m o n . D i a l o g s . A b s t r a c t F i l e D i a l o g . N e w E m p t y D o c u m e n t )     / /   n o u v e a u   d o c u m e n t   v i d e   ?  
 	 	 	 {  
 	 	 	 	 t h i s . C r e a t e D o c u m e n t ( t h i s . W i n d o w ) ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . N e w ( ) ;  
 	 	 	 	 t h i s . g l o b a l S e t t i n g s . L a s t M o d e l A d d ( v a l u e ) ;  
 	 	 	 	 t h i s . i n i t i a l i z a t i o n I n P r o g r e s s   =   t r u e ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . I n i t i a l i z a t i o n I n P r o g r e s s   =   t r u e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . O p e n ( v a l u e ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d ( " Q u i t A p p l i c a t i o n " ) ]  
 	 	 [ C o m m a n d ( A p p l i c a t i o n C o m m a n d s . I d . Q u i t ) ]  
 	 	 v o i d   C o m m a n d Q u i t A p p l i c a t i o n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i f   (   ! t h i s . A u t o S a v e A l l ( )   )     r e t u r n ;  
 	 	 	 t h i s . Q u i t A p p l i c a t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P r i n t " ) ]  
 	 	 v o i d   C o m m a n d P r i n t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 D o c u m e n t   d o c u m e n t   =   t h i s . C u r r e n t D o c u m e n t ;  
 	 	 	 C o m m o n . D i a l o g s . P r i n t D i a l o g   d i a l o g   =   d o c u m e n t . P r i n t D i a l o g ;  
 	 	 	 d i a l o g . D o c u m e n t . D o c u m e n t N a m e   =   C o m m o n . S u p p o r t . U t i l i t i e s . X m l T o T e x t ( C o m m o n . D o c u m e n t . M i s c . F u l l N a m e ( d o c u m e n t . F i l e n a m e ,   f a l s e ) ) ;  
 	 	 	 d i a l o g . O w n e r   =   t h i s . W i n d o w ;  
  
 	 	 	 t h i s . d l g P r i n t . S h o w ( ) ;  
 	 	 }  
 	 	  
 	 	 [ C o m m a n d   ( " E x p o r t " ) ]  
 	 	 v o i d   C o m m a n d E x p o r t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 / / 	 C h o i x   d u   t y p e   d e   f i c h i e r   à   e x p o r t e r .  
 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y ( t h i s . C u r r e n t D o c u m e n t . E x p o r t F i l e n a m e ) )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g E x p o r t T y p e . F i l e T y p e   =   ( t h i s . d o c u m e n t T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m )   ?   " . p n g "   :   " . p d f " ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d l g E x p o r t T y p e . F i l e T y p e   =   S y s t e m . I O . P a t h . G e t E x t e n s i o n ( t h i s . C u r r e n t D o c u m e n t . E x p o r t F i l e n a m e ) . T o L o w e r I n v a r i a n t ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . d l g E x p o r t T y p e . S h o w   ( ) ;     / /   c h o i x   d u   t y p e   d e   f i c h i e r . . .  
 	 	 	 i f   ( ! t h i s . d l g E x p o r t T y p e . I s O K c l i c k e d )     / /   a n n u l e r   ?  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 / / 	 S i   l e   n o m   d u   f i c h i e r   d u   d o c u m e n t   à   e x p o r t e r   n ' e x i s t e   p a s ,   o n   p r e n d   c e l u i   d u  
 	 	 	 / / 	 d o c u m e n t   e n   c o u r s ,   c e   q u i   r e n d   s e r v i c e   l a   p l u p a r t   d u   t e m p s .  
 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y   ( t h i s . C u r r e n t D o c u m e n t . E x p o r t F i l e n a m e )   & &  
 	 	 	 	 ! s t r i n g . I s N u l l O r E m p t y   ( t h i s . C u r r e n t D o c u m e n t . F i l e n a m e )   & &  
 	 	 	 	 ! s t r i n g . I s N u l l O r E m p t y   ( t h i s . d l g E x p o r t T y p e . F i l e T y p e ) )  
 	 	 	 {  
 	 	 	 	 v a r   d   =   S y s t e m . I O . P a t h . G e t D i r e c t o r y N a m e   ( t h i s . C u r r e n t D o c u m e n t . F i l e n a m e ) ;  
 	 	 	 	 v a r   n   =   S y s t e m . I O . P a t h . G e t F i l e N a m e W i t h o u t E x t e n s i o n   ( t h i s . C u r r e n t D o c u m e n t . F i l e n a m e ) ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . E x p o r t F i l e n a m e   =   S y s t e m . I O . P a t h . C o m b i n e   ( d ,   n + t h i s . d l g E x p o r t T y p e . F i l e T y p e ) ;  
 	 	 	 }  
  
 	 	 	 / / 	 C h o i x   d u   f i c h i e r .  
 	 	 	 i f   ( t h i s . d l g E x p o r t T y p e . F i l e T y p e   = =   " . p d f " )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g F i l e E x p o r t . T i t l e   =   R e s . S t r i n g s . D i a l o g . E x p o r t . T i t l e 1 ;  
 	 	 	 	 t h i s . d l g F i l e E x p o r t . F i l t e r s . C l e a r   ( ) ;  
 	 	 	 	 t h i s . d l g F i l e E x p o r t . F i l t e r s . A d d   ( n e w   E p s i t e c . C o m m o n . D i a l o g s . F i l t e r I t e m   ( " x " ,   R e s . S t r i n g s . D i a l o g . F i l e . D o c u m e n t ,   t h i s . d l g E x p o r t T y p e . F i l e T y p e ) ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d l g F i l e E x p o r t . T i t l e   =   R e s . S t r i n g s . D i a l o g . E x p o r t . T i t l e ;  
 	 	 	 	 t h i s . d l g F i l e E x p o r t . F i l t e r s . C l e a r   ( ) ;  
 	 	 	 	 t h i s . d l g F i l e E x p o r t . F i l t e r s . A d d   ( n e w   E p s i t e c . C o m m o n . D i a l o g s . F i l t e r I t e m   ( " x " ,   R e s . S t r i n g s . D i a l o g . F i l e . I m a g e ,   t h i s . d l g E x p o r t T y p e . F i l e T y p e ) ) ;  
 	 	 	 }  
  
 	 	 	 i f   (   t h i s . C u r r e n t D o c u m e n t . E x p o r t D i r e c t o r y   = =   " "   )  
 	 	 	 {  
 	 	 	 	 i f   (   t h i s . C u r r e n t D o c u m e n t . F i l e n a m e   = =   " "   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . d l g F i l e E x p o r t . I n i t i a l D i r e c t o r y   =   t h i s . g l o b a l S e t t i n g s . I n i t i a l D i r e c t o r y ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . d l g F i l e E x p o r t . I n i t i a l D i r e c t o r y   =   S y s t e m . I O . P a t h . G e t D i r e c t o r y N a m e ( t h i s . C u r r e n t D o c u m e n t . F i l e n a m e ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d l g F i l e E x p o r t . I n i t i a l D i r e c t o r y   =   t h i s . C u r r e n t D o c u m e n t . E x p o r t D i r e c t o r y ;  
 	 	 	 }  
  
 	 	 	 t h i s . d l g F i l e E x p o r t . I n i t i a l F i l e N a m e   =   t h i s . C u r r e n t D o c u m e n t . E x p o r t F i l e n a m e ;  
  
 	 	 	 t h i s . d l g F i l e E x p o r t . S h o w D i a l o g ( ) ;     / /   c h o i x   d ' u n   f i c h i e r . . .  
 	 	 	 i f   ( t h i s . d l g F i l e E x p o r t . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . E x p o r t D i r e c t o r y   =   t h i s . d l g F i l e E x p o r t . I n i t i a l D i r e c t o r y ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . E x p o r t F i l e n a m e   =   t h i s . d l g F i l e E x p o r t . F i l e N a m e ;  
  
 	 	 	 / / 	 C h o i x   d e s   o p t i o n s   d ' e x p o r t a t i o n .  
 	 	 	 i f   ( t h i s . d l g E x p o r t T y p e . F i l e T y p e   = =   " . p d f " )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g E x p o r t P D F . S h o w ( t h i s . C u r r e n t D o c u m e n t . E x p o r t F i l e n a m e ) ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( t h i s . d l g E x p o r t T y p e . F i l e T y p e   = =   " . i c o " )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g E x p o r t I C O . S h o w ( t h i s . C u r r e n t D o c u m e n t . E x p o r t F i l e n a m e ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e F o r m a t   =   P r i n t e r . G e t I m a g e F o r m a t ( t h i s . d l g E x p o r t T y p e . F i l e T y p e ) ;  
 	 	 	 	 t h i s . d l g E x p o r t . S h o w ( t h i s . C u r r e n t D o c u m e n t . E x p o r t F i l e n a m e ) ;  
 	 	 	 }  
 	 	 }  
  
                 [ C o m m a n d ( " Q u i c k E x p o r t " ) ]  
                 v o i d   C o m m a n d Q u i c k E x p o r t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
                 {  
                         i f   ( s t r i n g . I s N u l l O r E m p t y ( t h i s . C u r r e n t D o c u m e n t . F i l e n a m e ) )  
                         {  
                                 r e t u r n ;  
                         }  
  
                         v a r   d   =   S y s t e m . I O . P a t h . G e t D i r e c t o r y N a m e ( t h i s . C u r r e n t D o c u m e n t . F i l e n a m e ) ;  
                         v a r   n   =   S y s t e m . I O . P a t h . G e t F i l e N a m e W i t h o u t E x t e n s i o n ( t h i s . C u r r e n t D o c u m e n t . F i l e n a m e ) ;  
  
 	 	 	 s t r i n g   e x t   =   " . x x x " ;  
 	 	 	 v a r   c o m p r e s s i o n   =   I m a g e C o m p r e s s i o n . N o n e ;  
 	 	 	 i n t   d e p t h   =   2 4 ;  
  
 	 	 	 s w i t c h   ( t h i s . g l o b a l S e t t i n g s . Q u i c k E x p o r t F o r m a t )  
 	 	 	 {  
 	 	 	 	 c a s e   I m a g e F o r m a t . J p e g :  
 	 	 	 	 	 e x t   =   " . j p g " ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 	 c a s e   I m a g e F o r m a t . G i f :  
 	 	 	 	 	 e x t   =   " . g i f " ;  
 	 	 	 	 	 d e p t h   =   8 ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 	 c a s e   I m a g e F o r m a t . T i f f :  
 	 	 	 	 	 e x t   =   " . t i f " ;  
 	 	 	 	 	 c o m p r e s s i o n   =   I m a g e C o m p r e s s i o n . L z w ;  
 	 	 	 	 	 d e p t h   =   3 2 ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 	 c a s e   I m a g e F o r m a t . P n g :  
 	 	 	 	 	 e x t   =   " . p n g " ;  
 	 	 	 	 	 c o m p r e s s i o n   =   I m a g e C o m p r e s s i o n . L z w ;  
 	 	 	 	 	 d e p t h   =   3 2 ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 	 c a s e   I m a g e F o r m a t . B m p :  
 	 	 	 	 	 e x t   =   " . b m p " ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 }  
                         s t r i n g   f i l e n a m e   =   S y s t e m . I O . P a t h . C o m b i n e ( d ,   n   +   e x t ) ;  
  
                         i f   ( ! s t r i n g . I s N u l l O r E m p t y ( f i l e n a m e ) )  
                         {  
                                 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e O n l y S e l e c t e d   =   f a l s e ;  
                                 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e C r o p   =   E x p o r t I m a g e C r o p . P a g e ;  
                                 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e F o r m a t   =   t h i s . g l o b a l S e t t i n g s . Q u i c k E x p o r t F o r m a t ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e D p i   =   t h i s . g l o b a l S e t t i n g s . Q u i c k E x p o r t D p i ;  
                                 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e C o m p r e s s i o n   =   c o m p r e s s i o n ;  
                                 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e D e p t h   =   d e p t h ;  
                                 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e Q u a l i t y   =   1 . 0 ;  
                                 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e A A   =   1 . 0 ;  
                                 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e A l p h a C o r r e c t   =   t r u e ;  
                                 t h i s . C u r r e n t D o c u m e n t . P r i n t e r . I m a g e A l p h a P r e m u l t i p l i e d   =   t r u e ;  
  
                                 s t r i n g   e r r   =   t h i s . C u r r e n t D o c u m e n t . E x p o r t ( f i l e n a m e ) ;  
                         }  
                 }  
  
                 [ C o m m a n d ( " G l y p h s " ) ]  
 	 	 v o i d   C o m m a n d G l y p h s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 i f   (   t h i s . g l y p h s S t a t e . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o   )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g G l y p h s . S h o w ( ) ;  
 	 	 	 	 t h i s . g l y p h s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d l g G l y p h s . H i d e ( ) ;  
 	 	 	 	 t h i s . g l y p h s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " R e p l a c e " ) ]  
 	 	 v o i d   C o m m a n d R e p l a c e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 t h i s . d l g R e p l a c e . S h o w ( ) ;  
 	 	 	 t h i s . r e p l a c e S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " F i n d N e x t " ) ]  
 	 	 [ C o m m a n d   ( " F i n d P r e v " ) ]  
 	 	 [ C o m m a n d   ( " F i n d D e f N e x t " ) ]  
 	 	 [ C o m m a n d   ( " F i n d D e f P r e v " ) ]  
 	 	 v o i d   C o m m a n d F i n d ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	  
 	 	 	 s t r i n g   c o m m a n d N a m e   =   e . C o m m a n d . C o m m a n d I d ;  
  
 	 	 	 b o o l   i s P r e v   =   ( c o m m a n d N a m e   = =   " F i n d P r e v "   | |   c o m m a n d N a m e   = =   " F i n d D e f P r e v " ) ;  
 	 	 	 b o o l   i s D e f   =   ( c o m m a n d N a m e   = =   " F i n d D e f N e x t "   | |   c o m m a n d N a m e   = =   " F i n d D e f P r e v " ) ;  
  
 	 	 	 i f   (   i s D e f   )     / /   C t r l - F 3   ?  
 	 	 	 {  
 	 	 	 	 s t r i n g   w o r d   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . G e t S e l e c t e d W o r d ( ) ;     / /   m o t   a c t u e l l e m e n t   s é l e c t i o n n é  
 	 	 	 	 i f   (   w o r d   = =   n u l l   )     r e t u r n ;  
 	 	 	 	 t h i s . d l g R e p l a c e . F i n d T e x t   =   w o r d ;     / /   i l   d e v i e n t   l e   c r i t è r e   d e   r e c h e r c h e  
 	 	 	 }  
  
 	 	 	 M i s c . S t r i n g S e a r c h   m o d e   =   t h i s . d l g R e p l a c e . M o d e ;  
 	 	 	 m o d e   & =   ~ M i s c . S t r i n g S e a r c h . E n d T o S t a r t ;  
 	 	 	 i f   (   i s P r e v   )     m o d e   | =   M i s c . S t r i n g S e a r c h . E n d T o S t a r t ;     / /   S h i f t - F 3   ?  
 	 	 	 t h i s . d l g R e p l a c e . M o d e   =   m o d e ;     / /   m o d i f i e   j u s t e   l a   d i r e c t i o n   d e   l a   r e c h e r c h e  
  
 	 	 	 t h i s . d l g R e p l a c e . M e m o r i z e T e x t s ( ) ;  
  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T e x t R e p l a c e ( t h i s . d l g R e p l a c e . F i n d T e x t ,   n u l l ,   m o d e ) ;  
 	 	 }  
  
 	 	 # r e g i o n   P a l e t t e I O  
 	 	 [ C o m m a n d   ( C o m m o n . W i d g e t s . R e s . C o m m a n d I d s . C o l o r P a l e t t e . S e l e c t D e f a u l t C o l o r s ) ]  
 	 	 v o i d   C o m m a n d N e w P a l e t t e D e f a u l t ( )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n . C o p y F r o m   ( n e w   C o l o r C o l l e c t i o n ( C o l o r C o l l e c t i o n T y p e . D e f a u l t ) ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( C o m m o n . W i d g e t s . R e s . C o m m a n d I d s . C o l o r P a l e t t e . S e l e c t R a i n b o w C o l o r s ) ]  
 	 	 v o i d   N e w P a l e t t e R a i n b o w ( )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n . C o p y F r o m   ( n e w   C o l o r C o l l e c t i o n ( C o l o r C o l l e c t i o n T y p e . R a i n b o w ) ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( C o m m o n . W i d g e t s . R e s . C o m m a n d I d s . C o l o r P a l e t t e . S e l e c t L i g h t C o l o r s ) ]  
 	 	 v o i d   N e w P a l e t t e L i g h t ( )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n . C o p y F r o m   ( n e w   C o l o r C o l l e c t i o n   ( C o l o r C o l l e c t i o n T y p e . L i g h t ) ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( C o m m o n . W i d g e t s . R e s . C o m m a n d I d s . C o l o r P a l e t t e . S e l e c t D a r k C o l o r s ) ]  
 	 	 v o i d   N e w P a l e t t e D a r k ( )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n . C o p y F r o m   ( n e w   C o l o r C o l l e c t i o n ( C o l o r C o l l e c t i o n T y p e . D a r k ) ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( C o m m o n . W i d g e t s . R e s . C o m m a n d I d s . C o l o r P a l e t t e . S e l e c t G r a y C o l o r s ) ]  
 	 	 v o i d   C o m m a n d N e w P a l e t t e G r a y ( )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n . C o p y F r o m   ( n e w   C o l o r C o l l e c t i o n   ( C o l o r C o l l e c t i o n T y p e . G r a y ) ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( C o m m o n . W i d g e t s . R e s . C o m m a n d I d s . C o l o r P a l e t t e . L o a d ) ]  
 	 	 v o i d   C o m m a n d O p e n P a l e t t e ( )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 C o m m o n . D i a l o g s . F i l e O p e n D i a l o g   d i a l o g   =   n e w   C o m m o n . D i a l o g s . F i l e O p e n D i a l o g ( ) ;  
 	 	 	  
 	 	 	 d i a l o g . I n i t i a l D i r e c t o r y   =   t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n D i r e c t o r y ;  
 	 	 	 d i a l o g . F i l e N a m e   =   t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n F i l e n a m e ;  
 	 	 	 d i a l o g . T i t l e   =   R e s . S t r i n g s . D i a l o g . O p e n . T i t l e C o l ;  
 	 	 	 d i a l o g . F i l t e r s . A d d ( " c r c o l o r s " ,   R e s . S t r i n g s . D i a l o g . F i l e C o l ,   " * . c r c o l o r s " ) ;  
 	 	 	 d i a l o g . F i l t e r I n d e x   =   0 ;  
 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 i f   (   d i a l o g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t   )     r e t u r n ;  
  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n D i r e c t o r y   =   S y s t e m . I O . P a t h . G e t D i r e c t o r y N a m e ( d i a l o g . F i l e N a m e ) ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n F i l e n a m e   =   S y s t e m . I O . P a t h . G e t F i l e N a m e ( d i a l o g . F i l e N a m e ) ;  
  
 	 	 	 s t r i n g   e r r   =   t h i s . P a l e t t e R e a d ( d i a l o g . F i l e N a m e ) ;  
 	 	 	 t h i s . D i a l o g E r r o r ( e r r ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( C o m m o n . W i d g e t s . R e s . C o m m a n d I d s . C o l o r P a l e t t e . S a v e ) ]  
 	 	 v o i d   C o m m a n d S a v e P a l e t t e ( )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 C o m m o n . D i a l o g s . F i l e S a v e D i a l o g   d i a l o g   =   n e w   C o m m o n . D i a l o g s . F i l e S a v e D i a l o g ( ) ;  
 	 	 	  
 	 	 	 d i a l o g . I n i t i a l D i r e c t o r y   =   t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n D i r e c t o r y ;  
 	 	 	 d i a l o g . F i l e N a m e   =   t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n F i l e n a m e ;  
 	 	 	 d i a l o g . T i t l e   =   R e s . S t r i n g s . D i a l o g . S a v e . T i t l e C o l ;  
 	 	 	 d i a l o g . F i l t e r s . A d d ( " c r c o l o r s " ,   R e s . S t r i n g s . D i a l o g . F i l e C o l ,   " * . c r c o l o r s " ) ;  
 	 	 	 d i a l o g . F i l t e r I n d e x   =   0 ;  
 	 	 	 d i a l o g . P r o m p t F o r O v e r w r i t i n g   =   t r u e ;  
 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 i f   (   d i a l o g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t   )     r e t u r n ;  
  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n D i r e c t o r y   =   S y s t e m . I O . P a t h . G e t D i r e c t o r y N a m e ( d i a l o g . F i l e N a m e ) ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n F i l e n a m e   =   S y s t e m . I O . P a t h . G e t F i l e N a m e ( d i a l o g . F i l e N a m e ) ;  
  
 	 	 	 s t r i n g   e r r   =   t h i s . P a l e t t e W r i t e ( d i a l o g . F i l e N a m e ) ;  
 	 	 	 t h i s . D i a l o g E r r o r ( e r r ) ;  
 	 	 }  
  
 	 	 p u b l i c   s t r i n g   P a l e t t e R e a d ( s t r i n g   f i l e n a m e )  
 	 	 {  
 	 	 	 / / 	 L i t   l a   c o l l e c t i o n   d e   c o u l e u r s   à   p a r t i r   d ' u n   f i c h i e r .  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 u s i n g   (   S t r e a m   s t r e a m   =   F i l e . O p e n R e a d ( f i l e n a m e )   )  
 	 	 	 	 {  
 	 	 	 	 	 S o a p F o r m a t t e r   f o r m a t t e r   =   n e w   S o a p F o r m a t t e r ( ) ;  
 	 	 	 	 	 f o r m a t t e r . B i n d e r   =   n e w   G e n e r i c D e s e r i a l i z a t i o n B i n d e r   ( ) ;  
 	 	 	 	 	 C o l o r C o l l e c t i o n   c c   =   ( C o l o r C o l l e c t i o n )   f o r m a t t e r . D e s e r i a l i z e ( s t r e a m ) ;  
 	 	 	 	 	 c c . C o p y T o ( t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 c a t c h   (   S y s t e m . E x c e p t i o n   e   )  
 	 	 	 {  
 	 	 	 	 r e t u r n   e . M e s s a g e ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   " " ;     / /   o k  
 	 	 }  
  
 	 	 p u b l i c   s t r i n g   P a l e t t e W r i t e ( s t r i n g   f i l e n a m e )  
 	 	 {  
 	 	 	 / / 	 E c r i t   l a   c o l l e c t i o n   d e   c o u l e u r s   d a n s   u n   f i c h i e r .  
 	 	 	 i f   (   F i l e . E x i s t s ( f i l e n a m e )   )  
 	 	 	 {  
 	 	 	 	 F i l e . D e l e t e ( f i l e n a m e ) ;  
 	 	 	 }  
  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 u s i n g   (   S t r e a m   s t r e a m   =   F i l e . O p e n W r i t e ( f i l e n a m e )   )  
 	 	 	 	 {  
 	 	 	 	 	 S o a p F o r m a t t e r   f o r m a t t e r   =   n e w   S o a p F o r m a t t e r ( ) ;  
 	 	 	 	 	 f o r m a t t e r . S e r i a l i z e ( s t r e a m ,   t h i s . C u r r e n t D o c u m e n t . G l o b a l S e t t i n g s . C o l o r C o l l e c t i o n ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 c a t c h   (   S y s t e m . E x c e p t i o n   e   )  
 	 	 	 {  
 	 	 	 	 r e t u r n   e . M e s s a g e ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   " " ;     / /   o k  
 	 	 }  
 	 	 # e n d r e g i o n  
 	 	  
  
 	 	 [ C o m m a n d   ( " D e l e t e " ) ]  
 	 	 v o i d   C o m m a n d D e l e t e ( )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . D e l e t e S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " D u p l i c a t e " ) ]  
 	 	 v o i d   C o m m a n d D u p l i c a t e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 P o i n t   m o v e   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . E f f e c t i v e D u p l i c a t e M o v e ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . D u p l i c a t e S e l e c t i o n ( m o v e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C u t " ) ]  
 	 	 v o i d   C o m m a n d C u t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C u t S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C o p y " ) ]  
 	 	 v o i d   C o m m a n d C o p y ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o p y S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P a s t e " ) ]  
 	 	 v o i d   C o m m a n d P a s t e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . P a s t e ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( C o m m o n . D o c u m e n t . R e s . C o m m a n d I d s . F o n t B o l d ) ]  
 	 	 [ C o m m a n d   ( C o m m o n . D o c u m e n t . R e s . C o m m a n d I d s . F o n t I t a l i c ) ]  
 	 	 [ C o m m a n d   ( C o m m o n . D o c u m e n t . R e s . C o m m a n d I d s . F o n t U n d e r l i n e ) ]  
 	 	 [ C o m m a n d   ( C o m m a n d s . F o n t O v e r l i n e ) ]  
 	 	 [ C o m m a n d   ( C o m m a n d s . F o n t S t r i k e o u t ) ]  
 	 	 [ C o m m a n d   ( C o m m a n d s . F o n t S u b s c r i p t ) ]  
 	 	 [ C o m m a n d   ( C o m m a n d s . F o n t S u p e r s c r i p t ) ]  
 	 	 [ C o m m a n d   ( C o m m a n d s . F o n t S i z e P l u s ) ]  
 	 	 [ C o m m a n d   ( C o m m a n d s . F o n t S i z e M i n u s ) ]  
 	 	 [ C o m m a n d   ( C o m m a n d s . F o n t C l e a r ) ]  
 	 	 [ C o m m a n d   ( " P a r a g r a p h L e a d i n g P l u s " ) ]  
 	 	 [ C o m m a n d   ( " P a r a g r a p h L e a d i n g M i n u s " ) ]  
 	 	 [ C o m m a n d   ( " P a r a g r a p h I n d e n t P l u s " ) ]  
 	 	 [ C o m m a n d   ( " P a r a g r a p h I n d e n t M i n u s " ) ]  
 	 	 [ C o m m a n d   ( " P a r a g r a p h C l e a r " ) ]  
 	 	 v o i d   C o m m a n d F o n t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . W r a p p e r s . E x e c u t e C o m m a n d ( e . C o m m a n d . C o m m a n d I d ,   n u l l ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P a r a g r a p h L e a d i n g " ) ]  
 	 	 [ C o m m a n d   ( " P a r a g r a p h J u s t i f " ) ]  
 	 	 v o i d   C o m m a n d C o m b o ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 I c o n B u t t o n C o m b o   c o m b o   =   e . S o u r c e   a s   I c o n B u t t o n C o m b o ;  
 	 	 	 C o m m a n d S t a t e   c s   =   t h i s . c o m m a n d C o n t e x t . G e t C o m m a n d S t a t e   ( e . C o m m a n d ) ;  
 	 	 	 i f   (   c o m b o   ! =   n u l l   & &   c s   ! =   n u l l   )  
 	 	 	 {  
 	 	 	 	 c s . A d v a n c e d S t a t e   =   c o m b o . S e l e c t e d N a m e ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . W r a p p e r s . E x e c u t e C o m m a n d   ( e . C o m m a n d . C o m m a n d I d ,   c s . A d v a n c e d S t a t e ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . W r a p p e r s . E x e c u t e C o m m a n d   ( e . C o m m a n d . C o m m a n d I d ,   n u l l ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 [ C o m m a n d   ( " U n d o " ) ]  
 	 	 v o i d   C o m m a n d U n d o ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . U n d o ( 1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " R e d o " ) ]  
 	 	 v o i d   C o m m a n d R e d o ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R e d o ( 1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " U n d o R e d o L i s t " ) ]  
 	 	 v o i d   C o m m a n d U n d o R e d o L i s t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 # i f   f a l s e  
 	 	 	 W i d g e t   b u t t o n   =   t h i s . h T o o l B a r . F i n d C h i l d ( " U n d o " ) ;  
 	 	 	 i f   (   b u t t o n   = =   n u l l   )     r e t u r n ;  
 	 	 	 P o i n t   p o s   =   b u t t o n . M a p C l i e n t T o S c r e e n ( n e w   P o i n t ( 0 ,   1 ) ) ;  
 	 	 	 V M e n u   m e n u   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C r e a t e U n d o R e d o M e n u ( n u l l ) ;  
 	 	 	 m e n u . H o s t   =   t h i s ;  
 	 	 	 m e n u . A c c e p t e d   + =   t h i s . H a n d l e U n d o R e d o M e n u A c c e p t e d ;  
 	 	 	 m e n u . R e j e c t e d   + =   t h i s . H a n d l e U n d o R e d o M e n u R e j e c t e d ;  
 	 	 	 m e n u . S h o w A s C o n t e x t M e n u ( t h i s . W i n d o w ,   p o s ) ;  
 	 	 	 t h i s . W i d g e t U n d o R e d o M e n u E n g a g e d ( t r u e ) ;  
 # e n d i f  
 	 	 }  
  
 # i f   f a l s e  
 	 	 p r i v a t e   v o i d   H a n d l e U n d o R e d o M e n u A c c e p t e d ( o b j e c t   s e n d e r ,   M e n u E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . W i d g e t U n d o R e d o M e n u E n g a g e d ( f a l s e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e U n d o R e d o M e n u R e j e c t e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 t h i s . W i d g e t U n d o R e d o M e n u E n g a g e d ( f a l s e ) ;  
 	 	 }  
 # e n d i f  
  
 # i f   f a l s e  
 	 	 p r o t e c t e d   v o i d   W i d g e t U n d o R e d o M e n u E n g a g e d ( b o o l   e n g a g e d )  
 	 	 {  
 	 	 	 W i d g e t   b u t t o n ;  
  
 	 	 	 b u t t o n   =   t h i s . h T o o l B a r . F i n d C h i l d ( " U n d o " ) ;  
 	 	 	 i f   (   b u t t o n   ! =   n u l l   )     b u t t o n . A c t i v e S t a t e   =   e n g a g e d   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	  
 	 	 	 b u t t o n   =   t h i s . h T o o l B a r . F i n d C h i l d ( " U n d o R e d o L i s t " ) ;  
 	 	 	 i f   (   b u t t o n   ! =   n u l l   )     b u t t o n . A c t i v e S t a t e   =   e n g a g e d   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	  
 	 	 	 b u t t o n   =   t h i s . h T o o l B a r . F i n d C h i l d ( " R e d o " ) ;  
 	 	 	 i f   (   b u t t o n   ! =   n u l l   )     b u t t o n . A c t i v e S t a t e   =   e n g a g e d   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 }  
 # e n d i f  
  
 	 	 [ C o m m a n d   ( " U n d o R e d o L i s t D o " ) ]  
 	 	 v o i d   C o m m a n d U n d o R e d o L i s t D o ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 s t r i n g   v a l u e   =   e . C o m m a n d S t a t e . A d v a n c e d S t a t e ;  
  
 	 	 	 i n t   n b   =   S y s t e m . C o n v e r t . T o I n t 3 2 ( v a l u e ) ;  
 	 	 	 i f   (   n b   >   0   )  
 	 	 	 {  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . U n d o ( n b ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R e d o ( - n b ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 [ C o m m a n d   ( " O r d e r U p O n e " ) ]  
 	 	 v o i d   C o m m a n d O r d e r U p O n e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . O r d e r U p O n e S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " O r d e r D o w n O n e " ) ]  
 	 	 v o i d   C o m m a n d O r d e r D o w n O n e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . O r d e r D o w n O n e S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " O r d e r U p A l l " ) ]  
 	 	 v o i d   C o m m a n d O r d e r U p A l l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . O r d e r U p A l l S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " O r d e r D o w n A l l " ) ]  
 	 	 v o i d   C o m m a n d O r d e r D o w n A l l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . O r d e r D o w n A l l S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " R o t a t e 9 0 " ) ]  
 	 	 v o i d   C o m m a n d R o t a t e 9 0 ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R o t a t e S e l e c t i o n ( 9 0 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " R o t a t e 1 8 0 " ) ]  
 	 	 v o i d   C o m m a n d R o t a t e 1 8 0 ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R o t a t e S e l e c t i o n ( 1 8 0 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " R o t a t e 2 7 0 " ) ]  
 	 	 v o i d   C o m m a n d R o t a t e 2 7 0 ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R o t a t e S e l e c t i o n ( 2 7 0 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " R o t a t e F r e e C C W " ) ]  
 	 	 v o i d   C o m m a n d R o t a t e F r e e C C W ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   a n g l e   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R o t a t e A n g l e ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R o t a t e S e l e c t i o n ( a n g l e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " R o t a t e F r e e C W " ) ]  
 	 	 v o i d   C o m m a n d R o t a t e F r e e C W ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   a n g l e   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R o t a t e A n g l e ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R o t a t e S e l e c t i o n ( - a n g l e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M i r r o r H " ) ]  
 	 	 v o i d   C o m m a n d M i r r o r H ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M i r r o r S e l e c t i o n ( t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M i r r o r V " ) ]  
 	 	 v o i d   C o m m a n d M i r r o r V ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M i r r o r S e l e c t i o n ( f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S c a l e M u l 2 " ) ]  
 	 	 v o i d   C o m m a n d S c a l e M u l 2 ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S c a l e S e l e c t i o n ( 2 . 0 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S c a l e D i v 2 " ) ]  
 	 	 v o i d   C o m m a n d S c a l e D i v 2 ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S c a l e S e l e c t i o n ( 0 . 5 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S c a l e M u l F r e e " ) ]  
 	 	 v o i d   C o m m a n d S c a l e M u l F r e e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   s c a l e   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S c a l e F a c t o r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S c a l e S e l e c t i o n ( s c a l e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S c a l e D i v F r e e " ) ]  
 	 	 v o i d   C o m m a n d S c a l e D i v F r e e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   s c a l e   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S c a l e F a c t o r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S c a l e S e l e c t i o n ( 1 . 0 / s c a l e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A l i g n L e f t " ) ]  
 	 	 v o i d   C o m m a n d A l i g n L e f t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A l i g n S e l e c t i o n ( - 1 ,   t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A l i g n C e n t e r X " ) ]  
 	 	 v o i d   C o m m a n d A l i g n C e n t e r X ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A l i g n S e l e c t i o n ( 0 ,   t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A l i g n R i g h t " ) ]  
 	 	 v o i d   C o m m a n d A l i g n R i g h t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A l i g n S e l e c t i o n ( 1 ,   t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A l i g n T o p " ) ]  
 	 	 v o i d   C o m m a n d A l i g n T o p ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A l i g n S e l e c t i o n ( 1 ,   f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A l i g n C e n t e r Y " ) ]  
 	 	 v o i d   C o m m a n d A l i g n C e n t e r Y ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A l i g n S e l e c t i o n ( 0 ,   f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A l i g n B o t t o m " ) ]  
 	 	 v o i d   C o m m a n d A l i g n B o t t o m ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A l i g n S e l e c t i o n ( - 1 ,   f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " A l i g n G r i d " ) ]  
 	 	 v o i d   C o m m a n d A l i g n G r i d ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A l i g n G r i d S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " R e s e t " ) ]  
 	 	 v o i d   C o m m a n d R e s e t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R e s e t S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " S h a r e L e f t " ) ]  
 	 	 v o i d   C o m m a n d S h a r e L e f t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S h a r e S e l e c t i o n ( - 1 ,   t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S h a r e C e n t e r X " ) ]  
 	 	 v o i d   C o m m a n d S h a r e C e n t e r X ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S h a r e S e l e c t i o n ( 0 ,   t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S h a r e S p a c e X " ) ]  
 	 	 v o i d   C o m m a n d S h a r e S p a c e X ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S p a c e S e l e c t i o n ( t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S h a r e R i g h t " ) ]  
 	 	 v o i d   C o m m a n d S h a r e R i g h t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S h a r e S e l e c t i o n ( 1 ,   t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S h a r e T o p " ) ]  
 	 	 v o i d   C o m m a n d S h a r e T o p ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S h a r e S e l e c t i o n ( 1 ,   f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S h a r e C e n t e r Y " ) ]  
 	 	 v o i d   C o m m a n d S h a r e C e n t e r Y ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S h a r e S e l e c t i o n ( 0 ,   f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S h a r e S p a c e Y " ) ]  
 	 	 v o i d   C o m m a n d S h a r e S p a c e Y ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S p a c e S e l e c t i o n ( f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S h a r e B o t t o m " ) ]  
 	 	 v o i d   C o m m a n d S h a r e B o t t o m ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S h a r e S e l e c t i o n ( - 1 ,   f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A d j u s t W i d t h " ) ]  
 	 	 v o i d   C o m m a n d A d j u s t W i d t h ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A d j u s t S e l e c t i o n ( t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A d j u s t H e i g h t " ) ]  
 	 	 v o i d   C o m m a n d A d j u s t H e i g h t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A d j u s t S e l e c t i o n ( f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C o l o r T o R g b " ) ]  
 	 	 v o i d   C o m m a n d C o l o r T o R g b ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r S e l e c t i o n ( C o l o r S p a c e . R g b ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C o l o r T o C m y k " ) ]  
 	 	 v o i d   C o m m a n d C o l o r T o C m y k ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r S e l e c t i o n ( C o l o r S p a c e . C m y k ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C o l o r T o G r a y " ) ]  
 	 	 v o i d   C o m m a n d C o l o r T o G r a y ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r S e l e c t i o n ( C o l o r S p a c e . G r a y ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C o l o r S t r o k e D a r k " ) ]  
 	 	 v o i d   C o m m a n d C o l o r S t r o k e D a r k ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   a d j u s t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r A d j u s t ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r S e l e c t i o n ( - a d j u s t ,   t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C o l o r S t r o k e L i g h t " ) ]  
 	 	 v o i d   C o m m a n d C o l o r S t r o k e L i g h t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   a d j u s t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r A d j u s t ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r S e l e c t i o n ( a d j u s t ,   t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C o l o r F i l l D a r k " ) ]  
 	 	 v o i d   C o m m a n d C o l o r F i l l D a r k ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   a d j u s t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r A d j u s t ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r S e l e c t i o n ( - a d j u s t ,   f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C o l o r F i l l L i g h t " ) ]  
 	 	 v o i d   C o m m a n d C o l o r F i l l L i g h t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   a d j u s t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r A d j u s t ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o l o r S e l e c t i o n ( a d j u s t ,   f a l s e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M e r g e " ) ]  
 	 	 v o i d   C o m m a n d M e r g e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M e r g e S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " E x t r a c t " ) ]  
 	 	 v o i d   C o m m a n d E x t r a c t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . E x t r a c t S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " G r o u p " ) ]  
 	 	 v o i d   C o m m a n d G r o u p ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . G r o u p S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " U n g r o u p " ) ]  
 	 	 v o i d   C o m m a n d U n g r o u p ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . U n g r o u p S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " I n s i d e " ) ]  
 	 	 v o i d   C o m m a n d I n s i d e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . I n s i d e S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " O u t s i d e " ) ]  
 	 	 v o i d   C o m m a n d O u t s i d e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . O u t s i d e S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " C o m b i n e " ) ]  
 	 	 v o i d   C o m m a n d C o m b i n e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . C o m b i n e S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " U n c o m b i n e " ) ]  
 	 	 v o i d   C o m m a n d U n c o m b i n e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . U n c o m b i n e S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " T o B e z i e r " ) ]  
 	 	 v o i d   C o m m a n d T o B e z i e r ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o B e z i e r S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " T o P o l y " ) ]  
 	 	 v o i d   C o m m a n d T o P o l y ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o P o l y S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " T o T e x t B o x 2 " ) ]  
 	 	 v o i d   C o m m a n d T o T e x t B o x 2 ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o T e x t B o x 2 S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " T o S i m p l e s t " ) ]  
 	 	 v o i d   C o m m a n d T o S i m p l e s t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o S i m p l e s t S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " F r a g m e n t " ) ]  
 	 	 v o i d   C o m m a n d F r a g m e n t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . F r a g m e n t S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e A d d " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e S u b " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e T o L i n e " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e T o C u r v e " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e S y m " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e S m o o t h " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e D i s " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e I n l i n e " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e F r e e " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e S i m p l y " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e C o r n e r " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e C o n t i n u e " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e S h a r p " ) ]  
 	 	 [ C o m m a n d   ( " S h a p e r H a n d l e R o u n d " ) ]  
 	 	 v o i d   C o m m a n d S h a p e r H a n d l e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S h a p e r H a n d l e C o m m a n d   ( e . C o m m a n d . C o m m a n d I d ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " B o o l e a n O r " ) ]  
 	 	 v o i d   C o m m a n d B o o l e a n O r ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . B o o l e a n S e l e c t i o n ( D r a w i n g . P a t h O p e r a t i o n . O r ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " B o o l e a n A n d " ) ]  
 	 	 v o i d   C o m m a n d B o o l e a n A n d ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . B o o l e a n S e l e c t i o n ( D r a w i n g . P a t h O p e r a t i o n . A n d ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " B o o l e a n X o r " ) ]  
 	 	 v o i d   C o m m a n d B o o l e a n X o r ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . B o o l e a n S e l e c t i o n ( D r a w i n g . P a t h O p e r a t i o n . X o r ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " B o o l e a n F r o n t M i n u s " ) ]  
 	 	 v o i d   C o m m a n d B o o l e a n F r o n t M i n u s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . B o o l e a n S e l e c t i o n ( D r a w i n g . P a t h O p e r a t i o n . A M i n u s B ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " B o o l e a n B a c k M i n u s " ) ]  
 	 	 v o i d   C o m m a n d B o o l e a n B a c k M i n u s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . B o o l e a n S e l e c t i o n ( D r a w i n g . P a t h O p e r a t i o n . B M i n u s A ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " G r i d " ) ]  
 	 	 v o i d   C o m m a n d G r i d ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . G r i d A c t i v e   =   ! c o n t e x t . G r i d A c t i v e ;  
 	 	 	 c o n t e x t . G r i d S h o w   =   c o n t e x t . G r i d A c t i v e ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " T e x t G r i d " ) ]  
 	 	 v o i d   C o m m a n d T e x t G r i d ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . T e x t G r i d S h o w   =   ! c o n t e x t . T e x t G r i d S h o w ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( C o m m a n d s . T e x t S h o w C o n t r o l C h a r a c t e r s ) ]  
 	 	 v o i d   C o m m a n d T e x t S h o w C o n t r o l C h a r a c t e r s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . T e x t S h o w C o n t r o l C h a r a c t e r s   =   ! c o n t e x t . T e x t S h o w C o n t r o l C h a r a c t e r s ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " T e x t F o n t F i l t e r " ) ]  
 	 	 v o i d   C o m m a n d T e x t F o n t F i l t e r ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . T e x t F o n t F i l t e r   =   ! c o n t e x t . T e x t F o n t F i l t e r ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " T e x t F o n t S a m p l e A b c " ) ]  
 	 	 v o i d   C o m m a n d T e x t F o n t S a m p l e A b c ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . T e x t F o n t S a m p l e A b c   =   ! c o n t e x t . T e x t F o n t S a m p l e A b c ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " T e x t I n s e r t Q u a d " ) ]  
 	 	 v o i d   C o m m a n d T e x t I n s e r t Q u a d ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . E d i t I n s e r t T e x t ( C o m m o n . T e x t . U n i c o d e . C o d e . N o B r e a k S p a c e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " T e x t I n s e r t N e w F r a m e " ) ]  
 	 	 v o i d   C o m m a n d T e x t I n s e r t N e w F r a m e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . E d i t I n s e r t T e x t ( C o m m o n . T e x t . P r o p e r t i e s . B r e a k P r o p e r t y . N e w F r a m e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " T e x t I n s e r t N e w P a g e " ) ]  
 	 	 v o i d   C o m m a n d T e x t I n s e r t N e w P a g e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . E d i t I n s e r t T e x t ( C o m m o n . T e x t . P r o p e r t i e s . B r e a k P r o p e r t y . N e w P a g e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " C o n s t r a i n " ) ]  
 	 	 v o i d   C o m m a n d C o n s t r a i n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . C o n s t r a i n A c t i v e   =   ! c o n t e x t . C o n s t r a i n A c t i v e ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " M a g n e t " ) ]  
 	 	 v o i d   C o m m a n d M a g n e t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . M a g n e t A c t i v e   =   ! c o n t e x t . M a g n e t A c t i v e ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M a g n e t L a y e r " ) ]  
 	 	 v o i d   C o m m a n d M a g n e t L a y e r ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M a g n e t L a y e r I n v e r t ( r a n k ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " R u l e r s " ) ]  
 	 	 v o i d   C o m m a n d R u l e r s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . R u l e r s S h o w   =   ! c o n t e x t . R u l e r s S h o w ;  
 	 	 	 t h i s . U p d a t e R u l e r s ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " L a b e l s " ) ]  
 	 	 v o i d   C o m m a n d L a b e l s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . L a b e l s S h o w   =   ! c o n t e x t . L a b e l s S h o w ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A g g r e g a t e s " ) ]  
 	 	 v o i d   C o m m a n d A g g r e g a t e s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . A g g r e g a t e s S h o w   =   ! c o n t e x t . A g g r e g a t e s S h o w ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P r e v i e w " ) ]  
 	 	 v o i d   C o m m a n d P r e v i e w ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . P r e v i e w A c t i v e   =   ! c o n t e x t . P r e v i e w A c t i v e ;  
 	 	 }  
  
 / / 	 	 [ C o m m a n d   ( " D e s e l e c t A l l " ) ]  
 	 	 [ C o m m a n d   ( C o m m o n . W i d g e t s . R e s . C o m m a n d I d s . D e s e l e c t A l l ) ]  
 	 	 v o i d   C o m m a n d D e s e l e c t A l l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . D e s e l e c t A l l C m d ( ) ;  
 	 	 }  
  
 / / 	 	 [ C o m m a n d   ( " S e l e c t A l l " ) ]  
 	 	 [ C o m m a n d   ( C o m m o n . W i d g e t s . R e s . C o m m a n d I d s . S e l e c t A l l ) ]  
 	 	 v o i d   C o m m a n d S e l e c t A l l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S e l e c t A l l ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S e l e c t I n v e r t " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t I n v e r t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . I n v e r t S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S e l e c t T o t a l " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t T o t a l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o o l   =   " T o o l S e l e c t " ;  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . P a r t i a l S e l e c t   =   f a l s e ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S e l e c t P a r t i a l " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t P a r t i a l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o o l   =   " T o o l S e l e c t " ;  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . P a r t i a l S e l e c t   =   t r u e ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S e l e c t o r A u t o " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t o r A u t o ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o o l   =   " T o o l S e l e c t " ;  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . S e l e c t o r T y p e   =   S e l e c t o r T y p e . A u t o ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S e l e c t o r I n d i v i d u a l " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t o r I n d i v i d u a l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o o l   =   " T o o l S e l e c t " ;  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . S e l e c t o r T y p e   =   S e l e c t o r T y p e . I n d i v i d u a l ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S e l e c t o r S c a l e r " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t o r S c a l e r ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o o l   =   " T o o l S e l e c t " ;  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . S e l e c t o r T y p e   =   S e l e c t o r T y p e . S c a l e r ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S e l e c t o r S t r e t c h " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t o r S t r e t c h ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o o l   =   " T o o l S e l e c t " ;  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . S e l e c t o r T y p e   =   S e l e c t o r T y p e . S t r e t c h e r ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S e l e c t o r S t r e t c h T y p e " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t o r S t r e t c h T y p e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 H T o o l B a r   t o o l b a r   =   d i . c o n t a i n e r P r i n c i p a l . S e l e c t o r T o o l B a r ;  
 	 	 	 W i d g e t   b u t t o n   =   t o o l b a r . F i n d C h i l d ( " S e l e c t o r S t r e t c h " ) ;  
 	 	 	 W i d g e t   t y p e   =   t o o l b a r . F i n d C h i l d ( " S e l e c t o r S t r e t c h T y p e " ) ;  
 	 	 	 i f   ( b u t t o n   = =   n u l l )     r e t u r n ;  
 	 	 	 V M e n u   m e n u   =   d i . c o n t a i n e r P r i n c i p a l . C r e a t e S t r e t c h T y p e M e n u ( n u l l ) ;  
 	 	 	 m e n u . H o s t   =   t h i s ;  
 	 	 	 m e n u . B e h a v i o r . A c c e p t e d   + =   t h i s . H a n d l e S t r e t c h T y p e M e n u A c c e p t e d ;  
 	 	 	 m e n u . B e h a v i o r . R e j e c t e d   + =   t h i s . H a n d l e S t r e t c h T y p e M e n u R e j e c t e d ;  
 	 	 	 m e n u . M i n W i d t h   =   b u t t o n . A c t u a l W i d t h + t y p e . A c t u a l W i d t h ;  
 	 	 	 T e x t F i e l d C o m b o . A d j u s t C o m b o S i z e ( b u t t o n ,   m e n u ,   f a l s e ) ;  
 	 	 	 m e n u . S h o w A s C o m b o L i s t ( b u t t o n ,   P o i n t . Z e r o ,   t y p e ) ;  
 	 	 	 t h i s . W i d g e t S t r e t c h T y p e M e n u E n g a g e d ( t r u e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S t r e t c h T y p e M e n u A c c e p t e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 t h i s . W i d g e t S t r e t c h T y p e M e n u E n g a g e d ( t r u e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S t r e t c h T y p e M e n u R e j e c t e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 b o o l   a c t i v a t e   =   ( v i e w e r . S e l e c t o r T y p e   = =   S e l e c t o r T y p e . S t r e t c h e r ) ;  
 	 	 	 t h i s . W i d g e t S t r e t c h T y p e M e n u E n g a g e d ( a c t i v a t e ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   W i d g e t S t r e t c h T y p e M e n u E n g a g e d ( b o o l   a c t i v a t e )  
 	 	 {  
 	 	 	 W i d g e t   b u t t o n ;  
  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 H T o o l B a r   t o o l b a r   =   d i . c o n t a i n e r P r i n c i p a l . S e l e c t o r T o o l B a r ;  
 	 	 	 i f   (   t o o l b a r   = =   n u l l   )     r e t u r n ;  
  
 	 	 	 b u t t o n   =   t o o l b a r . F i n d C h i l d ( " S e l e c t o r S t r e t c h " ) ;  
 	 	 	 i f   (   b u t t o n   ! =   n u l l   )     b u t t o n . A c t i v e S t a t e   =   a c t i v a t e   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S e l e c t o r S t r e t c h T y p e D o " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t o r S t r e t c h T y p e D o ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 s t r i n g   v a l u e   =   e . C o m m a n d S t a t e . A d v a n c e d S t a t e ;  
 	 	 	 i n t   n b   =   S y s t e m . C o n v e r t . T o I n t 3 2 ( v a l u e ) ;  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . S e l e c t o r T y p e S t r e t c h   =   ( S e l e c t o r T y p e S t r e t c h )   n b ;  
 	 	 }  
  
 	 	  
 	 	 [ C o m m a n d   ( " S e l e c t o r A d a p t L i n e " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t o r A d a p t L i n e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . S e l e c t o r A d a p t L i n e   =   ! v i e w e r . S e l e c t o r A d a p t L i n e ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " S e l e c t o r A d a p t T e x t " ) ]  
 	 	 v o i d   C o m m a n d S e l e c t o r A d a p t T e x t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 v i e w e r . S e l e c t o r A d a p t T e x t   =   ! v i e w e r . S e l e c t o r A d a p t T e x t ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " H i d e H a l f " ) ]  
 	 	 v o i d   C o m m a n d H i d e H a l f ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o o l   =   " T o o l S e l e c t " ;  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . H i d e H a l f A c t i v e   =   ! c o n t e x t . H i d e H a l f A c t i v e ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " H i d e S e l " ) ]  
 	 	 v o i d   C o m m a n d H i d e S e l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . H i d e S e l e c t i o n ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " H i d e R e s t " ) ]  
 	 	 v o i d   C o m m a n d H i d e R e s t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . H i d e R e s t ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " H i d e C a n c e l " ) ]  
 	 	 v o i d   C o m m a n d H i d e C a n c e l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . H i d e C a n c e l ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " Z o o m M i n " ) ]  
 	 	 v o i d   C o m m a n d Z o o m M i n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m C h a n g e ( 0 . 0 0 0 1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " Z o o m P a g e " ) ]  
 	 	 v o i d   C o m m a n d Z o o m P a g e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m M e m o r i z e ( ) ;  
 	 	 	 c o n t e x t . Z o o m P a g e A n d C e n t e r ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " Z o o m P a g e W i d t h " ) ]  
 	 	 v o i d   C o m m a n d Z o o m P a g e W i d t h ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m M e m o r i z e ( ) ;  
 	 	 	 c o n t e x t . Z o o m P a g e W i d t h A n d C e n t e r ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " Z o o m D e f a u l t " ) ]  
 	 	 v o i d   C o m m a n d Z o o m D e f a u l t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m M e m o r i z e ( ) ;  
 	 	 	 c o n t e x t . Z o o m D e f a u l t A n d C e n t e r ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " Z o o m S e l " ) ]  
 	 	 v o i d   C o m m a n d Z o o m S e l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m S e l ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " Z o o m S e l W i d t h " ) ]  
 	 	 v o i d   C o m m a n d Z o o m S e l W i d t h ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m S e l W i d t h ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " Z o o m P r e v " ) ]  
 	 	 v o i d   C o m m a n d Z o o m P r e v ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m P r e v ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " Z o o m S u b " ) ]  
 	 	 v o i d   C o m m a n d Z o o m S u b ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m C h a n g e ( 1 . 0 / 1 . 2 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " Z o o m A d d " ) ]  
 	 	 v o i d   C o m m a n d Z o o m A d d ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m C h a n g e ( 1 . 2 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " Z o o m C h a n g e " ) ]  
 	 	 v o i d   C o m m a n d Z o o m C h a n g e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 s t r i n g   v a l u e   =   e . C o m m a n d S t a t e . A d v a n c e d S t a t e ;  
 	 	 	 d o u b l e   z o o m   =   S y s t e m . C o n v e r t . T o D o u b l e ( v a l u e ) ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m V a l u e ( z o o m ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " O b j e c t " ) ]  
 	 	 n e w   v o i d   C o m m a n d O b j e c t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 E x é c u t e   u n e   c o m m a n d e   l o c a l e   à   u n   o b j e t .  
 	 	 	 W i d g e t   w i d g e t   =   e . S o u r c e   a s   W i d g e t ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . E x e c u t e O b j e c t C o m m a n d ( w i d g e t . N a m e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A r r a y O u t l i n e F r a m e " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y O u t l i n e H o r i z " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y O u t l i n e V e r t i " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y A d d C o l u m n L e f t " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y A d d C o l u m n R i g h t " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y A d d R o w T o p " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y A d d R o w B o t t o m " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y D e l C o l u m n " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y D e l R o w " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y A l i g n C o l u m n " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y A l i g n R o w " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y S w a p C o l u m n " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y S w a p R o w " ) ]  
 	 	 [ C o m m a n d   ( " A r r a y L o o k " ) ]  
 	 	 v o i d   C o m m a n d A r r a y ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 }  
  
  
 	 	 [ C o m m a n d   ( " S e t t i n g s " ) ]  
 	 	 v o i d   C o m m a n d S e t t i n g s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 i f   (   t h i s . s e t t i n g s S t a t e . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o   )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g S e t t i n g s . S h o w ( ) ;  
 	 	 	 	 t h i s . s e t t i n g s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d l g S e t t i n g s . H i d e ( ) ;  
 	 	 	 	 t h i s . s e t t i n g s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " I n f o s " ) ]  
 	 	 v o i d   C o m m a n d I n f o s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 i f   (   t h i s . i n f o s S t a t e . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o   )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g I n f o s . S h o w ( ) ;  
 	 	 	 	 t h i s . i n f o s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d l g I n f o s . H i d e ( ) ;  
 	 	 	 	 t h i s . i n f o s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P a g e S t a c k " ) ]  
 	 	 v o i d   C o m m a n d P a g e S t a c k ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 i f   (   t h i s . p a g e S t a c k S t a t e . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o   )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g P a g e S t a c k . S h o w ( ) ;  
 	 	 	 	 t h i s . p a g e S t a c k S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d l g P a g e S t a c k . H i d e ( ) ;  
 	 	 	 	 t h i s . p a g e S t a c k S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " U p d a t e A p p l i c a t i o n " ) ]  
 	 	 v o i d   C o m m a n d U p d a t e A p p l i c a t i o n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . S t a r t C h e c k ( t r u e ) ;  
 	 	 	 t h i s . E n d C h e c k ( t r u e ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " A b o u t A p p l i c a t i o n " ) ]  
 	 	 v o i d   C o m m a n d A b o u t A p p l i c a t i o n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
 	 	 	 t h i s . d l g A b o u t . S h o w ( ) ;  
 	 	 }  
  
  
 	 	 [ C o m m a n d   ( " M o v e L e f t F r e e " ) ]  
 	 	 v o i d   C o m m a n d M o v e L e f t F r e e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   d x   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e D i s t a n c e H ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( - d x , 0 ) ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e R i g h t F r e e " ) ]  
 	 	 v o i d   C o m m a n d M o v e R i g h t F r e e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   d x   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e D i s t a n c e H ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( d x , 0 ) ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e U p F r e e " ) ]  
 	 	 v o i d   C o m m a n d M o v e U p F r e e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   d y   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e D i s t a n c e V ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 0 , d y ) ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e D o w n F r e e " ) ]  
 	 	 v o i d   C o m m a n d M o v e D o w n F r e e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 d o u b l e   d y   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e D i s t a n c e V ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 0 , - d y ) ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e L e f t N o r m " ) ]  
 	 	 v o i d   C o m m a n d M o v e L e f t N o r m ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( - 1 , 0 ) ,   0 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e R i g h t N o r m " ) ]  
 	 	 v o i d   C o m m a n d M o v e R i g h t N o r m ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 1 , 0 ) ,   0 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e U p N o r m " ) ]  
 	 	 v o i d   C o m m a n d M o v e U p N o r m ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 0 , 1 ) ,   0 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e D o w n N o r m " ) ]  
 	 	 v o i d   C o m m a n d M o v e D o w n N o r m ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 0 , - 1 ) ,   0 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e L e f t C t r l " ) ]  
 	 	 v o i d   C o m m a n d M o v e L e f t C t r l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( - 1 , 0 ) ,   - 1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e R i g h t C t r l " ) ]  
 	 	 v o i d   C o m m a n d M o v e R i g h t C t r l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 1 , 0 ) ,   - 1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e U p C t r l " ) ]  
 	 	 v o i d   C o m m a n d M o v e U p C t r l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 0 , 1 ) ,   - 1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e D o w n C t r l " ) ]  
 	 	 v o i d   C o m m a n d M o v e D o w n C t r l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 0 , - 1 ) ,   - 1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e L e f t S h i f t " ) ]  
 	 	 v o i d   C o m m a n d M o v e L e f t S h i f t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( - 1 , 0 ) ,   1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e R i g h t S h i f t " ) ]  
 	 	 v o i d   C o m m a n d M o v e R i g h t S h i f t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 1 , 0 ) ,   1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e U p S h i f t " ) ]  
 	 	 v o i d   C o m m a n d M o v e U p S h i f t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 0 , 1 ) ,   1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " M o v e D o w n S h i f t " ) ]  
 	 	 v o i d   C o m m a n d M o v e D o w n S h i f t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M o v e S e l e c t i o n ( n e w   P o i n t ( 0 , - 1 ) ,   1 ) ;  
 	 	 }  
  
  
 	 	 [ C o m m a n d   ( " P a g e P r e v " ) ]  
 	 	 v o i d   C o m m a n d P a g e P r e v ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i f   (   c o n t e x t . C u r r e n t P a g e   >   0   )  
 	 	 	 {  
 	 	 	 	 c o n t e x t . C u r r e n t P a g e   =   c o n t e x t . C u r r e n t P a g e - 1 ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P a g e N e x t " ) ]  
 	 	 v o i d   C o m m a n d P a g e N e x t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i f   (   c o n t e x t . C u r r e n t P a g e   <   c o n t e x t . T o t a l P a g e s ( ) - 1   )  
 	 	 	 {  
 	 	 	 	 c o n t e x t . C u r r e n t P a g e   =   c o n t e x t . C u r r e n t P a g e + 1 ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e Q u i c k P a g e M e n u ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   " m e n u   d e s   p a g e s "   c l i q u é .  
 	 	 	 B u t t o n   b u t t o n   =   s e n d e r   a s   B u t t o n ;  
 	 	 	 i f   (   b u t t o n   = =   n u l l   )     r e t u r n ;  
 	 	 	 V M e n u   m e n u   =   t h i s . C r e a t e P a g e s M e n u ( n u l l ) ;  
 	 	 	 m e n u . H o s t   =   b u t t o n . W i n d o w ;  
 	 	 	 T e x t F i e l d C o m b o . A d j u s t C o m b o S i z e ( b u t t o n ,   m e n u ,   f a l s e ) ;  
 	 	 	 m e n u . S h o w A s C o m b o L i s t ( b u t t o n ,   P o i n t . Z e r o ,   b u t t o n ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P a g e S e l e c t " ) ]  
 	 	 v o i d   C o m m a n d P a g e S e l e c t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 s t r i n g   v a l u e   =   e . C o m m a n d S t a t e . A d v a n c e d S t a t e ;  
 	 	 	 i n t   s e l   =   S y s t e m . C o n v e r t . T o I n t 3 2 ( v a l u e ) ;  
 	 	 	 c o n t e x t . C u r r e n t P a g e   =   s e l ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " P a g e M i n i a t u r e s " ) ]  
 	 	 v o i d   C o m m a n d P a g e M i n i a t u r e s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 d i . d o c u m e n t . P a g e M i n i a t u r e s . I s P a n e l S h o w e d   =   ! d i . d o c u m e n t . P a g e M i n i a t u r e s . I s P a n e l S h o w e d ;  
 	 	 	 d i . p a g e P a n e . V i s i b i l i t y   =   d i . d o c u m e n t . P a g e M i n i a t u r e s . I s P a n e l S h o w e d ;  
 	 	 	 d i . q u i c k P a g e M i n i a t u r e s . G l y p h S h a p e   =   d i . d o c u m e n t . P a g e M i n i a t u r e s . I s P a n e l S h o w e d   ?   G l y p h S h a p e . A r r o w D o w n   :   G l y p h S h a p e . A r r o w U p ;  
 	 	 	 d i . p a g e M i n i a t u r e s . U p d a t e P a g e A f t e r C h a n g i n g ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " P a g e N e w " ) ]  
 	 	 v o i d   C o m m a n d P a g e N e w ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t P a g e ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . P a g e N e w ( r a n k + 1 ,   " " ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " P a g e D u p l i c a t e " ) ]  
 	 	 v o i d   C o m m a n d P a g e D u p l i c a t e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t P a g e ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . P a g e D u p l i c a t e ( r a n k ,   r a n k + 1 ,   " " ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P a g e D e l e t e " ) ]  
 	 	 v o i d   C o m m a n d P a g e D e l e t e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t P a g e ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . P a g e D e l e t e ( r a n k ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P a g e U p " ) ]  
 	 	 v o i d   C o m m a n d P a g e U p ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t P a g e ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . P a g e S w a p ( r a n k ,   r a n k - 1 ) ;  
 	 	 	 c o n t e x t . C u r r e n t P a g e   =   r a n k - 1 ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " P a g e D o w n " ) ]  
 	 	 v o i d   C o m m a n d P a g e D o w n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t P a g e ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . P a g e S w a p ( r a n k ,   r a n k + 1 ) ;  
 	 	 	 c o n t e x t . C u r r e n t P a g e   =   r a n k + 1 ;  
 	 	 }  
  
  
 	 	 [ C o m m a n d   ( " L a y e r P r e v " ) ]  
 	 	 v o i d   C o m m a n d L a y e r P r e v ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i f   (   c o n t e x t . C u r r e n t L a y e r   >   0   )  
 	 	 	 {  
 	 	 	 	 c o n t e x t . C u r r e n t L a y e r   =   c o n t e x t . C u r r e n t L a y e r - 1 ;  
 	 	 	 }  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " L a y e r N e x t " ) ]  
 	 	 v o i d   C o m m a n d L a y e r N e x t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i f   (   c o n t e x t . C u r r e n t L a y e r   <   c o n t e x t . T o t a l L a y e r s ( ) - 1   )  
 	 	 	 {  
 	 	 	 	 c o n t e x t . C u r r e n t L a y e r   =   c o n t e x t . C u r r e n t L a y e r + 1 ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e Q u i c k L a y e r M e n u ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   " m e n u   d e s   c a l q u e s "   c l i q u é .  
 	 	 	 B u t t o n   b u t t o n   =   s e n d e r   a s   B u t t o n ;  
 	 	 	 i f   (   b u t t o n   = =   n u l l   )     r e t u r n ;  
 	 	 	 V M e n u   m e n u   =   t h i s . C r e a t e L a y e r s M e n u ( n u l l ) ;  
 	 	 	 m e n u . H o s t   =   b u t t o n . W i n d o w ;  
 	 	 	 T e x t F i e l d C o m b o . A d j u s t C o m b o S i z e ( b u t t o n ,   m e n u ,   f a l s e ) ;  
 	 	 	 m e n u . S h o w A s C o m b o L i s t ( b u t t o n ,   P o i n t . Z e r o ,   b u t t o n ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " L a y e r S e l e c t " ) ]  
 	 	 v o i d   C o m m a n d L a y e r S e l e c t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 s t r i n g   v a l u e   =   e . C o m m a n d S t a t e . A d v a n c e d S t a t e ;  
 	 	 	 i n t   s e l   =   S y s t e m . C o n v e r t . T o I n t 3 2   ( v a l u e ) ;  
 	 	 	 c o n t e x t . C u r r e n t L a y e r   =   s e l ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " L a y e r M i n i a t u r e s " ) ]  
 	 	 v o i d   C o m m a n d L a y e r M i n i a t u r e s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 d i . d o c u m e n t . L a y e r M i n i a t u r e s . I s P a n e l S h o w e d   =   ! d i . d o c u m e n t . L a y e r M i n i a t u r e s . I s P a n e l S h o w e d ;  
 	 	 	 d i . l a y e r P a n e . V i s i b i l i t y   =   d i . d o c u m e n t . L a y e r M i n i a t u r e s . I s P a n e l S h o w e d ;  
 	 	 	 d i . q u i c k L a y e r M i n i a t u r e s . G l y p h S h a p e   =   d i . d o c u m e n t . L a y e r M i n i a t u r e s . I s P a n e l S h o w e d   ?   G l y p h S h a p e . A r r o w R i g h t   :   G l y p h S h a p e . A r r o w L e f t ;  
 	 	 	 d i . l a y e r M i n i a t u r e s . U p d a t e L a y e r A f t e r C h a n g i n g ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " L a y e r N e w " ) ]  
 	 	 v o i d   C o m m a n d L a y e r N e w ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . L a y e r N e w ( r a n k + 1 ,   " " ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d ( " L a y e r D u p l i c a t e " ) ]  
 	 	 v o i d   C o m m a n d L a y e r D u p l i c a t e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . L a y e r D u p l i c a t e ( r a n k ,   r a n k + 1 ,   " " ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " L a y e r N e w S e l " ) ]  
 	 	 v o i d   C o m m a n d L a y e r N e w S e l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . L a y e r N e w S e l ( r a n k ,   " " ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " L a y e r M e r g e U p " ) ]  
 	 	 v o i d   C o m m a n d L a y e r M e r g e U p ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . L a y e r M e r g e ( r a n k ,   r a n k + 1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " L a y e r M e r g e D o w n " ) ]  
 	 	 v o i d   C o m m a n d L a y e r M e r g e D o w n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . L a y e r M e r g e ( r a n k ,   r a n k - 1 ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " L a y e r U p " ) ]  
 	 	 v o i d   C o m m a n d L a y e r U p ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . L a y e r S w a p ( r a n k ,   r a n k + 1 ) ;  
 	 	 	 c o n t e x t . C u r r e n t L a y e r   =   r a n k + 1 ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " L a y e r D o w n " ) ]  
 	 	 v o i d   C o m m a n d L a y e r D o w n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . L a y e r S w a p ( r a n k ,   r a n k - 1 ) ;  
 	 	 	 c o n t e x t . C u r r e n t L a y e r   =   r a n k - 1 ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " L a y e r D e l e t e " ) ]  
 	 	 v o i d   C o m m a n d L a y e r D e l e t e ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 i n t   r a n k   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . L a y e r D e l e t e ( r a n k ) ;  
 	 	 }  
  
  
 	 	 [ C o m m a n d   ( " D e b u g B b o x T h i n " ) ]  
 	 	 v o i d   C o m m a n d D e b u g B b o x T h i n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . I s D r a w B o x T h i n   =   ! c o n t e x t . I s D r a w B o x T h i n ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " D e b u g B b o x G e o m " ) ]  
 	 	 v o i d   C o m m a n d D e b u g B b o x G e o m ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . I s D r a w B o x G e o m   =   ! c o n t e x t . I s D r a w B o x G e o m ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " D e b u g B b o x F u l l " ) ]  
 	 	 v o i d   C o m m a n d D e b u g B b o x F u l l ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 c o n t e x t . I s D r a w B o x F u l l   =   ! c o n t e x t . I s D r a w B o x F u l l ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " D e b u g D i r t y " ) ]  
 	 	 v o i d   C o m m a n d D e b u g D i r t y ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D i r t y A l l V i e w s ( ) ;  
 	 	 }  
  
 	 	 [ C o m m a n d   ( " D e b u g C o p y F o n t s " ) ]  
 	 	 v o i d   C o m m a n d C o p y F o n t s ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 C l i p b o a r d W r i t e D a t a   d a t a ;  
 	 	 	 d a t a   =   n e w   C l i p b o a r d W r i t e D a t a   ( ) ;  
 	 	 	 d a t a . W r i t e T e x t ( C o m m o n . O p e n T y p e . F o n t C o l l e c t i o n . D e f a u l t . D e b u g G e t F u l l F o n t L i s t ( ) ) ;  
 	 	 	 C l i p b o a r d . S e t D a t a ( d a t a ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   Q u i t A p p l i c a t i o n ( )  
 	 	 {  
 	 	 	 / / 	 Q u i t t e   l ' a p p l i c a t i o n .  
 	 	 	 t h i s . W r i t e d G l o b a l S e t t i n g s ( ) ;  
 	 	 	 t h i s . D e l e t e T e m p o r a r y F i l e s ( ) ;  
 	 	 	  
 	 	 	 f o r e a c h   ( D o c u m e n t I n f o   d i   i n   t h i s . d o c u m e n t s )  
 	 	 	 {  
 	 	 	 	 d i . D i s p o s e   ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . d o c u m e n t s . C l e a r   ( ) ;  
 	 	 	 t h i s . c u r r e n t D o c u m e n t   =   - 1 ;  
 	 	 	  
 	 	 	 W i n d o w . Q u i t ( ) ;  
 	 	 }  
  
  
 	 	 p r o t e c t e d   V M e n u   C r e a t e P a g e s M e n u ( S u p p o r t . E v e n t H a n d l e r < M e s s a g e E v e n t A r g s >   m e s s a g e )  
 	 	 {  
 	 	 	 / / 	 C o n s t r u i t   l e   m e n u   p o u r   c h o i s i r   u n e   p a g e .  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 U n d o a b l e L i s t   p a g e s   =   t h i s . C u r r e n t D o c u m e n t . D o c u m e n t O b j e c t s ;     / /   l i s t e   d e s   p a g e s  
 	 	 	 r e t u r n   O b j e c t s . P a g e . C r e a t e M e n u ( p a g e s ,   c o n t e x t . C u r r e n t P a g e ,   " P a g e S e l e c t " ,   m e s s a g e ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   V M e n u   C r e a t e L a y e r s M e n u ( S u p p o r t . E v e n t H a n d l e r < M e s s a g e E v e n t A r g s >   m e s s a g e )  
 	 	 {  
 	 	 	 / / 	 C o n s t r u i t   l e   m e n u   p o u r   c h o i s i r   u n   c a l q u e .  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 O b j e c t s . A b s t r a c t   p a g e   =   c o n t e x t . R o o t O b j e c t ( 1 ) ;  
 	 	 	 U n d o a b l e L i s t   l a y e r s   =   p a g e . O b j e c t s ;     / /   l i s t e   d e s   c a l q u e s  
 	 	 	 r e t u r n   O b j e c t s . L a y e r . C r e a t e M e n u ( l a y e r s ,   c o n t e x t . C u r r e n t L a y e r ,   " L a y e r S e l e c t " ,   m e s s a g e ) ;  
 	 	 }  
  
  
  
 	 	 p r o t e c t e d   v o i d   I n i t C o m m a n d s ( )  
 	 	 {  
 	 	 	 / / 	 I n i t i a l i s e   t o u t e s   l e s   c o m m a n d e s .  
 	 	 	 t h i s . t o o l S e l e c t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o o l S e l e c t " ,   " T o o l S e l e c t " ,   " T o o l S e l e c t " ,   K e y C o d e . A l p h a S ) ;  
 	 	 	 t h i s . t o o l G l o b a l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o o l G l o b a l " ,   " T o o l G l o b a l " ,   " T o o l G l o b a l " ,   K e y C o d e . A l p h a G ) ;  
 	 	 	 t h i s . t o o l S h a p e r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o o l S h a p e r " ,   " T o o l S h a p e r " ,   " T o o l S h a p e r " ,   K e y C o d e . A l p h a A ) ;  
 	 	 	 t h i s . t o o l E d i t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o o l E d i t " ,   " T o o l E d i t " ,   " T o o l E d i t " ,   K e y C o d e . A l p h a E ) ;  
 	 	 	 t h i s . t o o l Z o o m S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o o l Z o o m " ,   " T o o l Z o o m " ,   " T o o l Z o o m " ,   K e y C o d e . A l p h a Z ) ;  
 	 	 	 t h i s . t o o l H a n d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o o l H a n d " ,   " T o o l H a n d " ,   " T o o l H a n d " ,   K e y C o d e . A l p h a H ) ;  
 	 	 	 t h i s . t o o l P i c k e r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o o l P i c k e r " ,   " T o o l P i c k e r " ,   " T o o l P i c k e r " ,   K e y C o d e . A l p h a I ) ;  
 	 	 	 t h i s . t o o l H o t S p o t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o o l H o t S p o t " ,   " T o o l H o t S p o t " ,   " T o o l H o t S p o t " ) ;  
 	 	 	 t h i s . t o o l L i n e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t L i n e " ,   " O b j e c t L i n e " ,   " T o o l L i n e " ,   K e y C o d e . A l p h a L ) ;  
 	 	 	 t h i s . t o o l R e c t a n g l e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t R e c t a n g l e " ,   " O b j e c t R e c t a n g l e " ,   " T o o l R e c t a n g l e " ,   K e y C o d e . A l p h a R ) ;  
 	 	 	 t h i s . t o o l C i r c l e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t C i r c l e " ,   " O b j e c t C i r c l e " ,   " T o o l C i r c l e " ,   K e y C o d e . A l p h a C ) ;  
 	 	 	 t h i s . t o o l E l l i p s e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t E l l i p s e " ,   " O b j e c t E l l i p s e " ,   " T o o l E l l i p s e " ) ;  
 	 	 	 t h i s . t o o l P o l y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t P o l y " ,   " O b j e c t P o l y " ,   " T o o l P o l y " ,   K e y C o d e . A l p h a P ) ;  
 	 	 	 t h i s . t o o l F r e e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t F r e e " ,   " O b j e c t F r e e " ,   " T o o l F r e e " ,   K e y C o d e . A l p h a M ) ;  
 	 	 	 t h i s . t o o l B e z i e r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t B e z i e r " ,   " O b j e c t B e z i e r " ,   " T o o l B e z i e r " ,   K e y C o d e . A l p h a B ) ;  
 	 	 	 t h i s . t o o l R e g u l a r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t R e g u l a r " ,   " O b j e c t R e g u l a r " ,   " T o o l R e g u l a r " ) ;  
 	 	 	 t h i s . t o o l S u r f a c e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t S u r f a c e " ,   " O b j e c t S u r f a c e " ,   " T o o l S u r f a c e " ) ;  
 	 	 	 t h i s . t o o l V o l u m e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t V o l u m e " ,   " O b j e c t V o l u m e " ,   " T o o l V o l u m e " ) ;  
 	 	 	 t h i s . t o o l T e x t L i n e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t T e x t L i n e " ,   " O b j e c t T e x t L i n e " ,   " T o o l T e x t L i n e " ) ;  
 	 	 	 t h i s . t o o l T e x t L i n e 2 S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t T e x t L i n e 2 " ,   " O b j e c t T e x t L i n e " ,   " T o o l T e x t L i n e " ) ;  
 	 	 	 t h i s . t o o l T e x t B o x S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t T e x t B o x " ,   " O b j e c t T e x t B o x " ,   " T o o l T e x t B o x " ) ;  
 	 	 	 t h i s . t o o l T e x t B o x 2 S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t T e x t B o x 2 " ,   " O b j e c t T e x t B o x " ,   " T o o l T e x t B o x " ,   K e y C o d e . A l p h a T ) ;  
 	 	 	 t h i s . t o o l A r r a y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t A r r a y " ,   " O b j e c t A r r a y " ,   " T o o l A r r a y " ) ;  
 	 	 	 t h i s . t o o l I m a g e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t I m a g e " ,   " O b j e c t I m a g e " ,   " T o o l I m a g e " ) ;  
 	 	 	 t h i s . t o o l D i m e n s i o n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O b j e c t D i m e n s i o n " ,   " O b j e c t D i m e n s i o n " ,   " T o o l D i m e n s i o n " ) ;  
  
 	 	 	 t h i s . n e w S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " N e w " ,   " N e w " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a N ) ;  
 	 	 	 t h i s . o p e n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O p e n " ,   " O p e n " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a O ) ;  
 	 	 	 t h i s . o p e n M o d e l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O p e n M o d e l " ,   " O p e n M o d e l " ,   K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a O ) ;  
 	 	 	 t h i s . s a v e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S a v e " ,   " S a v e " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a S ) ;  
 	 	 	 t h i s . s a v e A s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S a v e A s " ,   " S a v e A s " ) ;  
 	 	 	 t h i s . s a v e M o d e l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S a v e M o d e l " ,   " S a v e M o d e l " ) ;  
 	 	 	 t h i s . c l o s e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C l o s e " ,   n u l l ,   " C l o s e " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . F u n c F 4 ) ;  
 	 	 	 t h i s . c l o s e A l l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C l o s e A l l " ,   " C l o s e A l l " ) ;  
 	 	 	 t h i s . f o r c e S a v e A l l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " F o r c e S a v e A l l " ) ;  
 	 	 	 t h i s . o v e r w r i t e A l l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O v e r w r i t e A l l " ) ;  
 	 	 	 t h i s . n e x t D o c S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " N e x t D o c u m e n t " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . F u n c F 6 ) ;  
 	 	 	 t h i s . p r e v D o c S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P r e v D o c u m e n t " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . F u n c F 6 ) ;  
 	 	 	 t h i s . p r i n t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P r i n t " ,   " P r i n t " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a P ) ;  
 	 	 	 t h i s . e x p o r t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e   ( " E x p o r t " ,   " E x p o r t " ) ;  
 	 	 	 t h i s . q u i c k E x p o r t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e   ( " Q u i c k E x p o r t " ,   " Q u i c k E x p o r t " ) ;  
 	 	 	 t h i s . g l y p h s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " G l y p h s " ,   " G l y p h s " ,   K e y C o d e . F u n c F 7 ) ;  
 	 	 	 t h i s . g l y p h s I n s e r t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " G l y p h s I n s e r t " ) ;  
 	 	 	 t h i s . t e x t E d i t i n g S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T e x t E d i t i n g " ) ;  
 	 	 	 t h i s . r e p l a c e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R e p l a c e " ,   " R e p l a c e " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a F ) ;  
 	 	 	 t h i s . f i n d N e x t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " F i n d N e x t " ,   K e y C o d e . F u n c F 3 ) ;  
 	 	 	 t h i s . f i n d P r e v S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " F i n d P r e v " ,   K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . F u n c F 3 ) ;  
 	 	 	 t h i s . f i n d D e f N e x t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " F i n d D e f N e x t " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . F u n c F 3 ) ;  
 	 	 	 t h i s . f i n d D e f P r e v S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " F i n d D e f P r e v " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . F u n c F 3 ) ;  
 	 	 	 t h i s . d e l e t e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " D e l e t e " ,   " D e l e t e " ,   K e y C o d e . D e l e t e ) ;  
 	 	 	 t h i s . d u p l i c a t e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " D u p l i c a t e " ,   " D u p l i c a t e " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a D ) ;  
  
 	 	 	 t h i s . c u t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C u t " ,   " C u t " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a X ) ;  
 	 	 	 t h i s . c o p y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C o p y " ,   " C o p y " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a C ) ;  
 	 	 	 t h i s . p a s t e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a s t e " ,   " P a s t e " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a V ) ;  
  
 	 	 	 t h i s . f o n t B o l d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m o n . D o c u m e n t . R e s . C o m m a n d s . F o n t B o l d . C o m m a n d I d ,   " F o n t B o l d " ,   t r u e ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a B ) ;  
 	 	 	 t h i s . f o n t I t a l i c S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e   ( C o m m o n . D o c u m e n t . R e s . C o m m a n d s . F o n t I t a l i c . C o m m a n d I d ,   " F o n t I t a l i c " ,   t r u e ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a I ) ;  
 	 	 	 t h i s . f o n t U n d e r l i n e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e   ( C o m m o n . D o c u m e n t . R e s . C o m m a n d s . F o n t U n d e r l i n e . C o m m a n d I d ,   " F o n t U n d e r l i n e " ,   t r u e ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a U ) ;  
 	 	 	 t h i s . f o n t O v e r l i n e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m a n d s . F o n t O v e r l i n e ,   " F o n t O v e r l i n e " ,   t r u e ) ;  
 	 	 	 t h i s . f o n t S t r i k e o u t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m a n d s . F o n t S t r i k e o u t ,   " F o n t S t r i k e o u t " ,   t r u e ) ;  
 	 	 	 t h i s . f o n t S u b s c r i p t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m a n d s . F o n t S u b s c r i p t ,   " F o n t S u b s c r i p t " ,   t r u e ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a G ) ;  
 	 	 	 t h i s . f o n t S u p e r s c r i p t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m a n d s . F o n t S u p e r s c r i p t ,   " F o n t S u p e r s c r i p t " ,   t r u e ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a T ) ;  
 	 	 	 t h i s . f o n t S i z e P l u s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m a n d s . F o n t S i z e P l u s ,   " F o n t S i z e P l u s " ) ;  
 	 	 	 t h i s . f o n t S i z e M i n u s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m a n d s . F o n t S i z e M i n u s ,   " F o n t S i z e M i n u s " ) ;  
 	 	 	 t h i s . f o n t C l e a r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m a n d s . F o n t C l e a r ,   " F o n t C l e a r " ) ;  
 	 	 	 t h i s . p a r a g r a p h L e a d i n g S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a r a g r a p h L e a d i n g " ,   " P a r a g r a p h L e a d i n g " ,   t r u e ) ;  
 	 	 	 t h i s . p a r a g r a p h L e a d i n g P l u s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a r a g r a p h L e a d i n g P l u s " ,   " P a r a g r a p h L e a d i n g P l u s " ) ;  
 	 	 	 t h i s . p a r a g r a p h L e a d i n g M i n u s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a r a g r a p h L e a d i n g M i n u s " ,   " P a r a g r a p h L e a d i n g M i n u s " ) ;  
 	 	 	 t h i s . p a r a g r a p h I n d e n t P l u s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a r a g r a p h I n d e n t P l u s " ,   " P a r a g r a p h I n d e n t P l u s " ) ;  
 	 	 	 t h i s . p a r a g r a p h I n d e n t M i n u s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a r a g r a p h I n d e n t M i n u s " ,   " P a r a g r a p h I n d e n t M i n u s " ) ;  
 	 	 	 t h i s . p a r a g r a p h J u s t i f S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a r a g r a p h J u s t i f " ,   n u l l ,   " P a r a g r a p h J u s t i f " ,   t r u e ) ;  
 	 	 	 t h i s . p a r a g r a p h C l e a r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a r a g r a p h C l e a r " ,   " P a r a g r a p h C l e a r " ) ;  
  
 	 	 	 t h i s . o r d e r U p O n e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O r d e r U p O n e " ,   " O r d e r U p O n e " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . P a g e U p ) ;  
 	 	 	 t h i s . o r d e r D o w n O n e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O r d e r D o w n O n e " ,   " O r d e r D o w n O n e " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . P a g e D o w n ) ;  
 	 	 	 t h i s . o r d e r U p A l l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O r d e r U p A l l " ,   " O r d e r U p A l l " ,   K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . P a g e U p ) ;  
 	 	 	 t h i s . o r d e r D o w n A l l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O r d e r D o w n A l l " ,   " O r d e r D o w n A l l " ,   K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . P a g e D o w n ) ;  
 	 	 	  
 	 	 	 t h i s . m o v e L e f t F r e e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e L e f t F r e e " ,   " M o v e H i " ,   " M o v e L e f t " ) ;  
 	 	 	 t h i s . m o v e R i g h t F r e e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e R i g h t F r e e " ,   " M o v e H " ,   " M o v e R i g h t " ) ;  
 	 	 	 t h i s . m o v e U p F r e e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e U p F r e e " ,   " M o v e V " ,   " M o v e U p " ) ;  
 	 	 	 t h i s . m o v e D o w n F r e e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e D o w n F r e e " ,   " M o v e V i " ,   " M o v e D o w n " ) ;  
 	 	 	  
 	 	 	 t h i s . r o t a t e 9 0 S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R o t a t e 9 0 " ,   " R o t a t e 9 0 " ) ;  
 	 	 	 t h i s . r o t a t e 1 8 0 S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R o t a t e 1 8 0 " ,   " R o t a t e 1 8 0 " ) ;  
 	 	 	 t h i s . r o t a t e 2 7 0 S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R o t a t e 2 7 0 " ,   " R o t a t e 2 7 0 " ) ;  
 	 	 	 t h i s . r o t a t e F r e e C C W S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R o t a t e F r e e C C W " ,   " R o t a t e F r e e C C W " ) ;  
 	 	 	 t h i s . r o t a t e F r e e C W S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R o t a t e F r e e C W " ,   " R o t a t e F r e e C W " ) ;  
 	 	 	  
 	 	 	 t h i s . m i r r o r H S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " M i r r o r H " ,   " M i r r o r H " ) ;  
 	 	 	 t h i s . m i r r o r V S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " M i r r o r V " ,   " M i r r o r V " ) ;  
  
 	 	 	 t h i s . s c a l e M u l 2 S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S c a l e M u l 2 " ,   " S c a l e M u l 2 " ) ;  
 	 	 	 t h i s . s c a l e D i v 2 S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S c a l e D i v 2 " ,   " S c a l e D i v 2 " ) ;  
 	 	 	 t h i s . s c a l e M u l F r e e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S c a l e M u l F r e e " ,   " S c a l e M u l F r e e " ) ;  
 	 	 	 t h i s . s c a l e D i v F r e e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S c a l e D i v F r e e " ,   " S c a l e D i v F r e e " ) ;  
  
 	 	 	 t h i s . a l i g n L e f t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A l i g n L e f t " ,   " A l i g n L e f t " ) ;  
 	 	 	 t h i s . a l i g n C e n t e r X S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A l i g n C e n t e r X " ,   " A l i g n C e n t e r X " ) ;  
 	 	 	 t h i s . a l i g n R i g h t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A l i g n R i g h t " ,   " A l i g n R i g h t " ) ;  
 	 	 	 t h i s . a l i g n T o p S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A l i g n T o p " ,   " A l i g n T o p " ) ;  
 	 	 	 t h i s . a l i g n C e n t e r Y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A l i g n C e n t e r Y " ,   " A l i g n C e n t e r Y " ) ;  
 	 	 	 t h i s . a l i g n B o t t o m S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A l i g n B o t t o m " ,   " A l i g n B o t t o m " ) ;  
 	 	 	 t h i s . a l i g n G r i d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A l i g n G r i d " ,   " A l i g n G r i d " ) ;  
  
 	 	 	 t h i s . r e s e t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R e s e t " ,   " R e s e t " ) ;  
  
 	 	 	 t h i s . s h a r e L e f t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a r e L e f t " ,   " S h a r e L e f t " ) ;  
 	 	 	 t h i s . s h a r e C e n t e r X S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a r e C e n t e r X " ,   " S h a r e C e n t e r X " ) ;  
 	 	 	 t h i s . s h a r e S p a c e X S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a r e S p a c e X " ,   " S h a r e S p a c e X " ) ;  
 	 	 	 t h i s . s h a r e R i g h t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a r e R i g h t " ,   " S h a r e R i g h t " ) ;  
 	 	 	 t h i s . s h a r e T o p S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a r e T o p " ,   " S h a r e T o p " ) ;  
 	 	 	 t h i s . s h a r e C e n t e r Y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a r e C e n t e r Y " ,   " S h a r e C e n t e r Y " ) ;  
 	 	 	 t h i s . s h a r e S p a c e Y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a r e S p a c e Y " ,   " S h a r e S p a c e Y " ) ;  
 	 	 	 t h i s . s h a r e B o t t o m S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a r e B o t t o m " ,   " S h a r e B o t t o m " ) ;  
  
 	 	 	 t h i s . a d j u s t W i d t h S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A d j u s t W i d t h " ,   " A d j u s t W i d t h " ) ;  
 	 	 	 t h i s . a d j u s t H e i g h t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A d j u s t H e i g h t " ,   " A d j u s t H e i g h t " ) ;  
  
 	 	 	 t h i s . c o l o r T o R g b S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C o l o r T o R g b " ,   " C o l o r T o R g b " ) ;  
 	 	 	 t h i s . c o l o r T o C m y k S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C o l o r T o C m y k " ,   " C o l o r T o C m y k " ) ;  
 	 	 	 t h i s . c o l o r T o G r a y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C o l o r T o G r a y " ,   " C o l o r T o G r a y " ) ;  
 	 	 	 t h i s . c o l o r S t r o k e D a r k S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C o l o r S t r o k e D a r k " ,   " C o l o r S t r o k e D a r k " ) ;  
 	 	 	 t h i s . c o l o r S t r o k e L i g h t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C o l o r S t r o k e L i g h t " ,   " C o l o r S t r o k e L i g h t " ) ;  
 	 	 	 t h i s . c o l o r F i l l D a r k S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C o l o r F i l l D a r k " ,   " C o l o r F i l l D a r k " ) ;  
 	 	 	 t h i s . c o l o r F i l l L i g h t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C o l o r F i l l L i g h t " ,   " C o l o r F i l l L i g h t " ) ;  
  
 	 	 	 t h i s . m e r g e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " M e r g e " ,   " M e r g e " ) ;  
 	 	 	 t h i s . e x t r a c t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " E x t r a c t " ,   " E x t r a c t " ) ;  
 	 	 	 t h i s . g r o u p S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " G r o u p " ,   " G r o u p " ) ;  
 	 	 	 t h i s . u n g r o u p S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " U n g r o u p " ,   " U n g r o u p " ) ;  
 	 	 	 t h i s . i n s i d e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " I n s i d e " ,   " I n s i d e " ) ;  
 	 	 	 t h i s . o u t s i d e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " O u t s i d e " ,   " O u t s i d e " ) ;  
 	 	 	 t h i s . c o m b i n e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C o m b i n e " ,   " C o m b i n e " ) ;  
 	 	 	 t h i s . u n c o m b i n e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " U n c o m b i n e " ,   " U n c o m b i n e " ) ;  
 	 	 	 t h i s . t o B e z i e r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o B e z i e r " ,   " T o B e z i e r " ) ;  
 	 	 	 t h i s . t o P o l y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o P o l y " ,   " T o P o l y " ) ;  
 	 	 	 t h i s . t o T e x t B o x 2 S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T o T e x t B o x 2 " ,   " O b j e c t T e x t B o x " ,   " T o T e x t B o x 2 " ) ;  
 	 	 	 t h i s . f r a g m e n t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " F r a g m e n t " ,   " F r a g m e n t " ) ;  
  
 	 	 	 t h i s . s h a p e r H a n d l e A d d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e A d d " ,   " S h a p e r H a n d l e A d d " ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e S u b S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e S u b " ,   " S h a p e r H a n d l e S u b " ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e T o L i n e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e T o L i n e " ,   " S h a p e r H a n d l e T o L i n e " ,   t r u e ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e T o C u r v e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e T o C u r v e " ,   " S h a p e r H a n d l e T o C u r v e " ,   t r u e ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e S y m S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e S y m " ,   " S h a p e r H a n d l e S y m " ,   t r u e ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e S m o o t h S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e S m o o t h " ,   " S h a p e r H a n d l e S m o o t h " ,   t r u e ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e D i s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e D i s " ,   " S h a p e r H a n d l e D i s " ,   t r u e ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e I n l i n e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e I n l i n e " ,   " S h a p e r H a n d l e I n l i n e " ,   t r u e ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e F r e e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e F r e e " ,   " S h a p e r H a n d l e F r e e " ,   t r u e ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e S i m p l y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e S i m p l y " ,   " S h a p e r H a n d l e S i m p l y " ,   t r u e ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e C o r n e r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e C o r n e r " ,   " S h a p e r H a n d l e C o r n e r " ,   t r u e ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e C o n t i n u e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e C o n t i n u e " ,   " S h a p e r H a n d l e C o n t i n u e " ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e S h a r p S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e S h a r p " ,   " S h a p e r H a n d l e S h a r p " ,   t r u e ) ;  
 	 	 	 t h i s . s h a p e r H a n d l e R o u n d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S h a p e r H a n d l e R o u n d " ,   " S h a p e r H a n d l e R o u n d " ,   t r u e ) ;  
  
 	 	 	 t h i s . b o o l e a n A n d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " B o o l e a n A n d " ,   " B o o l e a n A n d " ) ;  
 	 	 	 t h i s . b o o l e a n O r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " B o o l e a n O r " ,   " B o o l e a n O r " ) ;  
 	 	 	 t h i s . b o o l e a n X o r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " B o o l e a n X o r " ,   " B o o l e a n X o r " ) ;  
 	 	 	 t h i s . b o o l e a n F r o n t M i n u s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " B o o l e a n F r o n t M i n u s " ,   " B o o l e a n F r o n t M i n u s " ) ;  
 	 	 	 t h i s . b o o l e a n B a c k M i n u s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " B o o l e a n B a c k M i n u s " ,   " B o o l e a n B a c k M i n u s " ) ;  
  
 	 	 	 t h i s . u n d o S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " U n d o " ,   " U n d o " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a Z ) ;  
 	 	 	 t h i s . r e d o S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R e d o " ,   " R e d o " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a Y ) ;  
 	 	 	 t h i s . u n d o R e d o L i s t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " U n d o R e d o L i s t " ) ;  
  
 	 	 	 t h i s . d e s e l e c t A l l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m o n . W i d g e t s . R e s . C o m m a n d s . D e s e l e c t A l l . C o m m a n d I d ,   " D e s e l e c t A l l " ,   K e y C o d e . E s c a p e ) ;  
 	 	 	 t h i s . s e l e c t A l l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m o n . W i d g e t s . R e s . C o m m a n d s . S e l e c t A l l . C o m m a n d I d ,   " S e l e c t A l l " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A l p h a A ) ;  
 	 	 	 t h i s . s e l e c t I n v e r t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e l e c t I n v e r t " ,   " S e l e c t I n v e r t " ) ;  
 	 	 	 t h i s . s e l e c t o r A u t o S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e l e c t o r A u t o " ,   " S e l e c t o r A u t o " ) ;  
 	 	 	 t h i s . s e l e c t o r I n d i v i d u a l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e l e c t o r I n d i v i d u a l " ,   " S e l e c t o r I n d i v i d u a l " ) ;  
 	 	 	 t h i s . s e l e c t o r S c a l e r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e l e c t o r S c a l e r " ,   " S e l e c t o r S c a l e r " ) ;  
 	 	 	 t h i s . s e l e c t o r S t r e t c h S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e l e c t o r S t r e t c h " ,   " S e l e c t o r S t r e t c h " ) ;  
 	 	 	 t h i s . s e l e c t o r S t r e t c h T y p e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e l e c t o r S t r e t c h T y p e " ) ;  
 	 	 	 t h i s . s e l e c t T o t a l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e l e c t T o t a l " ,   " S e l e c t T o t a l " ) ;  
 	 	 	 t h i s . s e l e c t P a r t i a l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e l e c t P a r t i a l " ,   " S e l e c t P a r t i a l " ) ;  
 	 	 	 t h i s . s e l e c t o r A d a p t L i n e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e l e c t o r A d a p t L i n e " ,   " S e l e c t o r A d a p t L i n e " ) ;  
 	 	 	 t h i s . s e l e c t o r A d a p t T e x t   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e l e c t o r A d a p t T e x t " ,   " S e l e c t o r A d a p t T e x t " ) ;  
  
 	 	 	 t h i s . h i d e H a l f S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " H i d e H a l f " ,   " H i d e H a l f " ,   t r u e ) ;  
 	 	 	 t h i s . h i d e S e l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " H i d e S e l " ,   " H i d e S e l " ) ;  
 	 	 	 t h i s . h i d e R e s t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " H i d e R e s t " ,   " H i d e R e s t " ) ;  
 	 	 	 t h i s . h i d e C a n c e l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " H i d e C a n c e l " ,   " H i d e C a n c e l " ) ;  
  
 	 	 	 t h i s . z o o m M i n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " Z o o m M i n " ,   " Z o o m M i n " ,   t r u e ) ;  
 	 	 	 t h i s . z o o m P a g e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " Z o o m P a g e " ,   " Z o o m P a g e " ,   t r u e ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . D i g i t 0 ) ;  
 	 	 	 t h i s . z o o m P a g e W i d t h S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " Z o o m P a g e W i d t h " ,   " Z o o m P a g e W i d t h " ,   t r u e ) ;  
 	 	 	 t h i s . z o o m D e f a u l t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " Z o o m D e f a u l t " ,   " Z o o m D e f a u l t " ,   t r u e ) ;  
 	 	 	 t h i s . z o o m S e l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " Z o o m S e l " ,   " Z o o m S e l " ) ;  
 	 	 	 t h i s . z o o m S e l W i d t h S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " Z o o m S e l W i d t h " ,   " Z o o m S e l W i d t h " ) ;  
 	 	 	 t h i s . z o o m P r e v S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " Z o o m P r e v " ,   " Z o o m P r e v " ) ;  
 	 	 	 t h i s . z o o m S u b S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " Z o o m S u b " ,   " Z o o m S u b " ,   K e y C o d e . N u m e r i c S u b t r a c t ) ;  
 	 	 	 t h i s . z o o m A d d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " Z o o m A d d " ,   " Z o o m A d d " ,   K e y C o d e . N u m e r i c A d d ) ;  
  
 	 	 	 t h i s . p r e v i e w S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P r e v i e w " ,   " P r e v i e w " ,   t r u e ) ;  
 	 	 	 t h i s . g r i d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " G r i d " ,   " G r i d " ,   t r u e ) ;  
 	 	 	 t h i s . t e x t G r i d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T e x t G r i d " ,   " T e x t G r i d " ,   t r u e ) ;  
 	 	 	 t h i s . t e x t S h o w C o n t r o l C h a r a c t e r s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( C o m m a n d s . T e x t S h o w C o n t r o l C h a r a c t e r s ,   " T e x t S h o w C o n t r o l C h a r a c t e r s " ,   t r u e ) ;  
 	 	 	 t h i s . t e x t F o n t F i l t e r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T e x t F o n t F i l t e r " ,   " T e x t F o n t F i l t e r " ,   t r u e ) ;  
 	 	 	 t h i s . t e x t F o n t S a m p l e A b c S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T e x t F o n t S a m p l e A b c " ,   " T e x t F o n t S a m p l e A b c " ,   t r u e ) ;  
 	 	 	 t h i s . t e x t I n s e r t Q u a d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T e x t I n s e r t Q u a d " ,   " T e x t I n s e r t Q u a d " ) ;  
 	 	 	 t h i s . t e x t I n s e r t N e w F r a m e E d g e s   =   t h i s . C r e a t e C o m m a n d S t a t e   ( " T e x t I n s e r t N e w F r a m e " ,   " T e x t I n s e r t N e w F r a m e " ) ;  
 	 	 	 t h i s . t e x t I n s e r t N e w P a g e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " T e x t I n s e r t N e w P a g e " ,   " T e x t I n s e r t N e w P a g e " ) ;  
 	 	 	 t h i s . c o n s t r a i n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " C o n s t r a i n " ,   " C o n s t r a i n " ,   t r u e ) ;  
 	 	 	 t h i s . m a g n e t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " M a g n e t " ,   " M a g n e t " ,   t r u e ) ;  
 	 	 	 t h i s . m a g n e t L a y e r S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " M a g n e t L a y e r " ,   " M a g n e t L a y e r " ,   t r u e ) ;  
 	 	 	 t h i s . r u l e r s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R u l e r s " ,   " R u l e r s " ,   t r u e ) ;  
 	 	 	 t h i s . l a b e l s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a b e l s " ,   " L a b e l s " ,   t r u e ) ;  
 	 	 	 t h i s . a g g r e g a t e s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A g g r e g a t e s " ,   " A g g r e g a t e s " ,   t r u e ) ;  
  
 	 	 	 t h i s . a r r a y O u t l i n e F r a m e E d g e s   =   t h i s . C r e a t e C o m m a n d S t a t e   ( " A r r a y O u t l i n e F r a m e " ) ;  
 	 	 	 t h i s . a r r a y O u t l i n e H o r i z S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y O u t l i n e H o r i z " ) ;  
 	 	 	 t h i s . a r r a y O u t l i n e V e r t i S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y O u t l i n e V e r t i " ) ;  
 	 	 	 t h i s . a r r a y A d d C o l u m n L e f t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y A d d C o l u m n L e f t " ) ;  
 	 	 	 t h i s . a r r a y A d d C o l u m n R i g h t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y A d d C o l u m n R i g h t " ) ;  
 	 	 	 t h i s . a r r a y A d d R o w T o p S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y A d d R o w T o p " ) ;  
 	 	 	 t h i s . a r r a y A d d R o w B o t t o m S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y A d d R o w B o t t o m " ) ;  
 	 	 	 t h i s . a r r a y D e l C o l u m n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y D e l C o l u m n " ) ;  
 	 	 	 t h i s . a r r a y D e l R o w S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y D e l R o w " ) ;  
 	 	 	 t h i s . a r r a y A l i g n C o l u m n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y A l i g n C o l u m n " ) ;  
 	 	 	 t h i s . a r r a y A l i g n R o w S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y A l i g n R o w " ) ;  
 	 	 	 t h i s . a r r a y S w a p C o l u m n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y S w a p C o l u m n " ) ;  
 	 	 	 t h i s . a r r a y S w a p R o w S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y S w a p R o w " ) ;  
 	 	 	 t h i s . a r r a y L o o k S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A r r a y L o o k " ) ;  
  
 # i f   f a l s e  
 	 	 	 t h i s . r e s D e s i g n e r B u i l d S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R e s D e s i g n e r B u i l d " ) ;  
 	 	 	 t h i s . r e s D e s i g n e r T r a n s l a t e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " R e s D e s i g n e r T r a n s l a t e " ) ;  
 # e n d i f  
 	 	 	 t h i s . d e b u g B b o x T h i n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " D e b u g B b o x T h i n " ) ;  
 	 	 	 t h i s . d e b u g B b o x G e o m S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " D e b u g B b o x G e o m " ) ;  
 	 	 	 t h i s . d e b u g B b o x F u l l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " D e b u g B b o x F u l l " ) ;  
 	 	 	 t h i s . d e b u g D i r t y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " D e b u g D i r t y " ,   K e y C o d e . F u n c F 1 2 ) ;  
 	 	 	 t h i s . d e b u g C o p y F o n t s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " D e b u g C o p y F o n t s " ,   K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . A l p h a F ) ;  
  
 	 	 	 t h i s . p a g e P r e v S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a g e P r e v " ,   K e y C o d e . P a g e U p ) ;  
 	 	 	 t h i s . p a g e N e x t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a g e N e x t " ,   K e y C o d e . P a g e D o w n ) ;  
 	 	 	 t h i s . p a g e M e n u S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a g e M e n u " ) ;  
 	 	 	 t h i s . p a g e M i n i a t u r e s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a g e M i n i a t u r e s " ) ;  
 	 	 	 t h i s . p a g e N e w S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a g e N e w " ,   " P a g e N e w " ) ;  
 	 	 	 t h i s . p a g e D u p l i c a t e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a g e D u p l i c a t e " ) ;  
 	 	 	 t h i s . p a g e U p S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a g e U p " ) ;  
 	 	 	 t h i s . p a g e D o w n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a g e D o w n " ) ;  
 	 	 	 t h i s . p a g e D e l e t e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a g e D e l e t e " ) ;  
  
 	 	 	 t h i s . l a y e r P r e v S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r P r e v " ) ;  
 	 	 	 t h i s . l a y e r N e x t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r N e x t " ) ;  
 	 	 	 t h i s . l a y e r M e n u S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r M e n u " ) ;  
 	 	 	 t h i s . l a y e r M i n i a t u r e s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r M i n i a t u r e s " ) ;  
 	 	 	 t h i s . l a y e r N e w S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r N e w " ,   " L a y e r N e w " ) ;  
 	 	 	 t h i s . l a y e r D u p l i c a t e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r D u p l i c a t e " ) ;  
 	 	 	 t h i s . l a y e r N e w S e l S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r N e w S e l " ,   " L a y e r N e w S e l " ) ;  
 	 	 	 t h i s . l a y e r M e r g e U p S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r M e r g e U p " ,   " L a y e r M e r g e U p " ) ;  
 	 	 	 t h i s . l a y e r M e r g e D o w n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r M e r g e D o w n " ,   " L a y e r M e r g e D o w n " ) ;  
 	 	 	 t h i s . l a y e r U p S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r U p " ) ;  
 	 	 	 t h i s . l a y e r D o w n S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r D o w n " ) ;  
 	 	 	 t h i s . l a y e r D e l e t e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " L a y e r D e l e t e " ) ;  
  
 	 	 	 t h i s . s e t t i n g s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " S e t t i n g s " ,   " S e t t i n g s " ,   K e y C o d e . F u n c F 5 ) ;  
 	 	 	 t h i s . i n f o s S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " I n f o s " ,   " I n f o s " ) ;  
 	 	 	 t h i s . a b o u t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " A b o u t A p p l i c a t i o n " ,   " A b o u t " ,   " A b o u t " ) ;  
 	 	 	 t h i s . p a g e S t a c k S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " P a g e S t a c k " ,   " P a g e S t a c k " ) ;  
 	 	 	 t h i s . u p d a t e S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " U p d a t e A p p l i c a t i o n " ,   " U p d a t e " ,   " U p d a t e " ) ;  
 	 	 	 t h i s . k e y S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " K e y A p p l i c a t i o n " ,   " K e y " ,   " K e y " ) ;  
  
 	 	 	 t h i s . m o v e L e f t N o r m S t a t e       =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e L e f t N o r m " ,       K e y C o d e . A r r o w L e f t ) ;  
 	 	 	 t h i s . m o v e R i g h t N o r m S t a t e     =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e R i g h t N o r m " ,     K e y C o d e . A r r o w R i g h t ) ;  
 	 	 	 t h i s . m o v e U p N o r m S t a t e           =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e U p N o r m " ,           K e y C o d e . A r r o w U p ) ;  
 	 	 	 t h i s . m o v e D o w n N o r m S t a t e       =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e D o w n N o r m " ,       K e y C o d e . A r r o w D o w n ) ;  
 	 	 	 t h i s . m o v e L e f t C t r l S t a t e       =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e L e f t C t r l " ,       K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A r r o w L e f t ) ;  
 	 	 	 t h i s . m o v e R i g h t C t r l S t a t e     =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e R i g h t C t r l " ,     K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A r r o w R i g h t ) ;  
 	 	 	 t h i s . m o v e U p C t r l S t a t e           =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e U p C t r l " ,           K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A r r o w U p ) ;  
 	 	 	 t h i s . m o v e D o w n C t r l S t a t e       =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e D o w n C t r l " ,       K e y C o d e . M o d i f i e r C o n t r o l | K e y C o d e . A r r o w D o w n ) ;  
 	 	 	 t h i s . m o v e L e f t S h i f t S t a t e     =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e L e f t S h i f t " ,     K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . A r r o w L e f t ) ;  
 	 	 	 t h i s . m o v e R i g h t S h i f t S t a t e   =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e R i g h t S h i f t " ,   K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . A r r o w R i g h t ) ;  
 	 	 	 t h i s . m o v e U p S h i f t S t a t e         =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e U p S h i f t " ,         K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . A r r o w U p ) ;  
 	 	 	 t h i s . m o v e D o w n S h i f t S t a t e     =   t h i s . C r e a t e C o m m a n d S t a t e ( " M o v e D o w n S h i f t " ,     K e y C o d e . M o d i f i e r S h i f t | K e y C o d e . A r r o w D o w n ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   C o m m a n d S t a t e   C r e a t e C o m m a n d S t a t e ( s t r i n g   c o m m a n d N a m e ,   p a r a m s   S h o r t c u t [ ]   s h o r t c u t s )  
 	 	 {  
 	 	 	 r e t u r n   t h i s . C r e a t e C o m m a n d S t a t e ( c o m m a n d N a m e ,   n u l l ,   c o m m a n d N a m e ,   s h o r t c u t s ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   C o m m a n d S t a t e   C r e a t e C o m m a n d S t a t e ( s t r i n g   c o m m a n d N a m e ,   b o o l   s t a t e f u l l ,   p a r a m s   S h o r t c u t [ ]   s h o r t c u t s )  
 	 	 {  
 	 	 	 r e t u r n   t h i s . C r e a t e C o m m a n d S t a t e ( c o m m a n d N a m e ,   n u l l ,   c o m m a n d N a m e ,   s t a t e f u l l ,   s h o r t c u t s ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   C o m m a n d S t a t e   C r e a t e C o m m a n d S t a t e ( s t r i n g   c o m m a n d N a m e ,   s t r i n g   i c o n ,   b o o l   s t a t e f u l l ,   p a r a m s   S h o r t c u t [ ]   s h o r t c u t s )  
 	 	 {  
 	 	 	 r e t u r n   t h i s . C r e a t e C o m m a n d S t a t e ( c o m m a n d N a m e ,   i c o n ,   c o m m a n d N a m e ,   s t a t e f u l l ,   s h o r t c u t s ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   C o m m a n d S t a t e   C r e a t e C o m m a n d S t a t e ( s t r i n g   c o m m a n d N a m e ,   s t r i n g   i c o n ,   p a r a m s   S h o r t c u t [ ]   s h o r t c u t s )  
 	 	 {  
 	 	 	 r e t u r n   t h i s . C r e a t e C o m m a n d S t a t e ( c o m m a n d N a m e ,   i c o n ,   c o m m a n d N a m e ,   f a l s e ,   s h o r t c u t s ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   C o m m a n d S t a t e   C r e a t e C o m m a n d S t a t e ( s t r i n g   c o m m a n d N a m e ,   s t r i n g   i c o n ,   s t r i n g   t o o l t i p ,   p a r a m s   S h o r t c u t [ ]   s h o r t c u t s )  
 	 	 {  
 	 	 	 r e t u r n   t h i s . C r e a t e C o m m a n d S t a t e ( c o m m a n d N a m e ,   i c o n ,   t o o l t i p ,   f a l s e ,   s h o r t c u t s ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   C o m m a n d S t a t e   C r e a t e C o m m a n d S t a t e ( s t r i n g   c o m m a n d N a m e ,   s t r i n g   i c o n ,   s t r i n g   t o o l t i p ,   b o o l   s t a t e f u l l ,   p a r a m s   S h o r t c u t [ ]   s h o r t c u t s )  
 	 	 {  
 	 	 	 / / 	 C r é e   u n   n o u v e a u   C o m m a n d   +   C o m m a n d S t a t e .  
 	 	 	 C o m m a n d   c o m m a n d   =   E p s i t e c . C o m m o n . W i d g e t s . C o m m a n d . G e t ( c o m m a n d N a m e ) ;  
  
 	 	 	 i f   ( c o m m a n d . I s R e a d W r i t e )  
 	 	 	 {  
 	 	 	 	 i f   ( s h o r t c u t s . L e n g t h   >   0 )  
 	 	 	 	 {  
 	 	 	 	 	 c o m m a n d . S h o r t c u t s . A d d R a n g e ( s h o r t c u t s ) ;  
 	 	 	 	 }  
  
 	 	 	 	 s t r i n g   d e s c r i p t i o n   =   D o c u m e n t E d i t o r . G e t R e s ( " A c t i o n . " + t o o l t i p ) ;  
  
 	 	 	 	 c o m m a n d . M a n u a l l y D e f i n e C o m m a n d ( d e s c r i p t i o n ,   M i s c . I c o n ( i c o n ) ,   n u l l ,   s t a t e f u l l ) ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   t h i s . C o m m a n d C o n t e x t . G e t C o m m a n d S t a t e ( c o m m a n d ) ;  
 	 	 }  
  
  
 	 	 p r o t e c t e d   v o i d   C o n n e c t E v e n t s ( )  
 	 	 {  
 	 	 	 / / 	 O n   s ' e n r e g i s t r e   a u p r è s   d u   d o c u m e n t   p o u r   t o u s   l e s   é v é n e m e n t s .  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . D o c u m e n t C h a n g e d                 + =   t h i s . H a n d l e D o c u m e n t C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . M o u s e C h a n g e d                       + =   t h i s . H a n d l e M o u s e C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . M o d i f C h a n g e d                       + =   t h i s . H a n d l e M o d i f C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . O r i g i n C h a n g e d                     + =   t h i s . H a n d l e O r i g i n C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . Z o o m C h a n g e d                         + =   t h i s . H a n d l e Z o o m C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . T o o l C h a n g e d                         + =   t h i s . H a n d l e T o o l C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . S a v e C h a n g e d                         + =   t h i s . H a n d l e S a v e C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . S e l e c t i o n C h a n g e d               + =   t h i s . H a n d l e S e l e c t i o n C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . G e o m e t r y C h a n g e d                 + =   t h i s . H a n d l e G e o m e t r y C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . S h a p e r C h a n g e d                     + =   t h i s . H a n d l e S h a p e r C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . T e x t C h a n g e d                         + =   t h i s . H a n d l e T e x t C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . S t y l e C h a n g e d                       + =   t h i s . H a n d l e S t y l e C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . P a g e s C h a n g e d                       + =   t h i s . H a n d l e P a g e s C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . L a y e r s C h a n g e d                     + =   t h i s . H a n d l e L a y e r s C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . P a g e C h a n g e d                         + =   t h i s . H a n d l e P a g e C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . L a y e r C h a n g e d                       + =   t h i s . H a n d l e L a y e r C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . U n d o R e d o C h a n g e d                 + =   t h i s . H a n d l e U n d o R e d o C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . G r i d C h a n g e d                         + =   t h i s . H a n d l e G r i d C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . L a b e l P r o p e r t i e s C h a n g e d   + =   t h i s . H a n d l e L a b e l P r o p e r t i e s C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . C o n s t r a i n C h a n g e d               + =   t h i s . H a n d l e C o n s t r a i n C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . M a g n e t C h a n g e d                     + =   t h i s . H a n d l e M a g n e t C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . P r e v i e w C h a n g e d                   + =   t h i s . H a n d l e P r e v i e w C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . S e t t i n g s C h a n g e d                 + =   t h i s . H a n d l e S e t t i n g s C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . F o n t s S e t t i n g s C h a n g e d       + =   t h i s . H a n d l e F o n t s S e t t i n g s C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . G u i d e s C h a n g e d                     + =   t h i s . H a n d l e G u i d e s C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . H i d e H a l f C h a n g e d                 + =   t h i s . H a n d l e H i d e H a l f C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . D e b u g C h a n g e d                       + =   t h i s . H a n d l e D e b u g C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . P r o p e r t y C h a n g e d                 + =   t h i s . H a n d l e P r o p e r t y C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . A g g r e g a t e C h a n g e d               + =   t h i s . H a n d l e A g g r e g a t e C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . T e x t S t y l e C h a n g e d               + =   t h i s . H a n d l e T e x t S t y l e C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . T e x t S t y l e L i s t C h a n g e d       + =   t h i s . H a n d l e T e x t S t y l e L i s t C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . S e l N a m e s C h a n g e d                 + =   t h i s . H a n d l e S e l N a m e s C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . D r a w C h a n g e d                         + =   t h i s . H a n d l e D r a w C h a n g e d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . R i b b o n C o m m a n d                     + =   t h i s . H a n d l e R i b b o n C o m m a n d ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . B o o k P a n e l S h o w P a g e             + =   t h i s . H a n d l e B o o k P a n e l S h o w P a g e ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . S e t t i n g s S h o w P a g e               + =   t h i s . H a n d l e S e t t i n g s S h o w P a g e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e D o c u m e n t C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e s   i n f o r m a t i o n s   s u r   l e   d o c u m e n t   o n t   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 t h i s . p r i n t S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . e x p o r t S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . q u i c k E x p o r t S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . i n f o s S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . p a g e S t a c k S t a t e . E n a b l e   =   t r u e ;  
  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . D i a l o g s . U p d a t e I n f o s ( ) ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . D i a l o g s . U p d a t e F o n t s ( ) ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . W r a p p e r s . U p d a t e C o m m a n d s ( ) ;  
 	 	 	 	 t h i s . U p d a t e B o o k D o c u m e n t s ( ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . p r i n t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . e x p o r t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . q u i c k E x p o r t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . i n f o s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a g e S t a c k S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e M o u s e C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l a   p o s i t i o n   d e   l a   s o u r i s   a   c h a n g é .  
 	 	 	  
 	 	 	 i f   ( t h i s . i n f o . I t e m s   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 / / 	 Q u a n d   o n   t u e   l ' a p p l i c a t i o n   p a r   A L T - F 4 ,   i l   s e   p e u t   q u e   l ' o n   r e ç o i v e   e n c o r e  
 	 	 	 	 / / 	 d e s   é v é n e m e n t s   s o u r i s   f a n t ô m e s ,   a l o r s   q u e   n o u s   s o m m e s   d é j à   " m o r t s " .  
 	 	 	 	  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 S t a t u s F i e l d   f i e l d   =   t h i s . i n f o . I t e m s [ " S t a t u s M o u s e " ]   a s   S t a t u s F i e l d ;  
 	 	 	 f i e l d . T e x t   =   t h i s . T e x t I n f o M o u s e ;  
 	 	 	 f i e l d . I n v a l i d a t e ( ) ;  
  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 i f   (   d i . h R u l e r   ! =   n u l l   & &   d i . h R u l e r . I s V i s i b l e   )  
 	 	 	 	 {  
 	 	 	 	 	 P o i n t   m o u s e ;  
 	 	 	 	 	 i f   (   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . M o u s e P o s ( o u t   m o u s e )   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d i . h R u l e r . M a r k e r V i s i b l e   =   t r u e ;  
 	 	 	 	 	 	 d i . h R u l e r . M a r k e r   =   m o u s e . X ;  
  
 	 	 	 	 	 	 d i . v R u l e r . M a r k e r V i s i b l e   =   t r u e ;  
 	 	 	 	 	 	 d i . v R u l e r . M a r k e r   =   m o u s e . Y ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d i . h R u l e r . M a r k e r V i s i b l e   =   f a l s e ;  
 	 	 	 	 	 	 d i . v R u l e r . M a r k e r V i s i b l e   =   f a l s e ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e M o d i f C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e   t e x t e   d e s   m o d i f i c a t i o n s   a   c h a n g é .  
 	 	 	 / / 	 T O D O :   [ P A ]   P a r f o i s ,   t h i s . i n f o . I t e m s   e s t   n u l   a p r è s   a v o i r   c l i q u é   l a   c a s e   d e   f e r m e t u r e   d e   l a   f e n ê t r e   !  
 	 	 	 i f   (   t h i s . i n f o . I t e m s   = =   n u l l   )     r e t u r n ;  
  
 	 	 	 S t a t u s F i e l d   f i e l d   =   t h i s . i n f o . I t e m s [ " S t a t u s M o d i f " ]   a s   S t a t u s F i e l d ;  
 	 	 	 f i e l d . T e x t   =   t h i s . T e x t I n f o M o d i f ;  
 	 	 	 f i e l d . I n v a l i d a t e ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e O r i g i n C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' o r i g i n e   a   c h a n g é .  
 	 	 	 t h i s . U p d a t e S c r o l l e r ( ) ;  
  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 t h i s . z o o m P a g e S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . z o o m P a g e W i d t h S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . z o o m D e f a u l t S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . z o o m P a g e S t a t e . A c t i v e S t a t e   =   c o n t e x t . I s Z o o m P a g e   ?   C o m m o n . W i d g e t s . A c t i v e S t a t e . Y e s   :   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . z o o m P a g e W i d t h S t a t e . A c t i v e S t a t e   =   c o n t e x t . I s Z o o m P a g e W i d t h   ?   C o m m o n . W i d g e t s . A c t i v e S t a t e . Y e s   :   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . z o o m D e f a u l t S t a t e . A c t i v e S t a t e   =   c o n t e x t . I s Z o o m D e f a u l t   ?   C o m m o n . W i d g e t s . A c t i v e S t a t e . Y e s   :   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . z o o m P a g e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . z o o m P a g e W i d t h S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . z o o m D e f a u l t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e Z o o m C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e   z o o m   a   c h a n g é .  
 	 	 	 t h i s . U p d a t e S c r o l l e r ( ) ;  
  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 t h i s . z o o m M i n S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . z o o m P a g e S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . z o o m P a g e W i d t h S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . z o o m D e f a u l t S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . z o o m M i n S t a t e . A c t i v e S t a t e   =   ( c o n t e x t . Z o o m   < =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m M i n )   ?   C o m m o n . W i d g e t s . A c t i v e S t a t e . Y e s   :   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . z o o m P a g e S t a t e . A c t i v e S t a t e   =   c o n t e x t . I s Z o o m P a g e   ?   C o m m o n . W i d g e t s . A c t i v e S t a t e . Y e s   :   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . z o o m P a g e W i d t h S t a t e . A c t i v e S t a t e   =   c o n t e x t . I s Z o o m P a g e W i d t h   ?   C o m m o n . W i d g e t s . A c t i v e S t a t e . Y e s   :   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . z o o m D e f a u l t S t a t e . A c t i v e S t a t e   =   c o n t e x t . I s Z o o m D e f a u l t   ?   C o m m o n . W i d g e t s . A c t i v e S t a t e . Y e s   :   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . z o o m P r e v S t a t e . E n a b l e   =   (   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m M e m o r i z e C o u n t   >   0   ) ;  
 	 	 	 	 t h i s . z o o m S u b S t a t e . E n a b l e   =   (   c o n t e x t . Z o o m   >   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m M i n   ) ;  
 	 	 	 	 t h i s . z o o m A d d S t a t e . E n a b l e   =   (   c o n t e x t . Z o o m   <   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . Z o o m M a x   ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . z o o m M i n S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . z o o m P a g e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . z o o m P a g e W i d t h S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . z o o m D e f a u l t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . z o o m M i n S t a t e . A c t i v e S t a t e   =   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . z o o m P a g e S t a t e . A c t i v e S t a t e   =   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . z o o m P a g e W i d t h S t a t e . A c t i v e S t a t e   =   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . z o o m D e f a u l t S t a t e . A c t i v e S t a t e   =   C o m m o n . W i d g e t s . A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . z o o m P r e v S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . z o o m S u b S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . z o o m A d d S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 }  
  
 	 	 	 S t a t u s F i e l d   f i e l d   =   t h i s . i n f o . I t e m s [ " S t a t u s Z o o m " ]   a s   S t a t u s F i e l d ;  
 	 	 	 f i e l d . T e x t   =   t h i s . T e x t I n f o Z o o m ;  
 	 	 	 f i e l d . I n v a l i d a t e ( ) ;  
  
 	 	 	 A b s t r a c t S l i d e r   s l i d e r   =   t h i s . i n f o . I t e m s [ " S t a t u s Z o o m S l i d e r " ]   a s   A b s t r a c t S l i d e r ;  
 	 	 	 s l i d e r . V a l u e   =   ( d e c i m a l )   t h i s . V a l u e I n f o Z o o m ;  
 	 	 	 s l i d e r . E n a b l e   =   t h i s . H a s C u r r e n t D o c u m e n t ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e T o o l ( C o m m a n d S t a t e   c s ,   s t r i n g   c u r r e n t T o o l ,   b o o l   i s C r e a t i n g ,   b o o l   e n a b l e d )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   u n e   c o m m a n d e   d ' o u t i l .  
 	 	 	 C o m m a n d   c o m m a n d   =   c s . C o m m a n d ;  
 	 	 	 s t r i n g   t o o l   =   c o m m a n d . C o m m a n d I d ;  
  
 	 	 	 i f   (   e n a b l e d   )  
 	 	 	 {  
 	 	 	 	 c s . A c t i v e S t a t e   =   ( t o o l   = =   c u r r e n t T o o l )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ; ;  
 	 	 	 	 c s . E n a b l e   =   ( t o o l   = =   c u r r e n t T o o l   | |   t o o l   = =   " T o o l S e l e c t "   | |   t o o l   = =   " T o o l S h a p e r "   | |   ! i s C r e a t i n g ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 c s . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	 c s . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e T o o l C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' o u t i l   a   c h a n g é .  
 	 	 	 s t r i n g   t o o l   =   " " ;  
 	 	 	 b o o l   i s C r e a t i n g   =   f a l s e ;  
 	 	 	 b o o l   e n a b l e d   =   f a l s e ;  
  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 t o o l   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o o l ;  
 	 	 	 	 i s C r e a t i n g   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . I s C r e a t i n g ;  
 	 	 	 	 e n a b l e d   =   t r u e ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l S e l e c t S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l G l o b a l S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l S h a p e r S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l E d i t S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l Z o o m S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l H a n d S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l P i c k e r S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l H o t S p o t S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l L i n e S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l R e c t a n g l e S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l C i r c l e S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l E l l i p s e S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l P o l y S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l F r e e S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l B e z i e r S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l R e g u l a r S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l S u r f a c e S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l V o l u m e S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l T e x t L i n e S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l T e x t L i n e 2 S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l T e x t B o x S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l T e x t B o x 2 S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l A r r a y S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l I m a g e S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 	 t h i s . U p d a t e T o o l ( t h i s . t o o l D i m e n s i o n S t a t e ,   t o o l ,   i s C r e a t i n g ,   e n a b l e d ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S a v e C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' é t a t   " e n r e g i s t r e r "   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 b o o l   i s C r e a t i n g   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . I s C r e a t i n g ;  
 	 	 	 	 t h i s . s a v e S t a t e . E n a b l e   =   ! i s C r e a t i n g   & &   t h i s . C u r r e n t D o c u m e n t . I s D i r t y S e r i a l i z e ;  
 	 	 	 	 t h i s . s a v e A s S t a t e . E n a b l e   =   ! i s C r e a t i n g ;  
 	 	 	 	 t h i s . s a v e M o d e l S t a t e . E n a b l e   =   ! i s C r e a t i n g ;  
 	 	 	 	 t h i s . U p d a t e B o o k D o c u m e n t s ( ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . s a v e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s a v e A s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s a v e M o d e l S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S e l e c t i o n C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l a   s é l e c t i o n   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
  
 	 	 	 	 d i . c o n t a i n e r P r i n c i p a l . S e t D i r t y C o n t e n t ( ) ;  
 	 	 	 	 d i . c o n t a i n e r S t y l e s . S e t D i r t y C o n t e n t ( ) ;  
  
 	 	 	 	 i f   (   d i . c o n t a i n e r A u t o s   ! =   n u l l   )  
 	 	 	 	 {  
 	 	 	 	 	 d i . c o n t a i n e r A u t o s . S e t D i r t y C o n t e n t ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 	 i n t   t o t a l S e l e c t e d     =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o t a l S e l e c t e d ;  
 	 	 	 	 i n t   t o t a l H i d e             =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o t a l H i d e ;  
 	 	 	 	 i n t   t o t a l P a g e H i d e     =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o t a l P a g e H i d e ;  
 	 	 	 	 i n t   t o t a l O b j e c t s       =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o t a l O b j e c t s ;  
 	 	 	 	 b o o l   i s C r e a t i n g         =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . I s C r e a t i n g ;  
 	 	 	 	 b o o l   i s B a s e                 =   v i e w e r . D r a w i n g C o n t e x t . R o o t S t a c k I s B a s e ;  
 	 	 	 	 b o o l   i s E d i t                 =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . I s T o o l E d i t ;  
 	 	 	 	 S e l e c t o r T y p e   s T y p e   =   v i e w e r . S e l e c t o r T y p e ;  
 	 	 	 	 O b j e c t s . A b s t r a c t   o n e   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R e t O n l y S e l e c t e d O b j e c t ( ) ;  
  
 	 	 	 	 b o o l   i s O l d T e x t   =   f a l s e ;  
 	 	 	 	 i f   (   t h i s . C u r r e n t D o c u m e n t . C o n t a i n O l d T e x t   )  
 	 	 	 	 {  
 	 	 	 	 	 i s O l d T e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . I s S e l e c t e d O l d T e x t ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . n e w S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . o p e n S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . o p e n M o d e l S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . d e l e t e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   | |   i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . d u p l i c a t e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . o r d e r U p O n e S t a t e . E n a b l e   =   (   t o t a l O b j e c t s   >   1   & &   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . o r d e r D o w n O n e S t a t e . E n a b l e   =   (   t o t a l O b j e c t s   >   1   & &   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . o r d e r U p A l l S t a t e . E n a b l e   =   (   t o t a l O b j e c t s   >   1   & &   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . o r d e r D o w n A l l S t a t e . E n a b l e   =   (   t o t a l O b j e c t s   >   1   & &   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . m o v e L e f t F r e e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . m o v e R i g h t F r e e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . m o v e U p F r e e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . m o v e D o w n F r e e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . r o t a t e 9 0 S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . r o t a t e 1 8 0 S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . r o t a t e 2 7 0 S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . r o t a t e F r e e C C W S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . r o t a t e F r e e C W S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . m i r r o r H S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . m i r r o r V S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s c a l e M u l 2 S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s c a l e D i v 2 S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s c a l e M u l F r e e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s c a l e D i v F r e e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . a l i g n L e f t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . a l i g n C e n t e r X S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . a l i g n R i g h t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . a l i g n T o p S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . a l i g n C e n t e r Y S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . a l i g n B o t t o m S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . a l i g n G r i d S t a t e . E n a b l e   =   ( t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t ) ;  
 	 	 	 	 t h i s . r e s e t S t a t e . E n a b l e   =   ( t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t ) ;  
 	 	 	 	 t h i s . s h a r e L e f t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   2   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s h a r e C e n t e r X S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   2   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s h a r e S p a c e X S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   2   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s h a r e R i g h t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   2   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s h a r e T o p S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   2   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s h a r e C e n t e r Y S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   2   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s h a r e S p a c e Y S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   2   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . s h a r e B o t t o m S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   2   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . a d j u s t W i d t h S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . a d j u s t H e i g h t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . c o l o r T o R g b S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . c o l o r T o C m y k S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . c o l o r T o G r a y S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . c o l o r S t r o k e D a r k S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . c o l o r S t r o k e L i g h t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . c o l o r F i l l D a r k S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . c o l o r F i l l L i g h t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . m e r g e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . e x t r a c t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s B a s e   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . g r o u p S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . u n g r o u p S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   = =   1   & &   o n e   i s   O b j e c t s . G r o u p   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . i n s i d e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   = =   1   & &   o n e   i s   O b j e c t s . G r o u p   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . o u t s i d e S t a t e . E n a b l e   =   (   ! i s B a s e   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . c o m b i n e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . u n c o m b i n e S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . t o B e z i e r S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . t o P o l y S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . t o T e x t B o x 2 S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   & &   i s O l d T e x t   ) ;  
 	 	 	 	 t h i s . f r a g m e n t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . b o o l e a n A n d S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . b o o l e a n O r S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . b o o l e a n X o r S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . b o o l e a n F r o n t M i n u s S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . b o o l e a n B a c k M i n u s S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   1   & &   ! i s C r e a t i n g   & &   ! i s E d i t   ) ;  
 	 	 	 	 t h i s . l a y e r N e w S e l S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   ) ;  
  
 	 	 	 	 t h i s . h i d e S e l S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . h i d e R e s t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   t o t a l O b j e c t s - t o t a l S e l e c t e d - t o t a l H i d e   >   0   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . h i d e C a n c e l S t a t e . E n a b l e   =   (   t o t a l P a g e H i d e   >   0   & &   ! i s C r e a t i n g   ) ;  
  
 	 	 	 	 t h i s . z o o m S e l S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   ) ;  
 	 	 	 	 t h i s . z o o m S e l W i d t h S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   ) ;  
  
 	 	 	 	 t h i s . d e s e l e c t A l l S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   ) ;  
 	 	 	 	 t h i s . s e l e c t A l l S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   <   t o t a l O b j e c t s - t o t a l H i d e   ) ;  
 	 	 	 	 t h i s . s e l e c t I n v e r t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   t o t a l S e l e c t e d   <   t o t a l O b j e c t s - t o t a l H i d e   ) ;  
  
 	 	 	 	 t h i s . s e l e c t o r A u t o S t a t e . E n a b l e                 =   t r u e ;  
 	 	 	 	 t h i s . s e l e c t o r I n d i v i d u a l S t a t e . E n a b l e     =   t r u e ;  
 	 	 	 	 t h i s . s e l e c t o r S c a l e r S t a t e . E n a b l e             =   t r u e ;  
 	 	 	 	 t h i s . s e l e c t o r S t r e t c h S t a t e . E n a b l e           =   t r u e ;  
 	 	 	 	 t h i s . s e l e c t o r S t r e t c h T y p e S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . s e l e c t o r A u t o S t a t e . A c t i v e S t a t e               =   ( s T y p e   = =   S e l e c t o r T y p e . A u t o             )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . s e l e c t o r I n d i v i d u a l S t a t e . A c t i v e S t a t e   =   ( s T y p e   = =   S e l e c t o r T y p e . I n d i v i d u a l )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . s e l e c t o r S c a l e r S t a t e . A c t i v e S t a t e           =   ( s T y p e   = =   S e l e c t o r T y p e . S c a l e r         )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . s e l e c t o r S t r e t c h S t a t e . A c t i v e S t a t e         =   ( s T y p e   = =   S e l e c t o r T y p e . S t r e t c h e r   )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 d i . c o n t a i n e r P r i n c i p a l . U p d a t e S e l e c t o r S t r e t c h ( ) ;  
  
 	 	 	 	 t h i s . s e l e c t T o t a l S t a t e . E n a b l e       =   t r u e ;  
 	 	 	 	 t h i s . s e l e c t P a r t i a l S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . s e l e c t T o t a l S t a t e . A c t i v e S t a t e       =   ! v i e w e r . P a r t i a l S e l e c t   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . s e l e c t P a r t i a l S t a t e . A c t i v e S t a t e   =     v i e w e r . P a r t i a l S e l e c t   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
  
 	 	 	 	 t h i s . s e l e c t o r A d a p t L i n e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . s e l e c t o r A d a p t T e x t . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . s e l e c t o r A d a p t L i n e . A c t i v e S t a t e   =   v i e w e r . S e l e c t o r A d a p t L i n e   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . s e l e c t o r A d a p t T e x t . A c t i v e S t a t e   =   v i e w e r . S e l e c t o r A d a p t T e x t   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
  
 	 	 	 	 i f   (   ! t h i s . C u r r e n t D o c u m e n t . W r a p p e r s . I s W r a p p e r s A t t a c h e d   )     / /   p a s   é d i t i o n   e n   c o u r s   ?  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . c u t S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 	 t h i s . c o p y S t a t e . E n a b l e   =   (   t o t a l S e l e c t e d   >   0   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 	 / / ? t h i s . p a s t e S t a t e . E n a b l e   =   ( ! t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . I s C l i p b o a r d E m p t y ( )   & &   ! i s C r e a t i n g ) ;  
 	 	 	 	 	 t h i s . p a s t e S t a t e . E n a b l e   =   ! i s C r e a t i n g ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . D i a l o g s . U p d a t e I n f o s ( ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . n e w S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . o p e n S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . o p e n M o d e l S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . d e l e t e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . d u p l i c a t e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c u t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c o p y S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a s t e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . g l y p h s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . g l y p h s I n s e r t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t E d i t i n g S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t S h o w C o n t r o l C h a r a c t e r s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t F o n t F i l t e r S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t F o n t S a m p l e A b c S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t I n s e r t Q u a d S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t I n s e r t N e w F r a m e E d g e s . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t I n s e r t N e w P a g e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f o n t B o l d S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f o n t I t a l i c S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f o n t U n d e r l i n e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f o n t O v e r l i n e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f o n t S t r i k e o u t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f o n t S u b s c r i p t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f o n t S u p e r s c r i p t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f o n t S i z e P l u s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f o n t S i z e M i n u s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f o n t C l e a r S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a r a g r a p h L e a d i n g S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a r a g r a p h L e a d i n g P l u s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a r a g r a p h L e a d i n g M i n u s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a r a g r a p h I n d e n t P l u s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a r a g r a p h I n d e n t M i n u s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a r a g r a p h J u s t i f S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a r a g r a p h C l e a r S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . o r d e r U p O n e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . o r d e r D o w n O n e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . o r d e r U p A l l S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . o r d e r D o w n A l l S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . m o v e L e f t F r e e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . m o v e R i g h t F r e e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . m o v e U p F r e e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . m o v e D o w n F r e e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . r o t a t e 9 0 S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . r o t a t e 1 8 0 S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . r o t a t e 2 7 0 S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . r o t a t e F r e e C C W S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . r o t a t e F r e e C W S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . m i r r o r H S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . m i r r o r V S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s c a l e M u l 2 S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s c a l e D i v 2 S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s c a l e M u l F r e e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s c a l e D i v F r e e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . a l i g n L e f t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . a l i g n C e n t e r X S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . a l i g n R i g h t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . a l i g n T o p S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . a l i g n C e n t e r Y S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . a l i g n B o t t o m S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . a l i g n G r i d S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . r e s e t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a r e L e f t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a r e C e n t e r X S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a r e S p a c e X S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a r e R i g h t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a r e T o p S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a r e C e n t e r Y S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a r e S p a c e Y S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a r e B o t t o m S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . a d j u s t W i d t h S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . a d j u s t H e i g h t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c o l o r T o R g b S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c o l o r T o C m y k S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c o l o r T o G r a y S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c o l o r S t r o k e D a r k S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c o l o r S t r o k e L i g h t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c o l o r F i l l D a r k S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c o l o r F i l l L i g h t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . m e r g e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . e x t r a c t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . g r o u p S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . u n g r o u p S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . i n s i d e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . o u t s i d e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c o m b i n e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . u n c o m b i n e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t o B e z i e r S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t o P o l y S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t o T e x t B o x 2 S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . f r a g m e n t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . b o o l e a n A n d S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . b o o l e a n O r S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . b o o l e a n X o r S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . b o o l e a n F r o n t M i n u s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . b o o l e a n B a c k M i n u s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a y e r N e w S e l S t a t e . E n a b l e   =   f a l s e ;  
  
 	 	 	 	 t h i s . h i d e S e l S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . h i d e R e s t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . h i d e C a n c e l S t a t e . E n a b l e   =   f a l s e ;  
  
 	 	 	 	 t h i s . z o o m S e l S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . z o o m S e l W i d t h S t a t e . E n a b l e   =   f a l s e ;  
  
 	 	 	 	 t h i s . d e s e l e c t A l l S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t A l l S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t I n v e r t S t a t e . E n a b l e   =   f a l s e ;  
  
 	 	 	 	 t h i s . s e l e c t o r A u t o S t a t e . E n a b l e                 =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t o r I n d i v i d u a l S t a t e . E n a b l e     =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t o r S c a l e r S t a t e . E n a b l e             =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t o r S t r e t c h S t a t e . E n a b l e           =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t o r S t r e t c h T y p e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t o r A u t o S t a t e . A c t i v e S t a t e               =   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . s e l e c t o r I n d i v i d u a l S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . s e l e c t o r S c a l e r S t a t e . A c t i v e S t a t e           =   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . s e l e c t o r S t r e t c h S t a t e . A c t i v e S t a t e         =   A c t i v e S t a t e . N o ;  
  
 	 	 	 	 t h i s . s e l e c t T o t a l S t a t e . E n a b l e       =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t P a r t i a l S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t T o t a l S t a t e . A c t i v e S t a t e       =   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . s e l e c t P a r t i a l S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
  
 	 	 	 	 t h i s . s e l e c t o r A d a p t L i n e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t o r A d a p t T e x t . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s e l e c t o r A d a p t L i n e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . s e l e c t o r A d a p t T e x t . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 t h i s . d l g G l y p h s . S e t A l t e r n a t e s D i r t y ( ) ;  
  
 	 	 	 S t a t u s F i e l d   f i e l d   =   t h i s . i n f o . I t e m s [ " S t a t u s O b j e c t " ]   a s   S t a t u s F i e l d ;  
 	 	 	 f i e l d . T e x t   =   t h i s . T e x t I n f o O b j e c t ;  
 	 	 	 f i e l d . I n v a l i d a t e ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e G e o m e t r y C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l a   g é o m é t r i e   d ' u n   o b j e t   a   c h a n g é .  
 	 	 	 i f   ( t h i s . H a s C u r r e n t D o c u m e n t )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
  
 	 	 	 	 d i . c o n t a i n e r P r i n c i p a l . S e t D i r t y C o n t e n t ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S h a p e r C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e   m o d e l e u r   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   & &  
 	 	 	 	   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . I s T o o l S h a p e r   & &  
 	 	 	 	   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . T o t a l S e l e c t e d   ! =   0   )  
 	 	 	 {  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S h a p e r H a n d l e U p d a t e ( t h i s . C o m m a n d D i s p a t c h e r ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . s h a p e r H a n d l e A d d S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e S u b S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e T o L i n e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e T o C u r v e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e S y m S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e S m o o t h S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e D i s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e I n l i n e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e F r e e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e S i m p l y S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e C o r n e r S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e C o n t i n u e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e S h a r p S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . s h a p e r H a n d l e R o u n d S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e T e x t C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e   t e x t e   e n   é d i t i o n   a   c h a n g é .  
 	 	 	 t h i s . R i b b o n s N o t i f y T e x t S t y l e s C h a n g e d ( ) ;  
  
 	 	 	 t h i s . d l g G l y p h s . S e t A l t e r n a t e s D i r t y ( ) ;  
  
 	 	 	 i f   ( t h i s . H a s C u r r e n t D o c u m e n t )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 d i . d o c u m e n t . S e t D i r t y S e r i a l i z e ( C a c h e B i t m a p C h a n g i n g . L o c a l ) ;     / /   ( * R E M 1 * )  
  
 	 	 	 	 / /   ( * R E M 1 * ) 	 I l   f a u t   a p p e l e r   d o c u m e n t . S e t D i r t y S e r i a l i z e   p o u r   m e t t r e   à   j o u r   l e s   m i n i a t u r e s  
 	 	 	 	 / / 	 	 	 ( d e   f a ç o n   a s y n c h r o n e )   p e n d a n t   l a   f r a p p e   d u   t e x t e .  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S t y l e C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u ' u n   s t y l e   a   c h a n g é .  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 d i . c o n t a i n e r S t y l e s . S e t D i r t y C o n t e n t ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e P a g e s C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e s   p a g e s   o n t   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 d i . c o n t a i n e r P a g e s . S e t D i r t y C o n t e n t ( ) ;  
  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 i n t   c p   =   c o n t e x t . C u r r e n t P a g e ;  
 	 	 	 	 i n t   t p   =   c o n t e x t . T o t a l P a g e s ( ) ;  
  
 	 	 	 	 b o o l   i s C r e a t i n g   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . I s C r e a t i n g ;  
  
 	 	 	 	 t h i s . p a g e P r e v S t a t e . E n a b l e   =   ( c p   >   0   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . p a g e N e x t S t a t e . E n a b l e   =   ( c p   <   t p - 1   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . p a g e M e n u S t a t e . E n a b l e   =   ( t p   >   1   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . p a g e N e w S t a t e . E n a b l e   =   ! i s C r e a t i n g ;  
 	 	 	 	 t h i s . p a g e D u p l i c a t e S t a t e . E n a b l e   =   ! i s C r e a t i n g ;  
 	 	 	 	 t h i s . p a g e U p S t a t e . E n a b l e   =   ( c p   >   0   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . p a g e D o w n S t a t e . E n a b l e   =   ( c p   <   t p - 1   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . p a g e D e l e t e S t a t e . E n a b l e   =   ( t p   >   1   & &   ! i s C r e a t i n g   ) ;  
  
 	 	 	 	 O b j e c t s . P a g e   p a g e   =   t h i s . C u r r e n t D o c u m e n t . D o c u m e n t O b j e c t s [ c p ]   a s   O b j e c t s . P a g e ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t I n f o . q u i c k P a g e M e n u . T e x t   =   p a g e . S h o r t N a m e ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . I m a g e L o c k I n P a g e ( c p ) ;  
  
 	 	 	 	 i f   ( d i . p a g e M i n i a t u r e s   ! =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 d i . p a g e M i n i a t u r e s . U p d a t e P a g e A f t e r C h a n g i n g ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( d i . l a y e r M i n i a t u r e s   ! =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 d i . l a y e r M i n i a t u r e s . U p d a t e L a y e r A f t e r C h a n g i n g ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . d l g P a g e S t a c k . U p d a t e ( ) ;  
 	 	 	 	 t h i s . d l g P r i n t . U p d a t e P a g e s ( ) ;  
 	 	 	 	 t h i s . d l g E x p o r t P D F . U p d a t e P a g e s ( ) ;  
 	 	 	 	 t h i s . d l g E x p o r t I C O . U p d a t e P a g e s ( ) ;  
 	 	 	 	 t h i s . H a n d l e M o d i f C h a n g e d ( ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . p a g e P r e v S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a g e N e x t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a g e M e n u S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a g e N e w S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a g e D u p l i c a t e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a g e U p S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a g e D o w n S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p a g e D e l e t e S t a t e . E n a b l e   =   f a l s e ;  
  
 	 	 	 	 t h i s . d l g P a g e S t a c k . U p d a t e ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e L a y e r s C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e s   c a l q u e s   o n t   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 d i . c o n t a i n e r L a y e r s . S e t D i r t y C o n t e n t ( ) ;  
  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 i n t   c l   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 	 i n t   t l   =   c o n t e x t . T o t a l L a y e r s ( ) ;  
  
 	 	 	 	 b o o l   i s C r e a t i n g   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . I s C r e a t i n g ;  
  
 	 	 	 	 t h i s . l a y e r P r e v S t a t e . E n a b l e   =   ( c l   >   0   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . l a y e r N e x t S t a t e . E n a b l e   =   ( c l   <   t l - 1   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . l a y e r M e n u S t a t e . E n a b l e   =   ( t l   >   1   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . l a y e r N e w S t a t e . E n a b l e   =   ! i s C r e a t i n g   ;  
 	 	 	 	 t h i s . l a y e r D u p l i c a t e S t a t e . E n a b l e   =   ! i s C r e a t i n g   ;  
 	 	 	 	 t h i s . l a y e r M e r g e U p S t a t e . E n a b l e   =   ( c l   <   t l - 1   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . l a y e r M e r g e D o w n S t a t e . E n a b l e   =   ( c l   >   0   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . l a y e r U p S t a t e . E n a b l e   =   ( c l   <   t l - 1   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . l a y e r D o w n S t a t e . E n a b l e   =   ( c l   >   0   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . l a y e r D e l e t e S t a t e . E n a b l e   =   ( t l   >   1   & &   ! i s C r e a t i n g   ) ;  
  
 	 	 	 	 b o o l   m l   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . M a g n e t L a y e r S t a t e ( c l ) ;  
 	 	 	 	 t h i s . m a g n e t L a y e r S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . m a g n e t L a y e r S t a t e . A c t i v e S t a t e   =   m l   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
  
 	 	 	 	 i f   ( d i . l a y e r M i n i a t u r e s   ! =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 d i . l a y e r M i n i a t u r e s . U p d a t e L a y e r A f t e r C h a n g i n g ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t I n f o . q u i c k L a y e r M e n u . T e x t   =   O b j e c t s . L a y e r . S h o r t N a m e ( c l ) ;  
 	 	 	 	 t h i s . d l g P a g e S t a c k . U p d a t e ( ) ;  
 	 	 	 	 t h i s . H a n d l e M o d i f C h a n g e d ( ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . l a y e r P r e v S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a y e r N e x t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a y e r M e n u S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a y e r N e w S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a y e r D u p l i c a t e S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a y e r M e r g e U p S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a y e r M e r g e D o w n S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a y e r U p S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a y e r D o w n S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a y e r D e l e t e S t a t e . E n a b l e   =   f a l s e ;  
  
 	 	 	 	 t h i s . m a g n e t L a y e r S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . m a g n e t L a y e r S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e P a g e C h a n g e d ( O b j e c t s . A b s t r a c t   p a g e )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u ' u n   n o m   d e   p a g e   a   c h a n g é .  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 d i . c o n t a i n e r P a g e s . S e t D i r t y O b j e c t ( p a g e ) ;  
 	 	 	 t h i s . H a n d l e M o d i f C h a n g e d ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e L a y e r C h a n g e d ( O b j e c t s . A b s t r a c t   l a y e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u ' u n   n o m   d e   c a l q u e   a   c h a n g é .  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 d i . c o n t a i n e r L a y e r s . S e t D i r t y O b j e c t ( l a y e r ) ;  
 	 	 	 t h i s . H a n d l e M o d i f C h a n g e d ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e U n d o R e d o C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' é t a t   d e s   c o m m a n d e   u n d o / r e d o   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 b o o l   i s C r e a t i n g   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . I s C r e a t i n g ;  
 	 	 	 	 t h i s . u n d o S t a t e . E n a b l e   =   (   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . O p l e t Q u e u e . C a n U n d o   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . r e d o S t a t e . E n a b l e   =   (   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . O p l e t Q u e u e . C a n R e d o   & &   ! i s C r e a t i n g   ) ;  
 	 	 	 	 t h i s . u n d o R e d o L i s t S t a t e . E n a b l e   =   t h i s . u n d o S t a t e . E n a b l e | t h i s . r e d o S t a t e . E n a b l e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . u n d o S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . r e d o S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . u n d o R e d o L i s t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e G r i d C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' é t a t   d e   l a   g r i l l e   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	  
 	 	 	 	 t h i s . g r i d S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . g r i d S t a t e . A c t i v e S t a t e   =   c o n t e x t . G r i d A c t i v e   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	  
 	 	 	 	 t h i s . t e x t G r i d S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . t e x t G r i d S t a t e . A c t i v e S t a t e   =   c o n t e x t . T e x t G r i d S h o w   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	  
 	 	 	 	 t h i s . r u l e r s S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . r u l e r s S t a t e . A c t i v e S t a t e   =   c o n t e x t . R u l e r s S h o w   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	  
 	 	 	 	 t h i s . l a b e l s S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . l a b e l s S t a t e . A c t i v e S t a t e   =   c o n t e x t . L a b e l s S h o w   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	  
 	 	 	 	 t h i s . a g g r e g a t e s S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . a g g r e g a t e s S t a t e . A c t i v e S t a t e   =   c o n t e x t . A g g r e g a t e s S h o w   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
  
 	 	 	 	 t h i s . t e x t S h o w C o n t r o l C h a r a c t e r s S t a t e . E n a b l e   =   t h i s . C u r r e n t D o c u m e n t . W r a p p e r s . I s W r a p p e r s A t t a c h e d ;  
 	 	 	 	 t h i s . t e x t S h o w C o n t r o l C h a r a c t e r s S t a t e . A c t i v e S t a t e   =   c o n t e x t . T e x t S h o w C o n t r o l C h a r a c t e r s   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . g r i d S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . g r i d S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	  
 	 	 	 	 t h i s . t e x t G r i d S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t G r i d S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	  
 	 	 	 	 t h i s . r u l e r s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . r u l e r s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	  
 	 	 	 	 t h i s . l a b e l s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . l a b e l s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	  
 	 	 	 	 t h i s . a g g r e g a t e s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . a g g r e g a t e s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
  
 	 	 	 	 t h i s . t e x t S h o w C o n t r o l C h a r a c t e r s S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t S h o w C o n t r o l C h a r a c t e r s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e L a b e l P r o p e r t i e s C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' é t a t   d e s   n o m s   d ' a t t r i b u t s   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 d i . c o n t a i n e r P r i n c i p a l . S e t D i r t y C o n t e n t ( ) ;  
 	 	 	 	 d i . c o n t a i n e r S t y l e s . S e t D i r t y C o n t e n t ( ) ;  
 	 	 	 	 d i . c o n t a i n e r P a g e s . S e t D i r t y C o n t e n t ( ) ;  
 	 	 	 	 d i . c o n t a i n e r L a y e r s . S e t D i r t y C o n t e n t ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C o n s t r a i n C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' é t a t   d e s   c o n s t r u c t i o n s   a   c h a n g é .  
 	 	 	 i f   ( t h i s . H a s C u r r e n t D o c u m e n t )  
 	 	 	 {  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 t h i s . c o n s t r a i n S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . c o n s t r a i n S t a t e . A c t i v e S t a t e   =   c o n t e x t . C o n s t r a i n A c t i v e   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . c o n s t r a i n S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . c o n s t r a i n S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e M a g n e t C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' é t a t   d e s   l i g n e s   m a g n é t i q u e s   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 I L i s t < O b j e c t s . L a y e r >   l a y e r s   =   c o n t e x t . M a g n e t L a y e r L i s t ;  
 	 	 	 	 i f   (   l a y e r s . C o u n t   = =   0   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . m a g n e t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 	 t h i s . m a g n e t S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . m a g n e t S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 	 t h i s . m a g n e t S t a t e . A c t i v e S t a t e   =   c o n t e x t . M a g n e t A c t i v e   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . m a g n e t S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . m a g n e t S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e P r e v i e w C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' é t a t   d e   l ' a p e r ç u   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 t h i s . p r e v i e w S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . p r e v i e w S t a t e . A c t i v e S t a t e   =   c o n t e x t . P r e v i e w A c t i v e   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . p r e v i e w S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . p r e v i e w S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S e t t i n g s C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e s   r é g l a g e s   o n t   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . D i a l o g s . U p d a t e A l l S e t t i n g s ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e F o n t s S e t t i n g s C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e s   r é g l a g e s   d e   p o l i c e   o n t   c h a n g é s .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	  
 	 	 	 	 t h i s . t e x t F o n t F i l t e r S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . t e x t F o n t F i l t e r S t a t e . A c t i v e S t a t e   =   c o n t e x t . T e x t F o n t F i l t e r   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	  
 	 	 	 	 t h i s . t e x t F o n t S a m p l e A b c S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . t e x t F o n t S a m p l e A b c S t a t e . A c t i v e S t a t e   =   c o n t e x t . T e x t F o n t S a m p l e A b c   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . D i a l o g s . U p d a t e F o n t s ( ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . t e x t F o n t F i l t e r S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t F o n t F i l t e r S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	  
 	 	 	 	 t h i s . t e x t F o n t S a m p l e A b c S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . t e x t F o n t S a m p l e A b c S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 	 	  
 	 	 	 t h i s . R i b b o n s N o t i f y C h a n g e d ( " F o n t s S e t t i n g s C h a n g e d " ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e G u i d e s C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e s   r e p è r e s   o n t   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . D i a l o g s . U p d a t e G u i d e s ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e H i d e H a l f C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' é t a t   d e   l a   c o m m a n d e   " h i d e   h a l f "   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 t h i s . h i d e H a l f S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . h i d e H a l f S t a t e . A c t i v e S t a t e   =   c o n t e x t . H i d e H a l f A c t i v e   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . h i d e H a l f S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . h i d e H a l f S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e D e b u g C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l ' é t a t   d e s   c o m m a n d e   d e   d e b u g   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 t h i s . d e b u g B b o x T h i n S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . d e b u g B b o x G e o m S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . d e b u g B b o x F u l l S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . d e b u g B b o x T h i n S t a t e . A c t i v e S t a t e   =   c o n t e x t . I s D r a w B o x T h i n   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . d e b u g B b o x G e o m S t a t e . A c t i v e S t a t e   =   c o n t e x t . I s D r a w B o x G e o m   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . d e b u g B b o x F u l l S t a t e . A c t i v e S t a t e   =   c o n t e x t . I s D r a w B o x F u l l   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . d e b u g D i r t y S t a t e . E n a b l e   =   t r u e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d e b u g B b o x T h i n S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . d e b u g B b o x G e o m S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . d e b u g B b o x F u l l S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 	 t h i s . d e b u g B b o x T h i n S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . d e b u g B b o x G e o m S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . d e b u g B b o x F u l l S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . d e b u g D i r t y S t a t e . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e P r o p e r t y C h a n g e d ( S y s t e m . C o l l e c t i o n s . A r r a y L i s t   p r o p e r t y L i s t )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n e   p r o p r i é t é   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 d i . c o n t a i n e r P r i n c i p a l . S e t D i r t y P r o p e r t i e s ( p r o p e r t y L i s t ) ;  
 	 	 	 	 d i . c o n t a i n e r S t y l e s . S e t D i r t y P r o p e r t i e s ( p r o p e r t y L i s t ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e A g g r e g a t e C h a n g e d ( S y s t e m . C o l l e c t i o n s . A r r a y L i s t   a g g r e g a t e L i s t )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n   a g r é g a t   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 d i . c o n t a i n e r P r i n c i p a l . S e t D i r t y A g g r e g a t e s ( a g g r e g a t e L i s t ) ;  
 	 	 	 	 d i . c o n t a i n e r S t y l e s . S e t D i r t y A g g r e g a t e s ( a g g r e g a t e L i s t ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e T e x t S t y l e C h a n g e d ( S y s t e m . C o l l e c t i o n s . A r r a y L i s t   t e x t S t y l e L i s t )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n   a g r é g a t   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 d i . c o n t a i n e r S t y l e s . S e t D i r t y T e x t S t y l e s ( t e x t S t y l e L i s t ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . R i b b o n s N o t i f y T e x t S t y l e s C h a n g e d ( t e x t S t y l e L i s t ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e T e x t S t y l e L i s t C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n   s t y l e   d e   t e x t e   a   é t é   a j o u t é   o u   s u p p r i m é .  
 	 	 	 i f   ( t h i s . H a s C u r r e n t D o c u m e n t )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 d i . c o n t a i n e r S t y l e s . S e t D i r t y C o n t e n t ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . R i b b o n s N o t i f y C h a n g e d ( " T e x t S t y l e L i s t C h a n g e d " ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S e l N a m e s C h a n g e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l a   s é l e c t i o n   p a r   n o m s   a   c h a n g é .  
 	 	 	 i f   (   t h i s . H a s C u r r e n t D o c u m e n t   )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 d i . c o n t a i n e r P r i n c i p a l . S e t D i r t y S e l N a m e s ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e D r a w C h a n g e d ( V i e w e r   v i e w e r ,   D r a w i n g . R e c t a n g l e   r e c t )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u e   l e   d e s s i n   a   c h a n g é .  
 	 	 	 D r a w i n g . R e c t a n g l e   b o x   =   r e c t ;  
  
 	 	 	 i f   (   v i e w e r . D r a w i n g C o n t e x t . I s A c t i v e   )  
 	 	 	 {  
 	 	 	 	 b o x . I n f l a t e ( v i e w e r . D r a w i n g C o n t e x t . H a n d l e R e d r a w S i z e / 2 ) ;  
 	 	 	 }  
  
 	 	 	 b o x   =   v i e w e r . I n t e r n a l T o S c r e e n ( b o x ) ;  
 	 	 	 t h i s . I n v a l i d a t e D r a w ( v i e w e r ,   b o x ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e R i b b o n C o m m a n d ( s t r i n g   n a m e )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u ' i l   f a u t   c h a n g e r   d e   r u b a n .  
 	 	 	 R i b b o n P a g e   r i b b o n   =   t h i s . G e t R i b b o n ( n a m e ) ;  
  
 	 	 	 i f   (   n a m e . L e n g t h   >   0   & &   n a m e [ 0 ]   = =   ' ! '   )  
 	 	 	 {  
 	 	 	 	 r i b b o n   =   t h i s . L a s t R i b b o n ( n a m e . S u b s t r i n g ( 1 ) ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . S e t A c t i v e R i b b o n ( r i b b o n ) ;  
 	 	 }  
 	 	  
 	 	 p r i v a t e   v o i d   H a n d l e B o o k P a n e l S h o w P a g e ( s t r i n g   p a g e ,   s t r i n g   s u b )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u ' i l   f a u t   a f f i c h e r   u n   o n g l e t   s p é c i f i q u e .  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 i f   (   d i   = =   n u l l   )     r e t u r n ;  
  
 	 	 	 f o r e a c h   (   T a b P a g e   t a b   i n   d i . b o o k P a n e l s . I t e m s   )  
 	 	 	 {  
 	 	 	 	 i f   (   t a b   = =   n u l l   )     c o n t i n u e ;  
 	 	 	 	 i f   (   t a b . N a m e   = =   p a g e   )  
 	 	 	 	 {  
 	 	 	 	 	 d i . b o o k P a n e l s . A c t i v e P a g e   =   t a b ;  
  
 	 	 	 	 	 i f   (   p a g e   = =   " S t y l e s "   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d i . c o n t a i n e r S t y l e s . S e t C a t e g o r y ( s u b ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
 	 	  
 	 	 p r i v a t e   v o i d   H a n d l e S e t t i n g s S h o w P a g e ( s t r i n g   b o o k ,   s t r i n g   t a b )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   p a r   l e   d o c u m e n t   l o r s q u ' i l   f a u t   a f f i c h e r   u n e   p a g e   s p é c i f i q u e   d u   d i a l o q u e   d e s   r é g l a g e s .  
 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
  
 	 	 	 i f   (   t h i s . s e t t i n g s S t a t e . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o   )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g S e t t i n g s . S h o w ( ) ;  
 	 	 	 	 t h i s . s e t t i n g s S t a t e . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
  
 	 	 	 t h i s . d l g S e t t i n g s . S h o w P a g e ( b o o k ,   t a b ) ;  
 	 	 }  
 	 	  
  
 	 	 p r o t e c t e d   v o i d   I n v a l i d a t e D r a w ( V i e w e r   v i e w e r ,   D r a w i n g . R e c t a n g l e   b b o x )  
 	 	 {  
 	 	 	 / / 	 I n v a l i d e   u n e   p a r t i e   d e   l a   z o n e   d e   d e s s i n   d ' u n   v i s u a l i s a t e u r .  
 	 	 	 i f   (   b b o x . I s E m p t y   )     r e t u r n ;  
  
 	 	 	 i f   (   b b o x . I s I n f i n i t e   )  
 	 	 	 {  
 	 	 	 	 v i e w e r . S y n c P a i n t   =   t r u e ;  
 	 	 	 	 v i e w e r . I n v a l i d a t e ( ) ;  
 	 	 	 	 v i e w e r . S y n c P a i n t   =   f a l s e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 v i e w e r . S y n c P a i n t   =   t r u e ;  
 	 	 	 	 v i e w e r . I n v a l i d a t e ( b b o x ) ;  
 	 	 	 	 v i e w e r . S y n c P a i n t   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e S c r o l l e r ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e s   a s c e n s e u r s .  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 S i z e   a r e a   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S i z e A r e a ;  
 	 	 	 P o i n t   s c a l e   =   c o n t e x t . S c a l e ;  
 	 	 	 S i z e   c s   =   c o n t e x t . C o n t a i n e r S i z e ;  
 	 	 	 i f   (   c s . W i d t h   <   0 . 0   | |   c s . H e i g h t   <   0 . 0   )     r e t u r n ;  
 	 	 	 d o u b l e   r a t i o H   =   c s . W i d t h / s c a l e . X / a r e a . W i d t h ;  
 	 	 	 d o u b l e   r a t i o V   =   c s . H e i g h t / s c a l e . Y / a r e a . H e i g h t ;  
 	 	 	 r a t i o H   =   S y s t e m . M a t h . M i n ( r a t i o H ,   1 . 0 ) ;  
 	 	 	 r a t i o V   =   S y s t e m . M a t h . M i n ( r a t i o V ,   1 . 0 ) ;  
  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 d o u b l e   m i n ,   m a x ;  
  
 	 	 	 m i n   =   c o n t e x t . M i n O r i g i n X ;  
 	 	 	 m a x   =   c o n t e x t . M a x O r i g i n X ;  
 	 	 	 m a x   =   S y s t e m . M a t h . M a x ( m i n ,   m a x ) ;  
 	 	 	 d i . h S c r o l l e r . M i n V a l u e   =   ( d e c i m a l )   m i n ;  
 	 	 	 d i . h S c r o l l e r . M a x V a l u e   =   ( d e c i m a l )   m a x ;  
 	 	 	 d i . h S c r o l l e r . V i s i b l e R a n g e R a t i o   =   ( d e c i m a l )   r a t i o H ;  
 	 	 	 d i . h S c r o l l e r . V a l u e   =   ( d e c i m a l )   ( - c o n t e x t . O r i g i n X ) ;  
 	 	 	 d i . h S c r o l l e r . S m a l l C h a n g e   =   ( d e c i m a l )   ( ( c s . W i d t h * 0 . 1 ) / s c a l e . X ) ;  
 	 	 	 d i . h S c r o l l e r . L a r g e C h a n g e   =   ( d e c i m a l )   ( ( c s . W i d t h * 0 . 9 ) / s c a l e . X ) ;  
  
 	 	 	 m i n   =   c o n t e x t . M i n O r i g i n Y ;  
 	 	 	 m a x   =   c o n t e x t . M a x O r i g i n Y ;  
 	 	 	 m a x   =   S y s t e m . M a t h . M a x ( m i n ,   m a x ) ;  
 	 	 	 d i . v S c r o l l e r . M i n V a l u e   =   ( d e c i m a l )   m i n ;  
 	 	 	 d i . v S c r o l l e r . M a x V a l u e   =   ( d e c i m a l )   m a x ;  
 	 	 	 d i . v S c r o l l e r . V i s i b l e R a n g e R a t i o   =   ( d e c i m a l )   r a t i o V ;  
 	 	 	 d i . v S c r o l l e r . V a l u e   =   ( d e c i m a l )   ( - c o n t e x t . O r i g i n Y ) ;  
 	 	 	 d i . v S c r o l l e r . S m a l l C h a n g e   =   ( d e c i m a l )   ( ( c s . H e i g h t * 0 . 1 ) / s c a l e . Y ) ;  
 	 	 	 d i . v S c r o l l e r . L a r g e C h a n g e   =   ( d e c i m a l )   ( ( c s . H e i g h t * 0 . 9 ) / s c a l e . Y ) ;  
  
 	 	 	 i f   (   d i . h R u l e r   ! =   n u l l   & &   d i . h R u l e r . I s V i s i b l e   )  
 	 	 	 {  
 	 	 	 	 d i . h R u l e r . P P M   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R e a l S c a l e ;  
 	 	 	 	 d i . v R u l e r . P P M   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . R e a l S c a l e ;  
  
 	 	 	 	 R e c t a n g l e   r e c t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . R e c t a n g l e D i s p l a y e d ;  
  
 	 	 	 	 d i . h R u l e r . S t a r t i n g   =   r e c t . L e f t ;  
 	 	 	 	 d i . h R u l e r . E n d i n g       =   r e c t . R i g h t ;  
  
 	 	 	 	 d i . v R u l e r . S t a r t i n g   =   r e c t . B o t t o m ;  
 	 	 	 	 d i . v R u l e r . E n d i n g       =   r e c t . T o p ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e R u l e r s ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e s   r è g l e s ,   a p r è s   l e s   a v o i r   m o n t r é e s   o u   c a c h é e s .  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
  
 	 	 	 V i e w e r   v i e w e r   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r ;  
 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 i f   (   d i . h R u l e r   = =   n u l l   )     r e t u r n ;  
  
 	 	 	 d i . h R u l e r . V i s i b i l i t y   =   ( c o n t e x t . R u l e r s S h o w ) ;  
 	 	 	 d i . v R u l e r . V i s i b i l i t y   =   ( c o n t e x t . R u l e r s S h o w ) ;  
  
 	 	 	 d o u b l e   s w   =   1 7 ;     / /   l a r g e u r   d ' u n   a s c e n s e u r  
 	 	 	 d o u b l e   s r   =   1 3 ;     / /   l a r g e u r   d ' u n e   r è g l e  
 	 	 	 d o u b l e   w m   =   4 ;     / /   m a r g e s   a u t o u r   d u   v i e w e r  
 	 	 	 d o u b l e   l m   =   0 ;  
 	 	 	 d o u b l e   t m   =   0 ;  
 	 	 	 i f   (   c o n t e x t . R u l e r s S h o w   )  
 	 	 	 {  
 	 	 	 	 l m   =   s r - 1 ;  
 	 	 	 	 t m   =   s r - 1 ;  
 	 	 	 }  
 	 	 	 v i e w e r . M a r g i n s   =   n e w   M a r g i n s ( w m + l m ,   w m + s w + 1 ,   6 + t m ,   w m + s w + 1 ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e A f t e r R e a d ( )  
 	 	 {  
 	 	 	 / / 	 E f f e c t u e   u n e   m i s e   à   j o u r   a p r è s   a v o i r   o u v e r t   u n   f i c h i e r .  
 	 	 	 i f   ( t h i s . H a s C u r r e n t D o c u m e n t )  
 	 	 	 {  
 	 	 	 	 / / 	 I l   f a u d r a   r e f a i r e   l a   l i s t e   d e s   p o l i c e s   c o n n u e s ,   c e   q u i   e s t  
 	 	 	 	 / / 	 n é c e s s a i r e   s i   l e   d o c u m e n t   o u v e r t   c o n t e n a i t   d e s   p o l i c e s   n o n  
 	 	 	 	 / / 	 i n s t a l l é e s .  
 	 	 	 	 M i s c . C l e a r F o n t L i s t ( ) ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . D i a l o g s . U p d a t e F o n t s A d d e d ( ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r o t e c t e d   s t r i n g   T e x t I n f o O b j e c t  
 	 	 {  
 	 	 	 / / 	 T e x t e   p o u r   l e s   i n f o r m a t i o n s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 D o c u m e n t   d o c   =   t h i s . C u r r e n t D o c u m e n t ;  
 	 	 	 	 i f   (   d o c   = =   n u l l   )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   "   " ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 i n t   s e l       =   d o c . M o d i f i e r . T o t a l S e l e c t e d ;  
 	 	 	 	 	 i n t   h i d e     =   d o c . M o d i f i e r . T o t a l H i d e ;  
 	 	 	 	 	 i n t   t o t a l   =   d o c . M o d i f i e r . T o t a l O b j e c t s ;  
 	 	 	 	 	 i n t   d e e p     =   d o c . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t . R o o t S t a c k D e e p ;  
  
 	 	 	 	 	 s t r i n g   s D e e p   =   R e s . S t r i n g s . S t a t u s . O b j e c t s . S e l e c t ;  
 	 	 	 	 	 i f   (   d e e p   >   2   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s D e e p   =   s t r i n g . F o r m a t ( R e s . S t r i n g s . S t a t u s . O b j e c t s . L e v e l ,   d e e p - 2 ) ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 s t r i n g   s H i d e   =   " " ;  
 	 	 	 	 	 i f   (   h i d e   >   0   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s H i d e   =   s t r i n g . F o r m a t ( " - { 0 } " ,   h i d e ) ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 r e t u r n   s t r i n g . F o r m a t ( " { 0 } :   { 1 } / { 2 } { 3 } " ,   s D e e p ,   s e l ,   t o t a l ,   s H i d e ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   T e x t I n f o M o u s e  
 	 	 {  
 	 	 	 / / 	 T e x t e   p o u r   l e s   i n f o r m a t i o n s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 D o c u m e n t   d o c   =   t h i s . C u r r e n t D o c u m e n t ;  
 	 	 	 	 i f   (   d o c   = =   n u l l   )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   "   " ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 P o i n t   m o u s e ;  
 	 	 	 	 	 i f   (   d o c . M o d i f i e r . A c t i v e V i e w e r . M o u s e P o s ( o u t   m o u s e )   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d o c . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t . S n a p G r i d ( r e f   m o u s e ) ;  
 	 	 	 	 	 	 r e t u r n   s t r i n g . F o r m a t ( " x : { 0 }   y : { 1 } " ,   d o c . M o d i f i e r . R e a l T o S t r i n g ( m o u s e . X ) ,   d o c . M o d i f i e r . R e a l T o S t r i n g ( m o u s e . Y ) ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   "   " ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   T e x t I n f o M o d i f  
 	 	 {  
 	 	 	 / / 	 T e x t e   p o u r   l e s   i n f o r m a t i o n s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 D o c u m e n t   d o c   =   t h i s . C u r r e n t D o c u m e n t ;  
 	 	 	 	 i f   (   d o c   = =   n u l l   )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   "   " ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 i f   (   d o c . M o d i f i e r . T e x t I n f o M o d i f   = =   " "   )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 	 	 i n t   c p   =   c o n t e x t . C u r r e n t P a g e ;  
 	 	 	 	 	 	 i n t   c l   =   c o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 	 	 	 O b j e c t s . P a g e   p a g e   =   t h i s . C u r r e n t D o c u m e n t . D o c u m e n t O b j e c t s [ c p ]   a s   O b j e c t s . P a g e ;  
 	 	 	 	 	 	 O b j e c t s . L a y e r   l a y e r   =   p a g e . O b j e c t s [ c l ]   a s   O b j e c t s . L a y e r ;  
  
 	 	 	 	 	 	 s t r i n g   s p   =   p a g e . I n f o N a m e ;  
  
 	 	 	 	 	 	 s t r i n g   s l   =   l a y e r . N a m e ;  
 	 	 	 	 	 	 i f   (   s l   = =   " "   )     s l   =   O b j e c t s . L a y e r . S h o r t N a m e ( c l ) ;  
  
 	 	 	 	 	 	 r e t u r n   s t r i n g . F o r m a t ( R e s . S t r i n g s . S t a t u s . M o d i f . D e f a u l t ,   s p ,   s l ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 r e t u r n   d o c . M o d i f i e r . T e x t I n f o M o d i f ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   T e x t I n f o Z o o m  
 	 	 {  
 	 	 	 / / 	 T e x t e   p o u r   l e s   i n f o r m a t i o n s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 D o c u m e n t   d o c   =   t h i s . C u r r e n t D o c u m e n t ;  
 	 	 	 	 i f   (   d o c   = =   n u l l   )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   "   " ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   d o c . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 	 d o u b l e   z o o m   =   c o n t e x t . Z o o m ;  
 	 	 	 	 	 r e t u r n   s t r i n g . F o r m a t ( R e s . S t r i n g s . S t a t u s . Z o o m . V a l u e ,   ( z o o m * 1 0 0 ) . T o S t r i n g ( " F 0 " ) ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   d o u b l e   V a l u e I n f o Z o o m  
 	 	 {  
 	 	 	 / / 	 V a l e u r   p o u r   l e s   i n f o r m a t i o n s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 D o c u m e n t   d o c   =   t h i s . C u r r e n t D o c u m e n t ;  
 	 	 	 	 i f   (   d o c   = =   n u l l   )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   0 ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 D r a w i n g C o n t e x t   c o n t e x t   =   d o c . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t ;  
 	 	 	 	 	 r e t u r n   c o n t e x t . Z o o m ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r o t e c t e d   v o i d   M o u s e S h o w W a i t ( )  
 	 	 {  
 	 	 	 / / 	 M e t   l e   s a b l i e r .  
 	 	 	 i f   (   t h i s . W i n d o w   = =   n u l l   )     r e t u r n ;  
  
 	 	 	 i f   (   t h i s . M o u s e C u r s o r   ! =   M o u s e C u r s o r . A s W a i t   )  
 	 	 	 {  
 	 	 	 	 t h i s . l a s t M o u s e C u r s o r   =   t h i s . M o u s e C u r s o r ;  
 	 	 	 }  
  
 	 	 	 t h i s . M o u s e C u r s o r   =   M o u s e C u r s o r . A s W a i t ;  
 	 	 	 t h i s . W i n d o w . M o u s e C u r s o r   =   t h i s . M o u s e C u r s o r ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   M o u s e H i d e W a i t ( )  
 	 	 {  
 	 	 	 / / 	 E n l è v e   l e   s a b l i e r .  
 	 	 	 i f   (   t h i s . W i n d o w   = =   n u l l   )     r e t u r n ;  
 	 	 	 t h i s . M o u s e C u r s o r   =   t h i s . l a s t M o u s e C u r s o r ;  
 	 	 	 t h i s . W i n d o w . M o u s e C u r s o r   =   t h i s . M o u s e C u r s o r ;  
 	 	 }  
  
  
 	 	 # r e g i o n   T a b B o o k  
 	 	 p r i v a t e   v o i d   H a n d l e B o o k D o c u m e n t s A c t i v e P a g e C h a n g e d ( o b j e c t   s e n d e r ,   C a n c e l E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 L ' o n g l e t   p o u r   l e   d o c u m e n t   c o u r a n t   a   é t é   c l i q u é .  
 	 	 	 i f   (   t h i s . i g n o r e C h a n g e   )     r e t u r n ;  
  
 	 	 	 i n t   t o t a l   =   t h i s . b o o k D o c u m e n t s . P a g e C o u n t ;  
 	 	 	 f o r   (   i n t   i = 0   ;   i < t o t a l   ;   i + +   )  
 	 	 	 {  
 	 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . d o c u m e n t s [ i ] ;  
 	 	 	 	 i f   (   d i . t a b P a g e   = =   t h i s . b o o k D o c u m e n t s . A c t i v e P a g e   )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . U s e D o c u m e n t ( i ) ;  
 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   b o o l   H a s C u r r e n t D o c u m e n t  
 	 	 {  
 	 	 	 / / 	 I n d i q u e   s ' i l   e x i s t e   u n   d o c u m e n t   c o u r a n t .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   (   t h i s . c u r r e n t D o c u m e n t   > =   0   ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   D o c u m e n t I n f o   C u r r e n t D o c u m e n t I n f o  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   D o c u m e n t I n f o   c o u r a n t .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 i f   (   t h i s . c u r r e n t D o c u m e n t   <   0   )     r e t u r n   n u l l ;  
 	 	 	 	 r e t u r n   t h i s . d o c u m e n t s [ t h i s . c u r r e n t D o c u m e n t ] ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   D o c u m e n t   C u r r e n t D o c u m e n t  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   D o c u m e n t   c o u r a n t .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 i f   (   t h i s . c u r r e n t D o c u m e n t   <   0   )     r e t u r n   n u l l ;  
 	 	 	 	 r e t u r n   t h i s . C u r r e n t D o c u m e n t I n f o . d o c u m e n t ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   I s R e c y c l a b l e D o c u m e n t ( )  
 	 	 {  
 	 	 	 / / 	 I n d i q u e   s i   l e   d o c u m e n t   e n   c o u r s   e s t   u n   d o c u m e n t   v i d e   " s a n s   t i t r e "  
 	 	 	 / / 	 p o u v a n t   s e r v i r   d e   c o n t e n e u r   p o u r   o u v r i r   u n   n o u v e a u   d o c u m e n t .  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n   f a l s e ;  
 	 	 	 i f   (   t h i s . C u r r e n t D o c u m e n t . I s D i r t y S e r i a l i z e   )     r e t u r n   f a l s e ;  
 	 	 	 i f   (   t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . S t a t i s t i c T o t a l O b j e c t s ( )   ! =   0   )     r e t u r n   f a l s e ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e D o c u m e n t ( W i n d o w   w i n d o w )  
 	 	 {  
 	 	 	 / / 	 C r é e   u n   n o u v e a u   d o c u m e n t .  
 	 	 	 t h i s . P r e p a r e C l o s e D o c u m e n t ( ) ;  
  
 	 	 	 D o c u m e n t   d o c   =   n e w   D o c u m e n t ( t h i s . d o c u m e n t T y p e ,   D o c u m e n t M o d e . M o d i f y ,   t h i s . i n s t a l l T y p e ,   t h i s . d e b u g M o d e ,   t h i s . g l o b a l S e t t i n g s ,   t h i s . C o m m a n d D i s p a t c h e r ,   t h i s . C o m m a n d C o n t e x t ,   w i n d o w ) ;  
 	 	 	 d o c . N a m e   =   " D o c u m e n t " ;  
 	 	 	 d o c . C l i p b o a r d   =   t h i s . c l i p b o a r d ;  
  
 	 	 	 D o c u m e n t I n f o   d i   =   n e w   D o c u m e n t I n f o ( ) ;  
 	 	 	 d i . d o c u m e n t   =   d o c ;  
 	 	 	 t h i s . d o c u m e n t s . I n s e r t ( + + t h i s . c u r r e n t D o c u m e n t ,   d i ) ;  
  
 	 	 	 t h i s . C r e a t e D o c u m e n t L a y o u t ( t h i s . C u r r e n t D o c u m e n t ) ;  
 	 	 	 t h i s . C o n n e c t E v e n t s ( ) ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . M o d i f i e r . N e w ( ) ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . A c t i v e P a g e   =   d i . t a b P a g e ;     / /   ( * )  
 	 	 	 t h i s . U p d a t e C l o s e C o m m a n d ( ) ;  
 	 	 	 t h i s . P r e p a r e O p e n D o c u m e n t ( ) ;  
  
 	 	 	 / /   ( * ) 	 L e   M o d i f i e r . N e w   d o i t   a v o i r   é t é   f a i t ,   c a r   c e r t a i n s   p a n n e a u x   a c c è d e n t   a u x   d i m e n s i o n s  
 	 	 	 / / 	 	 d e   l a   p a g e .   P o u r   c e l a ,   D r a w i n g C o n t e x t   d o i t   a v o i r   u n   r o o t S t a c k   i n i t i a l i s é ,   c e   q u i  
 	 	 	 / / 	 	 e s t   f a i t   p a r   M o d i f i e r . N e w .  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U s e D o c u m e n t ( i n t   r a n k )  
 	 	 {  
 	 	 	 / / 	 U t i l i s e   u n   d o c u m e n t   o u v e r t .  
 	 	 	 i f   (   t h i s . i g n o r e C h a n g e   )     r e t u r n ;  
  
 	 	 	 t h i s . P r e p a r e C l o s e D o c u m e n t ( ) ;  
 	 	 	 t h i s . c u r r e n t D o c u m e n t   =   r a n k ;  
 	 	 	 t h i s . P r e p a r e O p e n D o c u m e n t ( ) ;  
  
 	 	 	 G l o b a l I m a g e C a c h e . U n l o c k A l l ( ) ;     / /   l i b è r e   t o u t e s   l e s   i m a g e s  
  
 	 	 	 i f   ( r a n k   > =   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . i g n o r e C h a n g e   =   t r u e ;  
 	 	 	 	 t h i s . b o o k D o c u m e n t s . A c t i v e P a g e   =   t h i s . C u r r e n t D o c u m e n t I n f o . t a b P a g e ;  
 	 	 	 	 t h i s . i g n o r e C h a n g e   =   f a l s e ;  
  
 	 	 	 	 D o c u m e n t I n f o   d i ;  
 	 	 	 	 i n t   t o t a l   =   t h i s . b o o k D o c u m e n t s . P a g e C o u n t ;  
 	 	 	 	 f o r   (   i n t   i = 0   ;   i < t o t a l   ;   i + +   )  
 	 	 	 	 {  
 	 	 	 	 	 d i   =   t h i s . d o c u m e n t s [ i ] ;  
 	 	 	 	 	 d i . b o o k P a n e l s . V i s i b i l i t y   =   ( i   = =   t h i s . c u r r e n t D o c u m e n t ) ;  
 	 	 	 	 }  
  
 	 	 	 	 d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . H R u l e r   =   d i . h R u l e r ;  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . V R u l e r   =   d i . v R u l e r ;  
  
 	 	 	 	 t h i s . R i b b o n s S e t D o c u m e n t ( t h i s . g l o b a l S e t t i n g s ,   t h i s . C u r r e n t D o c u m e n t ) ;  
  
 	 	 	 	 t h i s . C o m m a n d S t a t e S h a k e ( t h i s . p a g e N e x t S t a t e ) ;  
 	 	 	 	 t h i s . C o m m a n d S t a t e S h a k e ( t h i s . p a g e P r e v S t a t e ) ;  
 	 	 	 	 t h i s . C o m m a n d S t a t e S h a k e ( t h i s . p a g e M e n u S t a t e ) ;  
 	 	 	 	 t h i s . C o m m a n d S t a t e S h a k e ( t h i s . l a y e r N e x t S t a t e ) ;  
 	 	 	 	 t h i s . C o m m a n d S t a t e S h a k e ( t h i s . l a y e r P r e v S t a t e ) ;  
 	 	 	 	 t h i s . C o m m a n d S t a t e S h a k e ( t h i s . l a y e r M e n u S t a t e ) ;  
  
 	 	 	 	 t h i s . C u r r e n t D o c u m e n t . N o t i f i e r . N o t i f y A l l C h a n g e d ( ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . R i b b o n s S e t D o c u m e n t ( t h i s . g l o b a l S e t t i n g s ,   n u l l ) ;  
  
 	 	 	 	 t h i s . H a n d l e D o c u m e n t C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e M o u s e C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e O r i g i n C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e Z o o m C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e T o o l C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e S a v e C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e S e l e c t i o n C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e G e o m e t r y C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e S t y l e C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e P a g e s C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e L a y e r s C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e U n d o R e d o C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e G r i d C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e L a b e l P r o p e r t i e s C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e C o n s t r a i n C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e M a g n e t C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e P r e v i e w C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e S e t t i n g s C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e F o n t s S e t t i n g s C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e G u i d e s C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e H i d e H a l f C h a n g e d ( ) ;  
 	 	 	 	 t h i s . H a n d l e D e b u g C h a n g e d ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C l o s e D o c u m e n t ( )  
 	 	 {  
 	 	 	 / / 	 F e r m e   l e   d o c u m e n t   c o u r a n t .  
 	 	 	 t h i s . P r e p a r e C l o s e D o c u m e n t ( ) ;  
 	 	 	 i n t   r a n k   =   t h i s . c u r r e n t D o c u m e n t ;  
 	 	 	 i f   (   r a n k   <   0   )     r e t u r n ;  
  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 t h i s . c u r r e n t D o c u m e n t   =   - 1 ;  
 	 	 	 t h i s . d o c u m e n t s . R e m o v e A t ( r a n k ) ;  
 	 	 	 t h i s . i g n o r e C h a n g e   =   t r u e ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . I t e m s . R e m o v e A t ( r a n k ) ;  
 	 	 	 t h i s . i g n o r e C h a n g e   =   f a l s e ;  
 	 	 	 d i . D i s p o s e ( ) ;  
  
 	 	 	 i f   (   r a n k   > =   t h i s . b o o k D o c u m e n t s . P a g e C o u n t   )  
 	 	 	 {  
 	 	 	 	 r a n k   =   t h i s . b o o k D o c u m e n t s . P a g e C o u n t - 1 ;  
 	 	 	 }  
 	 	 	 t h i s . U s e D o c u m e n t ( r a n k ) ;  
 	 	 	 t h i s . U p d a t e C l o s e C o m m a n d ( ) ;  
  
 	 	 	 i f   (   t h i s . C u r r e n t D o c u m e n t   = =   n u l l   )  
 	 	 	 {  
 	 	 	 	 t h i s . S e t A c t i v e R i b b o n ( t h i s . r i b b o n M a i n ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e C l o s e C o m m a n d ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l ' é t a t   d e   l a   c o m m a n d e   d e   f e r m e t u r e .  
 	 	 	 D o c u m e n t I n f o   d i   =   t h i s . C u r r e n t D o c u m e n t I n f o ;  
 	 	 	 i f   (   d i   ! =   n u l l   )  
 	 	 	 {  
 	 	 	 	 t h i s . b o o k D o c u m e n t s . A c t i v e P a g e   =   d i . t a b P a g e ;  
 	 	 	 }  
  
 	 	 	 t h i s . c l o s e S t a t e . E n a b l e   =   ( t h i s . b o o k D o c u m e n t s . P a g e C o u n t   >   0 ) ;  
 	 	 	 t h i s . c l o s e A l l S t a t e . E n a b l e   =   ( t h i s . b o o k D o c u m e n t s . P a g e C o u n t   >   0 ) ;  
 	 	 	 t h i s . f o r c e S a v e A l l S t a t e . E n a b l e   =   ( t h i s . b o o k D o c u m e n t s . P a g e C o u n t   >   0 ) ;  
 	 	 	 t h i s . o v e r w r i t e A l l S t a t e . E n a b l e   =   ( t h i s . b o o k D o c u m e n t s . P a g e C o u n t   >   0 ) ;  
 	 	 	 t h i s . n e x t D o c S t a t e . E n a b l e   =   ( t h i s . b o o k D o c u m e n t s . P a g e C o u n t   >   1 ) ;  
 	 	 	 t h i s . p r e v D o c S t a t e . E n a b l e   =   ( t h i s . b o o k D o c u m e n t s . P a g e C o u n t   >   1 ) ;  
 	 	 	  
 	 	 	 i f   (   d i   ! =   n u l l   )  
 	 	 	 {  
 	 	 	 	 t h i s . b o o k D o c u m e n t s . U p d a t e A f t e r C h a n g e s ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e B o o k D o c u m e n t s ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e   n o m   d e   l ' o n g l e t   d e s   d o c u m e n t s .  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	 T a b P a g e   t a b   =   t h i s . b o o k D o c u m e n t s . I t e m s [ t h i s . c u r r e n t D o c u m e n t ]   a s   T a b P a g e ;  
 	 	 	 t a b . T a b T i t l e   =   M i s c . E x t r a c t N a m e ( t h i s . C u r r e n t D o c u m e n t . F i l e n a m e ,   t h i s . C u r r e n t D o c u m e n t . I s D i r t y S e r i a l i z e ) ;  
 	 	 	 t h i s . b o o k D o c u m e n t s . U p d a t e A f t e r C h a n g e s ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   P r e p a r e C l o s e D o c u m e n t ( )  
 	 	 {  
 	 	 	 / / 	 P r é p a r a t i o n   a v a n t   l a   f e r m e t u r e   d ' u n   d o c u m e n t .  
 	 	 	 i f   (   ! t h i s . H a s C u r r e n t D o c u m e n t   )     r e t u r n ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . D i a l o g s . F l u s h A l l ( ) ;  
 	 	 	 t h i s . C u r r e n t D o c u m e n t . C l o s e ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   P r e p a r e O p e n D o c u m e n t ( )  
 	 	 {  
 	 	 	 / / 	 P r é p a r a t i o n   a p r è s   l ' o u v e r t u r e   d ' u n   d o c u m e n t .  
 	 	 	 t h i s . d l g E x p o r t . R e b u i l d ( ) ;  
 	 	 	 t h i s . d l g E x p o r t P D F . R e b u i l d ( ) ;  
 	 	 	 t h i s . d l g E x p o r t I C O . R e b u i l d ( ) ;  
 	 	 	 t h i s . d l g G l y p h s . R e b u i l d ( ) ;  
 	 	 	 t h i s . d l g I n f o s . R e b u i l d ( ) ;  
 	 	 	 t h i s . d l g P r i n t . R e b u i l d ( ) ;  
 	 	 	 t h i s . d l g R e p l a c e . R e b u i l d ( ) ;  
 	 	 	 t h i s . d l g S e t t i n g s . R e b u i l d ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C o m m a n d S t a t e S h a k e ( C o m m a n d S t a t e   s t a t e )  
 	 	 {  
 	 	 	 / / 	 S e c o u e   u n   C o m m a n d   p o u r   l e   f o r c e r   à   s e   r e m e t t r e   à   j o u r .  
 	 	 	 s t a t e . E n a b l e   =   ! s t a t e . E n a b l e ;  
 	 	 	 s t a t e . E n a b l e   =   ! s t a t e . E n a b l e ;  
 	 	 }  
 	 	 # e n d r e g i o n  
  
  
 	 	 # r e g i o n   G l o b a l S e t t i n g s  
 	 	 p r o t e c t e d   b o o l   R e a d G l o b a l S e t t i n g s ( )  
 	 	 {  
 	 	 	 / / 	 L i t   l e   f i c h i e r   d e s   r é g l a g e s   d e   l ' a p p l i c a t i o n .  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 u s i n g   (   S t r e a m   s t r e a m   =   F i l e . O p e n R e a d ( t h i s . G l o b a l S e t t i n g s F i l e n a m e )   )  
 	 	 	 	 {  
 	 	 	 	 	 S o a p F o r m a t t e r   f o r m a t t e r   =   n e w   S o a p F o r m a t t e r ( ) ;  
 	 	 	 	 	 f o r m a t t e r . B i n d e r   =   n e w   G e n e r i c D e s e r i a l i z a t i o n B i n d e r   ( ) ;  
  
 	 	 	 	 	 t r y  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . g l o b a l S e t t i n g s   =   ( G l o b a l S e t t i n g s )   f o r m a t t e r . D e s e r i a l i z e ( s t r e a m ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 c a t c h  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 c a t c h  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
  
 	 	 p r o t e c t e d   b o o l   W r i t e d G l o b a l S e t t i n g s ( )  
 	 	 {  
 	 	 	 / / 	 E c r i t   l e   f i c h i e r   d e s   r é g l a g e s   d e   l ' a p p l i c a t i o n .  
 	 	 	 t h i s . g l o b a l S e t t i n g s . I s F u l l S c r e e n   =   t h i s . W i n d o w . I s F u l l S c r e e n ;  
 	 	 	 t h i s . g l o b a l S e t t i n g s . M a i n W i n d o w B o u n d s   =   t h i s . W i n d o w . W i n d o w P l a c e m e n t B o u n d s ;  
  
 	 	 	 t h i s . d l g A b o u t . S a v e ( ) ;  
 	 	 	 t h i s . d l g D o w n l o a d . S a v e ( ) ;  
 	 	 	 t h i s . d l g E x p o r t T y p e . S a v e ( ) ;  
 	 	 	 t h i s . d l g E x p o r t . S a v e ( ) ;  
 	 	 	 t h i s . d l g E x p o r t P D F . S a v e ( ) ;  
 	 	 	 t h i s . d l g E x p o r t I C O . S a v e ( ) ;  
 	 	 	 t h i s . d l g G l y p h s . S a v e ( ) ;  
 	 	 	 t h i s . d l g I n f o s . S a v e ( ) ;  
 	 	 	 t h i s . d l g P a g e S t a c k . S a v e ( ) ;  
 	 	 	 t h i s . d l g P r i n t . S a v e ( ) ;  
 	 	 	 t h i s . d l g R e p l a c e . S a v e ( ) ;  
 	 	 	 t h i s . d l g S e t t i n g s . S a v e ( ) ;  
 	 	 	 t h i s . d l g U p d a t e . S a v e ( ) ;  
  
 	 	 	 t h i s . g l o b a l S e t t i n g s . A d o r n e r   =   E p s i t e c . C o m m o n . W i d g e t s . A d o r n e r s . F a c t o r y . A c t i v e N a m e ;  
  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 u s i n g   (   S t r e a m   s t r e a m   =   F i l e . O p e n W r i t e ( t h i s . G l o b a l S e t t i n g s F i l e n a m e )   )  
 	 	 	 	 {  
 	 	 	 	 	 S o a p F o r m a t t e r   f o r m a t t e r   =   n e w   S o a p F o r m a t t e r ( ) ;  
 	 	 	 	 	 f o r m a t t e r . S e r i a l i z e ( s t r e a m ,   t h i s . g l o b a l S e t t i n g s ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 c a t c h  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   G l o b a l S e t t i n g s F i l e n a m e  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   n o m   d u   f i c h i e r   d e s   r é g l a g e s   d e   l ' a p p l i c a t i o n .  
 	 	 	 / / 	 L e   d o s s i e r   e s t   q q   c h o s e   d u   g e n r e :  
 	 	 	 / / 	 C : \ D o c u m e n t s   a n d   S e t t i n g s \ D a n i e l   R o u x \ A p p l i c a t i o n   D a t a \ E p s i t e c \ C r é s u s   d o c u m e n t s  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 s t r i n g   d i r   =   C o m m o n . S u p p o r t . G l o b a l s . D i r e c t o r i e s . U s e r A p p D a t a ;  
  
 	 	 	 	 i f   (   t h i s . d o c u m e n t T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m   )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   s t r i n g . C o n c a t ( d i r ,   " \ \ C r e s u s P i c t o 2 . d a t a " ) ;     / /   ( * )  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   s t r i n g . C o n c a t ( d i r ,   " \ \ C r e s u s D o c u m e n t s 2 . d a t a " ) ;     / /   ( * )  
 	 	 	 	 }  
  
 	 	 	 	 / / 	 ( * ) 	 L e   n o m   a   p a s s é   d e   ' C r e s u s D o c u m e n t s . d a t a '   à   ' C r e s u s D o c u m e n t s 2 . d a t a '  
 	 	 	 	 / / 	 	 a f i n   d e   r e p a r t i r   d ' u n   f i c h i e r   n e u f ,   c e   q u i   e s t   n é c e s s a i r e   v u  
 	 	 	 	 / / 	 	 n o t a m m e n t   l e s   c h a n g e m e n t s   d e   d o s s i e r   ' S a m p l e s '   e n   ' E x e m p l e s   o r i g i n a u x ' .  
 	 	 	 }  
 	 	 }  
 	 	 # e n d r e g i o n  
  
  
 	 	 p r o t e c t e d   v o i d   D e l e t e T e m p o r a r y F i l e s ( )  
 	 	 {  
 	 	 	 / / 	 S u p p r i m e   t o u t e s   l e s   i m a g e s   d a n s   l e   d o s s i e r   t e m p o r a i r e ,   i s s u e   d e s   o p é r a t i o n s   " c o l l e r " .  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 i f   ( S y s t e m . I O . D i r e c t o r y . E x i s t s ( M o d i f i e r . T e m p o r a r y D i r e c t o r y ) )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   f i l t e r   =   s t r i n g . C o n c a t ( M o d i f i e r . C l i p b o a r d F i l e n a m e P r e f i x ,   " * " ,   M o d i f i e r . C l i p b o a r d F i l e n a m e P o s t f i x ) ;  
 	 	 	 	 	 s t r i n g [ ]   l i s t   =   S y s t e m . I O . D i r e c t o r y . G e t F i l e s ( M o d i f i e r . T e m p o r a r y D i r e c t o r y ,   f i l t e r ) ;  
 	 	 	 	 	 f o r e a c h   ( s t r i n g   f i l e n a m e   i n   l i s t )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t r y  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 S y s t e m . I O . F i l e . D e l e t e ( f i l e n a m e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 c a t c h  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 	 S y s t e m . I O . D i r e c t o r y . D e l e t e ( M o d i f i e r . T e m p o r a r y D i r e c t o r y ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 c a t c h  
 	 	 	 {  
 	 	 	 }  
 	 	 }  
  
  
 	 	 # r e g i o n   C h e c k  
 	 	 p r o t e c t e d   v o i d   S t a r t C h e c k ( b o o l   a l w a y s )  
 	 	 {  
 	 	 	 / / 	 L a n c e   l e   p r o c e s s u s   a s y n c h r o n e   q u i   v a   s e   c o n n e c t e r   a u   s i t e   w e b  
 	 	 	 / / 	 e t   r e g a r d e r   s ' i l   y   a   u n e   v e r s i o n   p l u s   r é c e n t e .  
 	 	 	 / / 	 C h a q u e   e x é c u t i o n   ( q u i   d é b o u c h e   s u r   u n e   v é r i f i c a t i o n )   i n c r é m e n t e   l e   c o m p t e u r  
 	 	 	 / / 	 ' / c o u n t e r / c h e c k / C r e s u s _ D o c u m e n t s '   d a n s   l a   b a s e   M y S Q L   ( p o u r   l a   v e r s i o n   f r a n ç a i s e ) .  
 	 	 	 i f   (   ! a l w a y s   & &   ! t h i s . g l o b a l S e t t i n g s . A u t o C h e c k e r   )     r e t u r n ;  
  
 	 	 	 i f   (   ! a l w a y s   )  
 	 	 	 {  
 	 	 	 	 C o m m o n . T y p e s . D a t e   t o d a y   =   C o m m o n . T y p e s . D a t e . T o d a y ;  
 	 	 	 	 i f   (   C o m m o n . T y p e s . D a t e . E q u a l s ( t h i s . g l o b a l S e t t i n g s . D a t e C h e c k e r ,   t o d a y )   )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 / / 	 E n   f r a n ç a i s ,   l ' u r l   e s t   " h t t p : / / w w w . e p s i t e c . c h / d y n a m i c s / c h e c k . p h p ? s o f t w a r e = C r e s u s _ D o c u m e n t s & v e r s i o n = 2 . 4 . 0 . 8 3 2 " .  
 	 	 	  
 	 	 	 t h i s . c h e c k e r   =   V e r s i o n C h e c k e r . C h e c k U p d a t e   (  
 	 	 	 	 t y p e o f   ( C o m m o n . D o c u m e n t E d i t o r . A p p l i c a t i o n ) . A s s e m b l y ,  
 	 	 	 	 s t r i n g . C o n c a t   ( R e s . S t r i n g s . D i a l o g . U p d a t e . W e b 4 ,   " ? s o f t w a r e = { 0 } & v e r s i o n = { 1 } " ) ,  
 	 	 	 	 R e s . S t r i n g s . A p p l i c a t i o n . S o f t N a m e ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   E n d C h e c k ( b o o l   a l w a y s )  
 	 	 {  
 	 	 	 / / 	 A t t e n d   l a   f i n   d u   p r o c e s s u s   d e   c h e c k   e t   i n d i q u e   s i   u n e   m i s e   à   j o u r   e s t  
 	 	 	 / / 	 d i s p o n i b l e .  
 	 	 	 i f   ( t h i s . c h e c k e r   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 i n t   a t t e m p t s   =   5 0 ;  
  
 	 	 	 w h i l e   ( ! t h i s . c h e c k e r . I s R e a d y   & &   a t t e m p t s - -   >   0 )  
 	 	 	 {  
 	 	 	 	 E p s i t e c . C o m m o n . W i d g e t s . W i n d o w . P u m p E v e n t s   ( ) ;  
 	 	 	 	 S y s t e m . T h r e a d i n g . T h r e a d . S l e e p ( 1 0 0 ) ;     / /   a t t e n d   0 . 1 s  
 	 	 	 }  
  
 	 	 	 t h i s . g l o b a l S e t t i n g s . D a t e C h e c k e r   =   C o m m o n . T y p e s . D a t e . T o d a y ;  
  
 	 	 	 i f   (   t h i s . c h e c k e r . F o u n d N e w e r V e r s i o n   )  
 	 	 	 {  
 	 	 	 	 s t r i n g   v e r s i o n   =   t h i s . c h e c k e r . N e w e r V e r s i o n ;  
 	 	 	 	 s t r i n g   u r l   =   t h i s . c h e c k e r . N e w e r V e r s i o n U r l ;  
 	 	 	 	  
 	 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
 	 	 	 	 t h i s . d l g D o w n l o a d . S e t I n f o ( v e r s i o n ,   u r l ) ;  
 	 	 	 	 t h i s . d l g D o w n l o a d . S h o w ( ) ;  
 	 	 	 }  
 	 	 	 e l s e   i f   (   a l w a y s   )  
 	 	 	 {  
 	 	 	 	 t h i s . d l g S p l a s h . H i d e ( ) ;  
 	 	 	 	 t h i s . d l g D o w n l o a d . S e t I n f o ( " " ,   " " ) ;  
 	 	 	 	 t h i s . d l g D o w n l o a d . S h o w ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . c h e c k e r   =   n u l l ;  
 	 	 }  
 	 	 # e n d r e g i o n  
  
  
 	 	 # r e g i o n   R e s s o u r c e s  
 	 	 p u b l i c   s t a t i c   s t r i n g   G e t R e s ( s t r i n g   n a m e )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   u n e   r e s s o u r c e   s t r i n g   d ' a p r è s   s o n   n o m .  
 	 	 	 / / 	 S i   e l l e   n ' e s t   p a s   t r o u v é   d a n s   C o m m o n . D o c u m e n t E d i t o r ,   e l l e   e s t  
 	 	 	 / / 	 c h e r c h é e   d a n s   C o m m o n . D o c u m e n t   !  
 	 	 	 s t r i n g   s   =   R e s . S t r i n g s . G e t S t r i n g ( n a m e ) ;  
 	 	 	 i f   (   s   = =   n u l l   )  
 	 	 	 {  
 	 	 	 	 s   =   E p s i t e c . C o m m o n . D o c u m e n t . D o c u m e n t . G e t R e s ( n a m e ) ;  
 	 	 	 }  
 	 	 	 r e t u r n   s ;  
 	 	 }  
 	 	 # e n d r e g i o n  
  
  
 	 	 p r o t e c t e d   D o c u m e n t T y p e 	 	 	 	 	 d o c u m e n t T y p e ;  
 	 	 p r o t e c t e d   I n s t a l l T y p e 	 	 	 	 	 i n s t a l l T y p e ;  
 	 	 p r o t e c t e d   D e b u g M o d e 	 	 	 	 	 	 d e b u g M o d e ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 	 u s e A r r a y ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 	 i n i t i a l i z a t i o n I n P r o g r e s s ;  
 	 	 p r o t e c t e d   D o c u m e n t 	 	 	 	 	 	 c l i p b o a r d ;  
 	 	 p r o t e c t e d   D o c u m e n t M a n a g e r 	 	 	 	 d e f a u l t D o c u m e n t M a n a g e r ;  
 	 	 p r o t e c t e d   i n t 	 	 	 	 	 	 	 c u r r e n t D o c u m e n t ;  
 	 	 p r o t e c t e d   L i s t < D o c u m e n t I n f o > 	 	 	 d o c u m e n t s ;  
 	 	 p r o t e c t e d   G l o b a l S e t t i n g s 	 	 	 	 g l o b a l S e t t i n g s ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 	 a s k K e y   =   f a l s e ;  
 	 	 p r o t e c t e d   M o u s e C u r s o r 	 	 	 	 	 l a s t M o u s e C u r s o r   =   M o u s e C u r s o r . A s A r r o w ;  
 	 	 p r o t e c t e d   V e r s i o n C h e c k e r 	 	 	 	 c h e c k e r ;  
  
 	 	 p r o t e c t e d   C o m m a n d D i s p a t c h e r 	 	 	 	 c o m m a n d D i s p a t c h e r ;  
 	 	 p r o t e c t e d   C o m m a n d C o n t e x t 	 	 	 	 c o m m a n d C o n t e x t ;  
  
 	 	 p r o t e c t e d   H M e n u 	 	 	 	 	 	 	 m e n u ;  
 	 	 p r o t e c t e d   V M e n u 	 	 	 	 	 	 	 f i l e M e n u ;  
 	 	 p r o t e c t e d   R i b b o n B o o k 	 	 	 	 	 r i b b o n B o o k ;  
 	 	 p r o t e c t e d   R i b b o n P a g e 	 	 	 	 	 r i b b o n M a i n ;  
 	 	 p r o t e c t e d   R i b b o n P a g e 	 	 	 	 	 r i b b o n G e o m ;  
 	 	 p r o t e c t e d   R i b b o n P a g e 	 	 	 	 	 r i b b o n O p e r ;  
 	 	 p r o t e c t e d   R i b b o n P a g e 	 	 	 	 	 r i b b o n T e x t ;  
 	 	 p r o t e c t e d   R i b b o n P a g e 	 	 	 	 	 r i b b o n A c t i v e ;  
 	 	 p r o t e c t e d   S y s t e m . C o l l e c t i o n s . A r r a y L i s t 	 r i b b o n L i s t ;  
 	 	 p r o t e c t e d   V T o o l B a r 	 	 	 	 	 	 v T o o l B a r ;  
 	 	 p r o t e c t e d   S t a t u s B a r 	 	 	 	 	 	 i n f o ;  
 	 	 p r o t e c t e d   R e s i z e K n o b 	 	 	 	 	 r e s i z e ;  
 	 	 p r o t e c t e d   T a b B o o k 	 	 	 	 	 	 b o o k D o c u m e n t s ;  
 	 	 p r o t e c t e d   d o u b l e 	 	 	 	 	 	 p a n e l s W i d t h   =   2 5 2 ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 	 i g n o r e C h a n g e ;  
 	 	 p r o t e c t e d   i n t 	 	 	 	 	 	 	 t a b I n d e x ;  
  
 	 	 p r o t e c t e d   D i a l o g s . A b o u t 	 	 	 	 	 d l g A b o u t ;  
 	 	 p r o t e c t e d   D i a l o g s . D o w n l o a d 	 	 	 	 d l g D o w n l o a d ;  
 	 	 p r o t e c t e d   D i a l o g s . E x p o r t T y p e 	 	 	 d l g E x p o r t T y p e ;  
 	 	 p r o t e c t e d   D i a l o g s . E x p o r t 	 	 	 	 d l g E x p o r t ;  
 	 	 p r o t e c t e d   D i a l o g s . E x p o r t P D F 	 	 	 	 d l g E x p o r t P D F ;  
 	 	 p r o t e c t e d   D i a l o g s . E x p o r t I C O 	 	 	 	 d l g E x p o r t I C O ;  
 	 	 p r o t e c t e d   D i a l o g s . G l y p h s 	 	 	 	 d l g G l y p h s ;  
 	 	 p r o t e c t e d   D i a l o g s . I n f o s 	 	 	 	 	 d l g I n f o s ;  
 	 	 p r o t e c t e d   D i a l o g s . F i l e E x p o r t 	 	 	 d l g F i l e E x p o r t ;  
 	 	 p r o t e c t e d   D i a l o g s . F i l e N e w 	 	 	 	 d l g F i l e N e w ;  
 	 	 p r o t e c t e d   D i a l o g s . F i l e O p e n 	 	 	 	 d l g F i l e O p e n ;  
 	 	 p r o t e c t e d   D i a l o g s . F i l e O p e n M o d e l 	 	 	 d l g F i l e O p e n M o d e l ;  
 	 	 p r o t e c t e d   D i a l o g s . F i l e S a v e 	 	 	 	 d l g F i l e S a v e ;  
 	 	 p r o t e c t e d   D i a l o g s . F i l e S a v e M o d e l 	 	 	 d l g F i l e S a v e M o d e l ;  
 	 	 p r o t e c t e d   D i a l o g s . P a g e S t a c k 	 	 	 	 d l g P a g e S t a c k ;  
 	 	 p r o t e c t e d   D i a l o g s . P r i n t 	 	 	 	 	 d l g P r i n t ;  
 	 	 p r o t e c t e d   D i a l o g s . R e p l a c e 	 	 	 	 d l g R e p l a c e ;  
 	 	 p r o t e c t e d   D i a l o g s . S e t t i n g s 	 	 	 	 d l g S e t t i n g s ;  
 	 	 p r o t e c t e d   D i a l o g s . S p l a s h 	 	 	 	 d l g S p l a s h ;  
 	 	 p r o t e c t e d   D i a l o g s . U p d a t e 	 	 	 	 d l g U p d a t e ;  
  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l S e l e c t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l G l o b a l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l S h a p e r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l E d i t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l Z o o m S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l H a n d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l P i c k e r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l H o t S p o t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l L i n e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l R e c t a n g l e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l C i r c l e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l E l l i p s e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l P o l y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l F r e e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l B e z i e r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l R e g u l a r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l S u r f a c e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l V o l u m e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l T e x t L i n e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l T e x t L i n e 2 S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l T e x t B o x S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l T e x t B o x 2 S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l A r r a y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l I m a g e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o o l D i m e n s i o n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 n e w S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 o p e n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 o p e n M o d e l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s a v e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s a v e A s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s a v e M o d e l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c l o s e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c l o s e A l l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o r c e S a v e A l l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 o v e r w r i t e A l l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 n e x t D o c S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p r e v D o c S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p r i n t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 e x p o r t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 q u i c k E x p o r t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 g l y p h s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 g l y p h s I n s e r t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t e x t E d i t i n g S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r e p l a c e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f i n d N e x t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f i n d P r e v S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f i n d D e f N e x t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f i n d D e f P r e v S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 d e l e t e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 d u p l i c a t e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c u t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c o p y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a s t e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o n t B o l d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o n t I t a l i c S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o n t U n d e r l i n e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o n t O v e r l i n e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o n t S t r i k e o u t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o n t S u b s c r i p t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o n t S u p e r s c r i p t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o n t S i z e P l u s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o n t S i z e M i n u s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f o n t C l e a r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a r a g r a p h L e a d i n g S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a r a g r a p h L e a d i n g P l u s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a r a g r a p h L e a d i n g M i n u s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a r a g r a p h I n d e n t P l u s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a r a g r a p h I n d e n t M i n u s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a r a g r a p h J u s t i f S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a r a g r a p h C l e a r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 o r d e r U p O n e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 o r d e r D o w n O n e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 o r d e r U p A l l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 o r d e r D o w n A l l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e L e f t F r e e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e R i g h t F r e e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e U p F r e e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e D o w n F r e e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r o t a t e 9 0 S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r o t a t e 1 8 0 S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r o t a t e 2 7 0 S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r o t a t e F r e e C C W S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r o t a t e F r e e C W S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m i r r o r H S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m i r r o r V S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s c a l e M u l 2 S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s c a l e D i v 2 S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s c a l e M u l F r e e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s c a l e D i v F r e e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a l i g n L e f t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a l i g n C e n t e r X S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a l i g n R i g h t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a l i g n T o p S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a l i g n C e n t e r Y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a l i g n B o t t o m S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a l i g n G r i d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r e s e t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a r e S p a c e X S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a r e L e f t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a r e C e n t e r X S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a r e R i g h t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a r e S p a c e Y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a r e T o p S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a r e C e n t e r Y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a r e B o t t o m S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a d j u s t W i d t h S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a d j u s t H e i g h t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c o l o r T o R g b S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c o l o r T o C m y k S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c o l o r T o G r a y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c o l o r S t r o k e D a r k S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c o l o r S t r o k e L i g h t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c o l o r F i l l D a r k S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c o l o r F i l l L i g h t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m e r g e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 e x t r a c t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 g r o u p S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 u n g r o u p S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 i n s i d e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 o u t s i d e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c o m b i n e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 u n c o m b i n e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o B e z i e r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o P o l y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t o T e x t B o x 2 S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 f r a g m e n t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e A d d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e S u b S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e T o L i n e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e T o C u r v e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e S y m S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e S m o o t h S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e D i s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e I n l i n e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e F r e e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e S i m p l y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e C o r n e r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e C o n t i n u e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e S h a r p S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s h a p e r H a n d l e R o u n d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 b o o l e a n A n d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 b o o l e a n O r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 b o o l e a n X o r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 b o o l e a n F r o n t M i n u s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 b o o l e a n B a c k M i n u s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 u n d o S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r e d o S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 u n d o R e d o L i s t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 d e s e l e c t A l l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t A l l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t I n v e r t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t o r A u t o S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t o r I n d i v i d u a l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t o r S c a l e r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t o r S t r e t c h S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t o r S t r e t c h T y p e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t T o t a l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t P a r t i a l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t o r A d a p t L i n e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e l e c t o r A d a p t T e x t ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 h i d e H a l f S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 h i d e S e l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 h i d e R e s t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 h i d e C a n c e l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 z o o m M i n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 z o o m P a g e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 z o o m P a g e W i d t h S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 z o o m D e f a u l t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 z o o m S e l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 z o o m S e l W i d t h S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 z o o m P r e v S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 z o o m S u b S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 z o o m A d d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p r e v i e w S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 g r i d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t e x t G r i d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t e x t S h o w C o n t r o l C h a r a c t e r s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t e x t F o n t F i l t e r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t e x t F o n t S a m p l e A b c S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t e x t I n s e r t Q u a d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t e x t I n s e r t N e w F r a m e E d g e s ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 t e x t I n s e r t N e w P a g e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 c o n s t r a i n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m a g n e t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m a g n e t L a y e r S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r u l e r s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a b e l s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a g g r e g a t e s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y O u t l i n e F r a m e E d g e s ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y O u t l i n e H o r i z S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y O u t l i n e V e r t i S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y A d d C o l u m n L e f t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y A d d C o l u m n R i g h t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y A d d R o w T o p S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y A d d R o w B o t t o m S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y D e l C o l u m n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y D e l R o w S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y A l i g n C o l u m n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y A l i g n R o w S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y S w a p C o l u m n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y S w a p R o w S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a r r a y L o o k S t a t e ;  
 # i f   f a l s e  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r e s D e s i g n e r B u i l d S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 r e s D e s i g n e r T r a n s l a t e S t a t e ;  
 # e n d i f  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 d e b u g B b o x T h i n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 d e b u g B b o x G e o m S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 d e b u g B b o x F u l l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 d e b u g D i r t y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 d e b u g C o p y F o n t s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a g e P r e v S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a g e N e x t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a g e M e n u S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a g e N e w S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a g e M i n i a t u r e s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a g e D u p l i c a t e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a g e U p S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a g e D o w n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a g e D e l e t e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r P r e v S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r N e x t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r M e n u S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r N e w S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r M i n i a t u r e s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r D u p l i c a t e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r N e w S e l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r M e r g e U p S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r M e r g e D o w n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r D e l e t e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r U p S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 l a y e r D o w n S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 s e t t i n g s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 i n f o s S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 a b o u t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 p a g e S t a c k S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 u p d a t e S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 k e y S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e L e f t N o r m S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e R i g h t N o r m S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e U p N o r m S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e D o w n N o r m S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e L e f t C t r l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e R i g h t C t r l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e U p C t r l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e D o w n C t r l S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e L e f t S h i f t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e R i g h t S h i f t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e U p S h i f t S t a t e ;  
 	 	 p r o t e c t e d   C o m m a n d S t a t e 	 	 	 	 	 m o v e D o w n S h i f t S t a t e ;  
  
 	 	 p r o t e c t e d   c l a s s   D o c u m e n t I n f o  
 	 	 {  
 	 	 	 p u b l i c   D o c u m e n t 	 	 	 	 	 	 d o c u m e n t ;  
 	 	 	 p u b l i c   T a b P a g e 	 	 	 	 	 	 t a b P a g e ;  
 	 	 	 p u b l i c   D o c W i d g e t s . H R u l e r 	 	 	 h R u l e r ;  
 	 	 	 p u b l i c   D o c W i d g e t s . V R u l e r 	 	 	 v R u l e r ;  
 	 	 	 p u b l i c   H S c r o l l e r 	 	 	 	 	 h S c r o l l e r ;  
 	 	 	 p u b l i c   V S c r o l l e r 	 	 	 	 	 v S c r o l l e r ;  
 	 	 	 p u b l i c   G l y p h B u t t o n 	 	 	 	 	 q u i c k P a g e M i n i a t u r e s ;  
 	 	 	 p u b l i c   B u t t o n 	 	 	 	 	 	 q u i c k P a g e M e n u ;  
 	 	 	 p u b l i c   G l y p h B u t t o n 	 	 	 	 	 q u i c k L a y e r M i n i a t u r e s ;  
 	 	 	 p u b l i c   B u t t o n 	 	 	 	 	 	 q u i c k L a y e r M e n u ;  
 	 	 	 p u b l i c   T a b B o o k 	 	 	 	 	 	 b o o k P a n e l s ;  
 	 	 	 p u b l i c   C o n t a i n e r s . P r i n c i p a l 	 	 	 c o n t a i n e r P r i n c i p a l ;  
 	 	 	 p u b l i c   C o n t a i n e r s . S t y l e s 	 	 	 c o n t a i n e r S t y l e s ;  
 	 	 	 p u b l i c   C o n t a i n e r s . A u t o s 	 	 	 	 c o n t a i n e r A u t o s ;  
 	 	 	 p u b l i c   C o n t a i n e r s . P a g e s 	 	 	 	 c o n t a i n e r P a g e s ;  
 	 	 	 p u b l i c   C o n t a i n e r s . L a y e r s 	 	 	 c o n t a i n e r L a y e r s ;  
 	 	 	 p u b l i c   C o n t a i n e r s . O p e r a t i o n s 	 	 c o n t a i n e r O p e r a t i o n s ;  
 	 	 	 p u b l i c   F r a m e B o x 	 	 	 	 	 	 p a g e P a n e ;  
 	 	 	 p u b l i c   F r a m e B o x 	 	 	 	 	 	 m a i n P a n e ;  
 	 	 	 p u b l i c   F r a m e B o x 	 	 	 	 	 	 l a y e r P a n e ;  
 	 	 	 p u b l i c   C o n t a i n e r s . P a g e M i n i a t u r e s 	 p a g e M i n i a t u r e s ;  
 	 	 	 p u b l i c   C o n t a i n e r s . L a y e r M i n i a t u r e s 	 l a y e r M i n i a t u r e s ;  
  
 	 	 	 p u b l i c   v o i d   D i s p o s e ( )  
 	 	 	 {  
 	 	 	 	 t h i s . d o c u m e n t . D i s p o s e   ( ) ;  
 	 	 	 	 i f   (   t h i s . t a b P a g e   ! =   n u l l   )     t h i s . t a b P a g e . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . h R u l e r   ! =   n u l l   )     t h i s . h R u l e r . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . v R u l e r   ! =   n u l l   )     t h i s . v R u l e r . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . h S c r o l l e r   ! =   n u l l   )     t h i s . h S c r o l l e r . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . v S c r o l l e r   ! =   n u l l   )     t h i s . v S c r o l l e r . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . q u i c k P a g e M i n i a t u r e s   ! =   n u l l   )     t h i s . q u i c k P a g e M i n i a t u r e s . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . q u i c k P a g e M e n u   ! =   n u l l   )     t h i s . q u i c k P a g e M e n u . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . q u i c k L a y e r M i n i a t u r e s   ! =   n u l l   )     t h i s . q u i c k L a y e r M i n i a t u r e s . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . q u i c k L a y e r M e n u   ! =   n u l l   )     t h i s . q u i c k L a y e r M e n u . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . b o o k P a n e l s   ! =   n u l l   )     t h i s . b o o k P a n e l s . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . c o n t a i n e r P r i n c i p a l   ! =   n u l l   )     t h i s . c o n t a i n e r P r i n c i p a l . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . c o n t a i n e r S t y l e s   ! =   n u l l   )     t h i s . c o n t a i n e r S t y l e s . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . c o n t a i n e r A u t o s   ! =   n u l l   )     t h i s . c o n t a i n e r A u t o s . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . c o n t a i n e r P a g e s   ! =   n u l l   )     t h i s . c o n t a i n e r P a g e s . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . c o n t a i n e r L a y e r s   ! =   n u l l   )     t h i s . c o n t a i n e r L a y e r s . D i s p o s e ( ) ;  
 	 	 	 	 i f   (   t h i s . c o n t a i n e r O p e r a t i o n s   ! =   n u l l   )     t h i s . c o n t a i n e r O p e r a t i o n s . D i s p o s e ( ) ;  
 	 	 	 }  
 	 	 }  
 	 }  
 }  
 