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
 u s i n g   E p s i t e c . C r e s u s . C o r e . E n t i t i e s ;  
  
 u s i n g   S y s t e m . T e x t . R e g u l a r E x p r e s s i o n s ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . D o c u m e n t O p t i o n s C o n t r o l l e r  
 {  
 	 p u b l i c   c l a s s   M a i n C o n t r o l l e r  
 	 {  
 	 	 p u b l i c   M a i n C o n t r o l l e r ( B u s i n e s s C o n t e x t   b u s i n e s s C o n t e x t ,   D o c u m e n t O p t i o n s E n t i t y   d o c u m e n t O p t i o n s E n t i t y )  
 	 	 {  
 	 	 	 t h i s . b u s i n e s s C o n t e x t               =   b u s i n e s s C o n t e x t ;  
 	 	 	 t h i s . d o c u m e n t O p t i o n s E n t i t y   =   d o c u m e n t O p t i o n s E n t i t y ;  
  
 	 	 	 t h i s . o p t i o n s   =   t h i s . d o c u m e n t O p t i o n s E n t i t y . G e t O p t i o n s   ( ) ;  
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
 	 	 	 v a r   s e l e c t P a g e   =   n e w   T a b P a g e  
 	 	 	 {  
 	 	 	 	 N a m e   =   " s e l e c t " ,  
 	 	 	 	 T a b T i t l e   =   " C h o i x   d e s   o p t i o n s   à   u t i l i s e r " ,  
 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 1 0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   v a l u e s P a g e   =   n e w   T a b P a g e  
 	 	 	 {  
 	 	 	 	 N a m e   =   " v a l u e s " ,  
 	 	 	 	 T a b T i t l e   =   " V a l e u r s   d e s   o p t i o n s " ,  
 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 1 0 ) ,  
 	 	 	 } ;  
  
 	 	 	 t a b B o o k . I t e m s . A d d   ( s e l e c t P a g e ) ;  
 	 	 	 t a b B o o k . I t e m s . A d d   ( v a l u e s P a g e ) ;  
  
 	 	 	 i f   ( t h i s . o p t i o n s . C o u n t   = =   0 )  
 	 	 	 {  
 	 	 	 	 t a b B o o k . A c t i v e P a g e   =   s e l e c t P a g e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t a b B o o k . A c t i v e P a g e   =   v a l u e s P a g e ;  
 	 	 	 }  
  
 	 	 	 / / 	 P e u p l e   l e s   o n g l e t s .  
 	 	 	 v a r   s c   =   n e w   S e l e c t C o n t r o l l e r   ( t h i s . b u s i n e s s C o n t e x t ,   t h i s . d o c u m e n t O p t i o n s E n t i t y ,   t h i s . o p t i o n s ) ;  
 	 	 	 s c . C r e a t e U I   ( s e l e c t P a g e ) ;  
  
 	 	 	 v a r   v c   =   n e w   V a l u e s C o n t r o l l e r   ( t h i s . b u s i n e s s C o n t e x t ,   t h i s . d o c u m e n t O p t i o n s E n t i t y ,   t h i s . o p t i o n s ) ;  
 	 	 	 v c . C r e a t e U I   ( v a l u e s P a g e ) ;  
  
 	 	 	 / / 	 C o n n e c t i o n   d e s   é v é n e m e n t s .  
 	 	 	 t a b B o o k . A c t i v e P a g e C h a n g e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 i f   ( t a b B o o k . A c t i v e P a g e . N a m e   = =   " s e l e c t " )  
 	 	 	 	 {  
 	 	 	 	 	 s c . U p d a t e   ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t a b B o o k . A c t i v e P a g e . N a m e   = =   " v a l u e s " )  
 	 	 	 	 {  
 	 	 	 	 	 v c . U p d a t e   ( ) ;  
 	 	 	 	 }  
 	 	 	 } ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   S a v e D e s i g n ( )  
 	 	 {  
 	 	 	 t h i s . d o c u m e n t O p t i o n s E n t i t y . S e t O p t i o n s   ( t h i s . o p t i o n s ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   r e a d o n l y   B u s i n e s s C o n t e x t 	 	 	 	 	 b u s i n e s s C o n t e x t ;  
 	 	 p r i v a t e   r e a d o n l y   D o c u m e n t O p t i o n s E n t i t y 	 	 	 	 d o c u m e n t O p t i o n s E n t i t y ;  
 	 	 p r i v a t e   r e a d o n l y   P r i n t i n g O p t i o n D i c t i o n a r y 	 	 	 o p t i o n s ;  
 	 }  
 }  
 