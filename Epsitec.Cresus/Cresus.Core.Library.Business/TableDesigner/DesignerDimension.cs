ÿþ/ / 	 C o p y r i g h t   ©   2 0 1 0 ,   E P S I T E C   S A ,   C H - 1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
  
 u s i n g   E p s i t e c . C r e s u s . C o r e . E n t i t i e s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . B u s i n e s s . F i n a n c e . P r i c e C a l c u l a t o r s ;  
  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . T a b l e D e s i g n e r  
 {  
 	 p u b l i c   c l a s s   D e s i g n e r D i m e n s i o n  
 	 {  
 	 	 p u b l i c   D e s i g n e r D i m e n s i o n ( D e s i g n e r D i m e n s i o n   d i m e n s i o n )  
 	 	 {  
 	 	 	 t h i s . H a s D e c i m a l       =   d i m e n s i o n . H a s D e c i m a l ;  
 	 	 	 t h i s . C o d e                   =   d i m e n s i o n . C o d e ;  
 	 	 	 t h i s . N a m e                   =   d i m e n s i o n . N a m e ;  
 	 	 	 t h i s . D e s c r i p t i o n     =   d i m e n s i o n . D e s c r i p t i o n ;  
 	 	 	 t h i s . R o u n d i n g M o d e   =   d i m e n s i o n . R o u n d i n g M o d e ;  
  
 	 	 	 t h i s . p o i n t s   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 f o r e a c h   ( v a r   p o i n t   i n   d i m e n s i o n . P o i n t s )  
 	 	 	 {  
 	 	 	 	 t h i s . p o i n t s . A d d   ( p o i n t ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   D e s i g n e r D i m e n s i o n ( A b s t r a c t D i m e n s i o n   d i m e n s i o n )  
 	 	 {  
 	 	 	 t h i s . H a s D e c i m a l   =   d i m e n s i o n   i s   N u m e r i c D i m e n s i o n ;  
 	 	 	 t h i s . C o d e               =   d i m e n s i o n . C o d e ;  
  
 	 	 	 i f   ( t h i s . H a s D e c i m a l )  
 	 	 	 {  
 	 	 	 	 v a r   n   =   d i m e n s i o n   a s   N u m e r i c D i m e n s i o n ;  
 	 	 	 	 t h i s . R o u n d i n g M o d e   =   n . R o u n d i n g M o d e ;  
 	 	 	 }  
  
 	 	 	 t h i s . p o i n t s   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 f o r e a c h   ( v a r   v a l u e   i n   d i m e n s i o n . V a l u e s )  
 	 	 	 {  
 	 	 	 	 t h i s . p o i n t s . A d d   ( v a l u e ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   D e s i g n e r D i m e n s i o n ( A b s t r a c t A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y   a r t i c l e P a r a m e t e r )  
 	 	 {  
 	 	 	 t h i s . C o d e                 =   a r t i c l e P a r a m e t e r . C o d e ;  
 	 	 	 t h i s . N a m e                 =   a r t i c l e P a r a m e t e r . N a m e ;  
 	 	 	 t h i s . D e s c r i p t i o n   =   a r t i c l e P a r a m e t e r . D e s c r i p t i o n ;  
  
 	 	 	 t h i s . p o i n t s   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 i f   ( a r t i c l e P a r a m e t e r   i s   N u m e r i c V a l u e A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y )  
 	 	 	 {  
 	 	 	 	 t h i s . H a s D e c i m a l   =   t r u e ;  
  
 	 	 	 	 v a r   n u m e r i c A r t i c l e P a r a m e t e r   =   a r t i c l e P a r a m e t e r   a s   N u m e r i c V a l u e A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y ;  
  
 	 	 	 	 s t r i n g [ ]   v a l u e s   =   ( n u m e r i c A r t i c l e P a r a m e t e r . P r e f e r r e d V a l u e s   ? ?   " " ) . S p l i t   ( n e w   s t r i n g [ ]   {   A b s t r a c t A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y . S e p a r a t o r   } ,   S y s t e m . S t r i n g S p l i t O p t i o n s . N o n e ) ;  
  
 	 	 	 	 f o r e a c h   ( v a r   v a l u e   i n   v a l u e s )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ! s t r i n g . I s N u l l O r W h i t e S p a c e   ( v a l u e ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d e c i m a l   d ;  
  
 	 	 	 	 	 	 i f   ( d e c i m a l . T r y P a r s e   ( v a l u e ,   o u t   d ) )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . p o i n t s . A d d   ( v a l u e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 i f   ( a r t i c l e P a r a m e t e r   i s   E n u m V a l u e A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y )  
 	 	 	 {  
 	 	 	 	 t h i s . H a s D e c i m a l   =   f a l s e ;  
  
 	 	 	 	 v a r   e n u m A r t i c l e P a r a m e t e r   =   a r t i c l e P a r a m e t e r   a s   E n u m V a l u e A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y ;  
  
 	 	 	 	 s t r i n g [ ]   v a l u e s   =   ( e n u m A r t i c l e P a r a m e t e r . V a l u e s   ? ?   " " ) . S p l i t   ( n e w   s t r i n g [ ]   {   A b s t r a c t A r t i c l e P a r a m e t e r D e f i n i t i o n E n t i t y . S e p a r a t o r   } ,   S y s t e m . S t r i n g S p l i t O p t i o n s . N o n e ) ;  
 	 	 	 	 v a r   l i s t   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 	 f o r e a c h   ( v a r   v a l u e   i n   v a l u e s )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( v a l u e ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . p o i n t s . A d d   ( v a l u e ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   b o o l   H a s D e c i m a l  
 	 	 {  
 	 	 	 g e t ;  
 	 	 	 s e t ;  
 	 	 }  
  
 	 	 p u b l i c   s t r i n g   C o d e  
 	 	 {  
 	 	 	 g e t ;  
 	 	 	 s e t ;  
 	 	 }  
  
 	 	 p u b l i c   F o r m a t t e d T e x t   N a m e  
 	 	 {  
 	 	 	 g e t ;  
 	 	 	 s e t ;  
 	 	 }  
  
 	 	 p u b l i c   F o r m a t t e d T e x t   D e s c r i p t i o n  
 	 	 {  
 	 	 	 g e t ;  
 	 	 	 s e t ;  
 	 	 }  
  
 	 	 p u b l i c   F o r m a t t e d T e x t   N i c e D e s c r i p t i o n  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 v a r   d e s c   =   T e x t F o r m a t t e r . F o r m a t T e x t   ( " ( ~ " ,   t h i s . D e s c r i p t i o n ,   " ~ ) " ) ;  
 	 	 	 	 r e t u r n   T e x t F o r m a t t e r . F o r m a t T e x t   ( t h i s . N a m e ,   d e s c ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   R o u n d i n g M o d e   R o u n d i n g M o d e  
 	 	 {  
 	 	 	 g e t ;  
 	 	 	 s e t ;  
 	 	 }  
  
 	 	 p u b l i c   L i s t < s t r i n g >   P o i n t s  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . p o i n t s ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   L i s t < d e c i m a l >   D e c i m a l P o i n t s  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . H a s D e c i m a l )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   l i s t   =   n e w   L i s t < d e c i m a l >   ( ) ;  
  
 	 	 	 	 	 f o r e a c h   ( v a r   p o i n t   i n   t h i s . p o i n t s )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 l i s t . A d d   ( d e c i m a l . P a r s e   ( p o i n t ) ) ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 r e t u r n   l i s t ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   n u l l ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   i n t   S o r t ( i n t   i n d e x )  
 	 	 {  
 	 	 	 / / 	 S i   l a   d i m e n s i o n   e s t   d e   t y p e   ' d e c i m a l ' ,   t r i e   l e s   p o i n t s   d a n s   l ' o r d r e   c r o i s s a n t .  
 	 	 	 / / 	 R e t o u r n e   l e   n o u v e l   i n d e x   d ' u n   p o i n t .  
 	 	 	 i f   ( t h i s . H a s D e c i m a l )  
 	 	 	 {  
 	 	 	 	 s t r i n g   v a l u e   =   t h i s . p o i n t s [ i n d e x ] ;  
  
 	 	 	 	 t h i s . p o i n t s . S o r t   ( ( a ,   b )   = >   D e s i g n e r D i m e n s i o n . C o m p a r e   ( a ,   b ) ) ;  
  
 	 	 	 	 r e t u r n   t h i s . p o i n t s . I n d e x O f   ( v a l u e ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 r e t u r n   i n d e x ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   v o i d   C l e a n U p ( )  
 	 	 {  
 	 	 	 / / 	 S u p p r i m e   l e s   p o i n t s   d o u b l o n s .  
 	 	 	 i n t   i   =   0 ;  
 	 	 	 w h i l e   ( i   <   t h i s . p o i n t s . C o u n t )  
 	 	 	 {  
 	 	 	 	 i f   ( ( t h i s . G e t T o t a l P o i n t   ( t h i s . p o i n t s [ i ] ) )   >   1 )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . p o i n t s . R e m o v e A t   ( i ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 i + + ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   s t a t i c   i n t   C o m p a r e ( s t r i n g   a ,   s t r i n g   b )  
 	 	 {  
 	 	 	 d e c i m a l   d a   =   D e s i g n e r D i m e n s i o n . G e t D e c i m a l   ( a   a s   s t r i n g ) ;  
 	 	 	 d e c i m a l   d b   =   D e s i g n e r D i m e n s i o n . G e t D e c i m a l   ( b   a s   s t r i n g ) ;  
  
 	 	 	 r e t u r n   d a . C o m p a r e T o   ( d b ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   d e c i m a l   G e t D e c i m a l ( s t r i n g   v a l u e )  
 	 	 {  
 	 	 	 d e c i m a l   d ;  
  
 	 	 	 i f   ( d e c i m a l . T r y P a r s e   ( v a l u e ,   o u t   d ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   d ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   0 ;  
 	 	 }  
  
 	 	 p r i v a t e   i n t   G e t T o t a l P o i n t ( s t r i n g   p o i n t )  
 	 	 {  
 	 	 	 i n t   c o u n t   =   0 ;  
  
 	 	 	 f o r e a c h   ( v a r   p   i n   p o i n t s )  
 	 	 	 {  
 	 	 	 	 i f   ( p o i n t   = =   p )  
 	 	 	 	 {  
 	 	 	 	 	 c o u n t + + ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   c o u n t ;  
 	 	 }  
  
 	  
 	 	 p r i v a t e   r e a d o n l y   L i s t < s t r i n g > 	 	 	 p o i n t s ;  
 	 }  
 }  
 