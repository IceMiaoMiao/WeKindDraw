using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class MainMenu : MonoBehaviour
{
    public GameObject Fish;
    //public Material fishMat;
    
    public RuntimeAnimatorController fishAnim;

    public SpriteSkin boneAnim;
    public RuntimeAnimatorController horseAnim;

    public void PickImage(  )
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) =>
        {
            //Debug.Log( "Image path: " + path );
            if( path != null )
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath( path, 4096 );

                //Fish.GetComponent<SpriteRenderer>().material.mainTexture = texture;
                
                if( texture == null )
                {
                    Debug.Log( "Couldn't load texture from " + path );
                    return;
                }
    
                // Assign texture to a temporary quad and destroy it after 5 seconds
                GameObject quad = GameObject.CreatePrimitive( PrimitiveType.Quad );
                quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                quad.transform.forward = Camera.main.transform.forward;
                quad.transform.localScale = new Vector3( 5f, texture.height / (float) texture.width, 5f );
    
                Material material = quad.GetComponent<Renderer>().material;
                /*
                if( !material.shader.isSupported ) // happens when Standard shader is not included in the build
                    material.shader = Shader.Find( "Universal Render Pipeline/2D/Sprite-Unlit-Default" );
                */
                
                material.shader = Shader.Find( "Universal Render Pipeline/2D/Sprite-Unlit-Default" );
                
                material.mainTexture = texture;

                Animator anim = quad.AddComponent<Animator>();
                anim.runtimeAnimatorController = fishAnim;
                
                
                Destroy( quad, 30f );
    
                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                Destroy( texture, 30f );
            }
        } );
    
        Debug.Log( "Permission result: " + permission );
    }
    public void AddBoneAnimation( )
        {
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) =>
            {
                //Debug.Log( "Image path: " + path );
                if( path != null )
                {
                    // Create Texture from selected image
                    Texture2D texture = NativeGallery.LoadImageAtPath( path, 4096 );
    
                    //Fish.GetComponent<SpriteRenderer>().material.mainTexture = texture;
                    
                    if( texture == null )
                    {
                        Debug.Log( "Couldn't load texture from " + path );
                        return;
                    }
        
                    // Assign texture to a temporary quad and destroy it after 5 seconds
                    GameObject quad = new GameObject("quad");
                    SpriteRenderer sRender=quad.AddComponent<SpriteRenderer>();
                    quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                    quad.transform.forward = Camera.main.transform.forward;
                    quad.transform.localScale = new Vector3( 2f, 2f, 2f );
        
                    Material material = quad.GetComponent<Renderer>().material;
                    /*
                    if( !material.shader.isSupported ) // happens when Standard shader is not included in the build
                        material.shader = Shader.Find( "Universal Render Pipeline/2D/Sprite-Unlit-Default" );
                    */
                    
                    material.shader = Shader.Find( "Universal Render Pipeline/2D/Sprite-Unlit-Default" );
                    
                    material.mainTexture = texture;
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    Vector2 pivot = new Vector2(0.5f, 0.5f);
                    sRender.sprite = Sprite.Create(texture, rect,pivot);
                    
                    Animator anim = quad.AddComponent<Animator>();
                    anim.runtimeAnimatorController = horseAnim;

                    SpriteSkin spriteSkin = quad.AddComponent<SpriteSkin>();
                    spriteSkin = boneAnim;
                    
                    
                    Destroy( quad, 80f );
        
                    // If a procedural texture is not destroyed manually, 
                    // it will only be freed after a scene change
                    Destroy( texture, 80f );
                }
            } );
            
        }
}
