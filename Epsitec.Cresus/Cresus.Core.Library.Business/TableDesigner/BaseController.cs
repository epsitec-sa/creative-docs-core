ÿþ/ / 	 C o p y r i g h t   ©   2 0 1 0 ,   E P S I T E C   S A ,   C H - 1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
  
 u s i n g   E p s i t e c . C r e s u s . C o r e . E n t i t i e s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . B u s i n e s s . F i n a n c e . P r i c e C a l c u l a t o r s ;  
 u s i n g   E p s i t e c . C r e s u s . C o r e . W i d g e t s ;  
  
 u s i n g   S y s t e m . T e x t . R e g u l a r E x p r e s s i o n s ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . T a b l e D e s i g n e r  
 {  
 	 p u b l i c   c l a s s   B a s e C o n t r o l l e r  
 	 {  
 	 	 p u b l i c   B a s e C o n t r o l l e r ( P r i c e C a l c u l a t o r E n t i t y   p r i c e C a l c u l a t o r E n t i t y )  
 	 	 {  
 	 	 	 t h i s . p r i c e C a l c u l a t o r E n t i t y   =   p r i c e C a l c u l a t o r E n t i t y ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   C r e a t e U I ( W i d g e t   p a r e n t )  
 	 	 {  
 	 	 	 i n t   t a b I n d e x   =   1 ;  
  
 	 	 	 v a r   f r a m e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   p a r e n t ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . F i l l ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 1 0 ) ,  
 	 	 	 } ;  
  
 	 	 	 v a r   l e f t P a n e   =   n e w   F r a m e B o x  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   f r a m e ,  
 	 	 	 	 P r e f e r r e d W i d t h   =   3 0 0 ,  
 	 	 	 	 D r a w F u l l F r a m e   =   t r u e ,  
 	 	 	 	 D o c k   =   D o c k S t y l e . L e f t ,  
 	 	 	 	 P a d d i n g   =   n e w   M a r g i n s   ( 1 0 ) ,  
 	 	 	 } ;  
  
 	 	 	 {  
 	 	 	 	 n e w   S t a t i c T e x t  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   l e f t P a n e ,  
 	 	 	 	 	 T e x t   =   " C o d e   : " ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   L i b r a r y . U I . C o n s t a n t s . M a r g i n U n d e r L a b e l ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . c o d e F i e l d   =   n e w   T e x t F i e l d E x  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   l e f t P a n e ,  
 	 	 	 	 	 D e f o c u s A c t i o n   =   D e f o c u s A c t i o n . A u t o A c c e p t O r R e j e c t E d i t i o n ,  
 	 	 	 	 	 S w a l l o w E s c a p e O n R e j e c t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 S w a l l o w R e t u r n O n A c c e p t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   L i b r a r y . U I . C o n s t a n t s . M a r g i n U n d e r T e x t F i e l d ) ,  
 	 	 	 	 	 T a b I n d e x   =   t a b I n d e x + + ,  
 	 	 	 	 } ;  
 	 	 	 }  
  
 	 	 	 / /   T O D O :   i l   f a u d r a i t   u t i l i s e r   u n e   t u i l e   E d i t i o n *   e t   U I B u i l d e r ,   a f i n   d e   g é r e r   c o r r e c t e m e n t   l e   m u l t i l a n g u e   !  
 	 	 	 {  
 	 	 	 	 n e w   S t a t i c T e x t  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   l e f t P a n e ,  
 	 	 	 	 	 T e x t   =   " N o m   : " ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   L i b r a r y . U I . C o n s t a n t s . M a r g i n U n d e r L a b e l ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . n a m e F i e l d   =   n e w   T e x t F i e l d E x  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   l e f t P a n e ,  
 	 	 	 	 	 D e f o c u s A c t i o n   =   D e f o c u s A c t i o n . A u t o A c c e p t O r R e j e c t E d i t i o n ,  
 	 	 	 	 	 S w a l l o w E s c a p e O n R e j e c t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 S w a l l o w R e t u r n O n A c c e p t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   L i b r a r y . U I . C o n s t a n t s . M a r g i n U n d e r T e x t F i e l d ) ,  
 	 	 	 	 	 T a b I n d e x   =   t a b I n d e x + + ,  
 	 	 	 	 } ;  
 	 	 	 }  
  
 	 	 	 {  
 	 	 	 	 n e w   S t a t i c T e x t  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   l e f t P a n e ,  
 	 	 	 	 	 T e x t   =   " D e s c r i p t i o n   : " ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   L i b r a r y . U I . C o n s t a n t s . M a r g i n U n d e r L a b e l ) ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . d e s c r i p t i o n F i e l d   =   n e w   T e x t F i e l d M u l t i E x  
 	 	 	 	 {  
 	 	 	 	 	 P a r e n t   =   l e f t P a n e ,  
 	 	 	 	 	 P r e f e r r e d H e i g h t   =   1 0 0 ,  
 	 	 	 	 	 D e f o c u s A c t i o n   =   D e f o c u s A c t i o n . A u t o A c c e p t O r R e j e c t E d i t i o n ,  
 	 	 	 	 	 S c r o l l e r V i s i b i l i t y   =   f a l s e ,  
 	 	 	 	 	 P r e f e r r e d L a y o u t   =   T e x t F i e l d M u l t i E x P r e f e r r e d L a y o u t . P r e s e r v e S c r o l l e r H e i g h t ,  
 	 	 	 	 	 S w a l l o w E s c a p e O n R e j e c t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 S w a l l o w R e t u r n O n A c c e p t E d i t i o n   =   t r u e ,  
 	 	 	 	 	 D o c k   =   D o c k S t y l e . T o p ,  
 	 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   0 ,   L i b r a r y . U I . C o n s t a n t s . M a r g i n U n d e r T e x t F i e l d ) ,  
 	 	 	 	 	 T a b I n d e x   =   t a b I n d e x + + ,  
 	 	 	 	 } ;  
  
 	 	 	 	 t h i s . U p d a t e   ( ) ;  
 	 	 	 }  
  
 	 	 	 / / 	 C o n n e x i o n   d e s   é v é n e m e n t s .  
 	 	 	 t h i s . c o d e F i e l d . E d i t i o n A c c e p t e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . p r i c e C a l c u l a t o r E n t i t y . C o d e   =   t h i s . c o d e F i e l d . T e x t ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . n a m e F i e l d . E d i t i o n A c c e p t e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . p r i c e C a l c u l a t o r E n t i t y . N a m e   =   t h i s . n a m e F i e l d . F o r m a t t e d T e x t ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . d e s c r i p t i o n F i e l d . E d i t i o n A c c e p t e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . p r i c e C a l c u l a t o r E n t i t y . D e s c r i p t i o n   =   t h i s . d e s c r i p t i o n F i e l d . F o r m a t t e d T e x t ;  
 	 	 	 } ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   U p d a t e ( )  
 	 	 {  
 	 	 	 t h i s . c o d e F i e l d . T e x t   =   t h i s . p r i c e C a l c u l a t o r E n t i t y . C o d e ;  
 	 	 	 t h i s . n a m e F i e l d . F o r m a t t e d T e x t   =   t h i s . p r i c e C a l c u l a t o r E n t i t y . N a m e ;  
 	 	 	 t h i s . d e s c r i p t i o n F i e l d . F o r m a t t e d T e x t   =   t h i s . p r i c e C a l c u l a t o r E n t i t y . D e s c r i p t i o n ;  
 	 	 }  
  
  
 	 	 p r i v a t e   r e a d o n l y   P r i c e C a l c u l a t o r E n t i t y 	 	 	 p r i c e C a l c u l a t o r E n t i t y ;  
  
 	 	 p r i v a t e   T e x t F i e l d E x 	 	 	 	 	 	 	 	 c o d e F i e l d ;  
 	 	 p r i v a t e   T e x t F i e l d E x 	 	 	 	 	 	 	 	 n a m e F i e l d ;  
 	 	 p r i v a t e   T e x t F i e l d M u l t i E x 	 	 	 	 	 	 d e s c r i p t i o n F i e l d ;  
 	 }  
 }  
 