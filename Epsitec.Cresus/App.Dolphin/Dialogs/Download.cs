ÿþ/ / 	 C o p y r i g h t   ©   2 0 0 3 - 2 0 0 8 ,   E P S I T E C   S A ,   C H - 1 0 9 2   B E L M O N T ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
  
 n a m e s p a c e   E p s i t e c . A p p . D o l p h i n . D i a l o g s  
 {  
 	 / / /   < s u m m a r y >  
 	 / / /   D i a l o g u e   " T é l é c h a r g e r   u n e   m i s e   à   j o u r " .  
 	 / / /   < / s u m m a r y >  
 	 p u b l i c   c l a s s   D o w n l o a d   :   A b s t r a c t  
 	 {  
 	 	 p u b l i c   D o w n l o a d ( D o l p h i n A p p l i c a t i o n   e d i t o r )   :   b a s e ( e d i t o r )  
 	 	 {  
 	 	 }  
  
 	 	 p u b l i c   v o i d   S e t I n f o ( s t r i n g   v e r s i o n ,   s t r i n g   u r l )  
 	 	 {  
 	 	 	 / / 	 S p é c i f i e   l e s   i n f o r m a t i o n s   p o u r   l a   m i s e   à   j o u r .  
 	 	 	 i f   (   v e r s i o n . E n d s W i t h ( " . 0 " )   )  
 	 	 	 {  
 	 	 	 	 v e r s i o n   =   v e r s i o n . S u b s t r i n g ( 0 ,   v e r s i o n . L e n g t h - 2 ) ;  
 	 	 	 }  
 	 	 	 t h i s . v e r s i o n   =   v e r s i o n ;  
 	 	 	 t h i s . u r l   =   u r l ;  
 	 	 }  
  
 	 	 p u b l i c   o v e r r i d e   v o i d   S h o w ( )  
 	 	 {  
 	 	 	 / / 	 C r é e   e t   m o n t r e   l a   f e n ê t r e   d u   d i a l o g u e .  
 	 	 	 i f   (   t h i s . w i n d o w   = =   n u l l   )  
 	 	 	 {  
 	 	 	 	 t h i s . w i n d o w   =   n e w   W i n d o w ( ) ;  
 	 	 	 	 t h i s . w i n d o w . M a k e F i x e d S i z e W i n d o w ( ) ;  
 	 	 	 	 t h i s . w i n d o w . M a k e S e c o n d a r y W i n d o w ( ) ;  
 	 	 	 	 t h i s . W i n d o w I n i t ( " D o w n l o a d " ,   2 5 0 ,   1 2 0 ) ;  
 	 	 	 	 t h i s . w i n d o w . T e x t   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . D i a l o g . D o w n l o a d . T i t l e ) ;  
 	 	 	 	 t h i s . w i n d o w . P r e v e n t A u t o C l o s e   =   t r u e ;  
 	 	 	 	 t h i s . w i n d o w . O w n e r   =   t h i s . a p p l i c a t i o n . W i n d o w ;  
 	 	 	 	 t h i s . w i n d o w . W i n d o w C l o s e C l i c k e d   + =   n e w   E v e n t H a n d l e r ( t h i s . H a n d l e W i n d o w D o w n l o a d C l o s e C l i c k e d ) ;  
  
 	 	 	 	 S t a t i c T e x t   t i t l e   =   n e w   S t a t i c T e x t ( t h i s . w i n d o w . R o o t ) ;  
 	 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y ( t h i s . u r l ) )  
 	 	 	 	 {  
 	 	 	 	 	 t i t l e . T e x t   =   R e s . S t r i n g s . D i a l o g . D o w n l o a d . U p T o D a t e ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t i t l e . T e x t   =   R e s . S t r i n g s . D i a l o g . D o w n l o a d . U p d a t e A v a i l a b l e ;  
 	 	 	 	 }  
 	 	 	 	 t i t l e . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 	 t i t l e . M a r g i n s   =   n e w   M a r g i n s ( 1 0 ,   1 0 ,   1 0 ,   0 ) ;  
 	 	 	 	 t i t l e . H y p e r t e x t C l i c k e d   + =   A b s t r a c t . H a n d l e L i n k H y p e r t e x t C l i c k e d ;  
  
 	 	 	 	 s t r i n g   c h i p   =   " < l i s t   t y p e = \ " f i x \ "   w i d t h = \ " 1 . 5 \ " / > " ;  
  
 	 	 	 	 s t r i n g   c u r r e n t   =   s t r i n g . F o r m a t ( R e s . S t r i n g s . D i a l o g . D o w n l o a d . C u r r e n t V e r s i o n ,   M i s c . G e t V e r s i o n ( ) ) ;  
 	 	 	 	 S t a t i c T e x t   a c t u a l   =   n e w   S t a t i c T e x t ( t h i s . w i n d o w . R o o t ) ;  
 	 	 	 	 a c t u a l . T e x t   =   c h i p + c u r r e n t ;  
 	 	 	 	 a c t u a l . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 	 a c t u a l . M a r g i n s   =   n e w   M a r g i n s ( 1 0 ,   1 0 ,   1 0 ,   0 ) ;  
  
 	 	 	 	 s t r i n g   t e x t ;  
 	 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y   ( t h i s . u r l ) )  
 	 	 	 	 {  
 	 	 	 	 	 t e x t   =   R e s . S t r i n g s . D i a l o g . D o w n l o a d . N o U p d a t e ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   l i n k   =   s t r i n g . F o r m a t ( R e s . S t r i n g s . D i a l o g . D o w n l o a d . U p d a t e F o u n d ,   t h i s . v e r s i o n ) ;  
 	 	 	 	 	 t e x t   =   s t r i n g . F o r m a t ( " < a   h r e f = \ " { 0 } \ " > { 1 } < / a > " ,   t h i s . u r l ,   l i n k ) ;  
 	 	 	 	 }  
 	 	 	 	 S t a t i c T e x t   u r l   =   n e w   S t a t i c T e x t ( t h i s . w i n d o w . R o o t ) ;  
 	 	 	 	 u r l . T e x t   =   c h i p + t e x t ;  
 	 	 	 	 u r l . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 	 u r l . M a r g i n s   =   n e w   M a r g i n s ( 1 0 ,   1 0 ,   0 ,   0 ) ;  
 	 	 	 	 u r l . H y p e r t e x t C l i c k e d   + =   A b s t r a c t . H a n d l e L i n k H y p e r t e x t C l i c k e d ;  
  
 	 	 	 	 / / 	 B o u t o n   d e   f e r m e t u r e .  
 	 	 	 	 B u t t o n   b u t t o n C l o s e   =   n e w   B u t t o n ( t h i s . w i n d o w . R o o t ) ;  
 	 	 	 	 b u t t o n C l o s e . P r e f e r r e d W i d t h   =   7 5 ;  
 	 	 	 	 b u t t o n C l o s e . T e x t   =   R e s . S t r i n g s . D i a l o g . C l o s e . B u t t o n ;  
 	 	 	 	 b u t t o n C l o s e . B u t t o n S t y l e   =   B u t t o n S t y l e . D e f a u l t A c c e p t A n d C a n c e l ;  
 	 	 	 	 b u t t o n C l o s e . A n c h o r   =   A n c h o r S t y l e s . B o t t o m R i g h t ;  
 	 	 	 	 b u t t o n C l o s e . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 0 ,   0 ,   1 0 ) ;  
 	 	 	 	 b u t t o n C l o s e . C l i c k e d   + =   t h i s . H a n d l e D o w n l o a d B u t t o n C l o s e C l i c k e d ;  
 	 	 	 	 b u t t o n C l o s e . T a b I n d e x   =   1 0 0 0 ;  
 	 	 	 	 b u t t o n C l o s e . T a b N a v i g a t i o n M o d e   =   T a b N a v i g a t i o n M o d e . A c t i v a t e O n T a b ;  
 	 	 	 }  
  
 	 	 	 t h i s . w i n d o w . S h o w D i a l o g ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   o v e r r i d e   v o i d   S a v e ( )  
 	 	 {  
 	 	 	 / / 	 E n r e g i s t r e   l a   p o s i t i o n   d e   l a   f e n ê t r e   d u   d i a l o g u e .  
 	 	 	 t h i s . W i n d o w S a v e ( " D o w n l o a d " ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e W i n d o w D o w n l o a d C l o s e C l i c k e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 t h i s . C l o s e W i n d o w ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e D o w n l o a d B u t t o n C l o s e C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C l o s e W i n d o w ( ) ;  
 	 	 }  
  
  
 	 	 p r o t e c t e d   s t r i n g 	 	 	 	 v e r s i o n ;  
 	 	 p r o t e c t e d   s t r i n g 	 	 	 	 u r l ;  
 	 }  
 }  
 