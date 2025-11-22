import { ref } from "vue";

export const useToast = () => {
   const show = ref(false);
   const message = ref("");
   const type = ref("success");

   function open(msg, t = "success", duration = 2000) {
      message.value = msg;
      type.value = t;

      show.value = true;

      setTimeout(() => {
         show.value = false;
      }, duration);
   }

   return { show, message, type, open };
};
