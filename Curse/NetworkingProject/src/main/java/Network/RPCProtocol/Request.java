package Network.RPCProtocol;

import java.io.Serializable;


public class Request implements Serializable{
    private Network.RPCProtocol.RequestType type;
    private Object data;

    private Request(){}

    public Network.RPCProtocol.RequestType type(){
        return type;
    }

    public Object data(){
        return data;
    }


    @Override
    public String toString() {
        return "Request{" +
                "type='" + type + '\'' +
                ", data='" + data + '\'' +
                '}';
    }


    public static class Builder {
        private Request request = new Request();

        public Builder type(Network.RPCProtocol.RequestType type) {
            request.type(type);
            return this;
        }

        public Builder data(Object data) {
            request.data(data);
            return this;
        }

        public Request build() {
            return request;
        }
    }

    private void data(Object data) {
        this.data = data;
    }

    private void type(Network.RPCProtocol.RequestType type) {
        this.type = type;
    }

}