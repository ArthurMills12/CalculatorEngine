using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace CalculatorEngine
{
    //responsible for rendering continuous data to the screen. 
    class EntityRenderer
    {
        /* PROPERTIES */
        private EntityShader entityShader;

        
        /* CONSTRUCTORS */
        public EntityRenderer(EntityShader entityShader, Matrix4 projectionMatrix)
        {
            //initialization:
            this.entityShader = entityShader;

            //load the projection matrix:
            this.entityShader.LoadProjectionMatrix(projectionMatrix);
        }


        /* METHODS */

        //activate the GPU memory to accept the data for a given entity.
        private void Initialize(Entity entity)
        {
            //bind the VAO of the raw model:
            GL.BindVertexArray(entity.rawModel.vaoID);

            //enable the vertex array lists needed for the entity:
            GL.EnableVertexAttribArray(0); //vPosition.
            GL.EnableVertexAttribArray(1); //vColor.
        }

        //load the transformation matrix given by the transform of the entity.
        private void PrepareInstance(Entity entity)
        {
            Transform transform = entity.transform;
            entityShader.LoadTransformationMatrix(Mathematics.CreateTransformationMatrix(transform.position, transform.xAxisRotation, transform.yAxisRotation, transform.zAxisRotation, transform.scale));
        }

        //unbind the VAO.
        private void Unbind()
        {
            //disable vertex array lists:
            GL.DisableVertexAttribArray(0); //after we draw, we are done using this.
            GL.DisableVertexAttribArray(1); //after we draw, we are done using this.

            GL.BindVertexArray(0); //unbind the VAO.
        }


        //render a batch of entities:
        public void Render(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                //prepare the GPU and shader:
                Initialize(entity);
                PrepareInstance(entity);

                //draw:
                GL.DrawArrays(PrimitiveType.Triangles, 0, entity.rawModel.vertexCount);
            }

            Unbind();
        }
    }
}
